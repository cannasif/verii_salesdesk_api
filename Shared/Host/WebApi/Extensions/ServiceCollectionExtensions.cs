using System.Globalization;
using System.Security.Claims;
using System.Text;
using salesdesk_api.Helpers;
using salesdesk_api.Hubs;
using salesdesk_api.Infrastructure;
using salesdesk_api.Infrastructure.Filters;
using salesdesk_api.Infrastructure.Startup;
using salesdesk_api.Repositories;
using salesdesk_api.Shared.Common.Application;
using salesdesk_api.Shared.Common.Application.Common;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.Shared.Host.WebApi.Telemetry;
using salesdesk_api.Shared.Host.WebApi.Routing;
using salesdesk_api.Shared.Infrastructure.Services.Localization;
using salesdesk_api.Shared.Infrastructure.Persistence;
using salesdesk_api.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.BackgroundJobs.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace salesdesk_api.Shared.Host.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSalesdeskApiWebApi(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment,
        string[] configuredCorsOrigins)
    {
        var allowedCorsOrigins = CorsOriginMatcher.NormalizeAllowedOrigins(configuredCorsOrigins);

        var skipBackgroundBootstrap = string.Equals(
            configuration["SalesdeskRuntime:SkipBackgroundBootstrap"],
            "true",
            StringComparison.OrdinalIgnoreCase);

        if (configuredCorsOrigins.Length == 0)
        {
            throw new InvalidOperationException("Cors:AllowedOrigins ayari bos birakilamaz.");
        }

        services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddControllers(options =>
        {
            options.Conventions.Add(new IisSafeHttpMethodConvention());
            options.Filters.Add<ValidationFilterAttribute>();
        });

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);
        services.AddScoped<ValidationFilterAttribute>();
        services.AddMemoryCache();
        services.AddSalesDeskOpenTelemetry(configuration, environment);

        var dataProtectionKeyPath =
            configuration["DataProtection:KeyPath"] ??
            Path.Combine(environment.ContentRootPath, "DataProtectionKeys");
        Directory.CreateDirectory(dataProtectionKeyPath);

        services
            .AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(dataProtectionKeyPath))
            .SetApplicationName("V3RII_SALESDESK");

        services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = environment.IsDevelopment();
            options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            options.HandshakeTimeout = TimeSpan.FromSeconds(15);
        });

        services.AddDbContext<SalesDeskDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.CommandTimeout(60);
            });
        });

        services.AddHangfire(hangfire => hangfire
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        services.Configure<HangfireMonitoringOptions>(
            configuration.GetSection(HangfireMonitoringOptions.SectionName));
        services.Configure<RequestTelemetryOptions>(
            configuration.GetSection(RequestTelemetryOptions.SectionName));
        services.Configure<GeocodingOptions>(
            configuration.GetSection(GeocodingOptions.SectionName));

        // Retry is opt-in. ERP/import jobs usually fail because of persistent data or configuration issues.
        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute
        {
            Attempts = 0,
            LogEvents = true,
            OnAttemptsExceeded = AttemptsExceededAction.Fail
        });

        if (!skipBackgroundBootstrap)
        {
            services.AddHangfireServer(options =>
            {
                options.Queues = new[] { "default", "dead-letter" };
            });

            services.AddHostedService<AdminBootstrapHostedService>();
            services.AddHostedService<SystemSettingsBootstrapHostedService>();
        }
        services.AddAutoMapper(typeof(AssemblyMarker).Assembly);
        services.AddSharedInfrastructureModule();
        services.AddSystemModule();
        services.AddIdentityModule();
        services.AddAccessControlModule();
        services.AddSalesDeskModule();
        services.AddSmtpIntegrationModule();
        services.AddNotificationModule();

        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("tr-TR"),
                new CultureInfo("de-DE"),
                new CultureInfo("fr-FR"),
                new CultureInfo("es-ES"),
                new CultureInfo("it-IT"),
                new CultureInfo("ar-SA")
            };

            options.DefaultRequestCulture = new RequestCulture("tr-TR");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders.Insert(0, new CustomHeaderRequestCultureProvider());
        });

        services.AddCors(options =>
        {
            options.AddPolicy("DevCors", policy =>
            {
                policy.SetIsOriginAllowed(origin => CorsOriginMatcher.IsAllowed(origin, allowedCorsOrigins))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = !environment.IsDevelopment();
            options.SaveToken = true;

            var jwtSecret = configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrWhiteSpace(jwtSecret))
            {
                throw new InvalidOperationException("JwtSettings:SecretKey zorunludur.");
            }

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"] ?? "SalesDeskApi",
                ValidAudience = configuration["JwtSettings:Audience"] ?? "SalesDeskApiUsers",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken) && (
                        path.StartsWithSegments("/api/notificationHub") ||
                        path.StartsWithSegments("/notificationHub")))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var localizationService = context.HttpContext.RequestServices.GetRequiredService<ILocalizationService>();
                    var claims = context.Principal?.Claims;
                    var userIdClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (!long.TryParse(userIdClaim, out var userId))
                    {
                        context.Fail(localizationService.GetLocalizedString("Auth.TokenInvalidMissingUserId"));
                        return;
                    }

                    var sessionClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value
                        ?? claims?.FirstOrDefault(c => c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti)?.Value;
                    if (!Guid.TryParse(sessionClaim, out var sessionId))
                    {
                        context.Fail(localizationService.GetLocalizedString("Auth.TokenInvalidMissingSessionId"));
                        return;
                    }

                    try
                    {
                        var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
                        var sessionCacheService = context.HttpContext.RequestServices.GetRequiredService<IUserSessionCacheService>();
                        var cacheKey = sessionCacheService.GetCacheKey(sessionId);

                        if (cache.TryGetValue<long>(cacheKey, out var cachedUserId))
                        {
                            if (cachedUserId != userId)
                            {
                                context.Fail(localizationService.GetLocalizedString("Auth.SessionExpiredOrInvalid"));
                            }

                            return;
                        }

                        var restored = await sessionCacheService.RestoreSessionAsync(
                            sessionId,
                            userId,
                            context.HttpContext.RequestAborted);
                        if (!restored)
                        {
                            context.Fail(localizationService.GetLocalizedString("Auth.SessionExpiredOrInvalid"));
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Fail(localizationService.GetLocalizedString("Auth.SessionValidationFailed", ex.Message));
                    }
                }
                ,
                OnChallenge = async context =>
                {
                    context.HandleResponse();

                    var localizationService = context.HttpContext.RequestServices.GetRequiredService<ILocalizationService>();
                    ApplyCorsHeaders(context.HttpContext.Response, context.HttpContext.Request.Headers["Origin"], allowedCorsOrigins);

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var response = ApiResponse<object>.ErrorResult(
                        localizationService.GetLocalizedString("General.Unauthorized"),
                        context.ErrorDescription ?? context.Error ?? localizationService.GetLocalizedString("General.Unauthorized"),
                        StatusCodes.Status401Unauthorized,
                        errorCode: ApiErrorCodes.Unauthorized);
                    response.TraceId = System.Diagnostics.Activity.Current?.TraceId.ToString() ?? context.HttpContext.TraceIdentifier;

                    await context.Response.WriteAsJsonAsync(response);
                },
                OnForbidden = async context =>
                {
                    var localizationService = context.HttpContext.RequestServices.GetRequiredService<ILocalizationService>();
                    ApplyCorsHeaders(context.Response, context.Request.Headers["Origin"], allowedCorsOrigins);

                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";

                    var response = ApiResponse<object>.ErrorResult(
                        localizationService.GetLocalizedString("General.Forbidden"),
                        localizationService.GetLocalizedString("General.Forbidden"),
                        StatusCodes.Status403Forbidden,
                        errorCode: ApiErrorCodes.Forbidden);
                    response.TraceId = System.Diagnostics.Activity.Current?.TraceId.ToString() ?? context.HttpContext.TraceIdentifier;

                    await context.Response.WriteAsJsonAsync(response);
                }
            };
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SalesDesk Web API",
                Version = "v1",
                Description = "A comprehensive SalesDesk Web API with JWT Authentication",
                Contact = new OpenApiContact
                {
                    Name = "SalesDesk API Team",
                    Email = "support@salesdeskapi.com"
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityDefinition("Language", new OpenApiSecurityScheme
            {
                Description = "Language header for localization. Use 'tr' for Turkish or 'en' for English. Example: \"x-language: tr\"",
                Name = "x-language",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKey"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Language"
                        }
                    },
                    new List<string>()
                }
            });

            options.CustomSchemaIds(type => type.FullName);
            options.MapType<IFormFile>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            });
            options.ParameterFilter<FileUploadParameterFilter>();
            options.OperationFilter<FileUploadOperationFilter>();
            options.CustomOperationIds(apiDesc => apiDesc.ActionDescriptor.RouteValues["action"]);

            var xmlFile = $"{typeof(ServiceCollectionExtensions).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        return services;
    }

    private static void ApplyCorsHeaders(HttpResponse response, StringValues originHeader, IReadOnlyCollection<string> allowedCorsOrigins)
    {
        var origin = originHeader.ToString();
        if (!CorsOriginMatcher.IsAllowed(origin, allowedCorsOrigins))
        {
            return;
        }

        response.Headers["Access-Control-Allow-Origin"] = origin;
        response.Headers["Access-Control-Allow-Credentials"] = "true";
        response.Headers["Vary"] = "Origin";
    }
}
