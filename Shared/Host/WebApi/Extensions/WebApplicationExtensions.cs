using salesdesk_api.Helpers;
using salesdesk_api.Hubs;
using Hangfire;
using Hangfire.Dashboard;
using Infrastructure.BackgroundJobs.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;
using salesdesk_api.Shared.Host.WebApi.Telemetry;

namespace salesdesk_api.Shared.Host.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseSalesdeskApiWebApi(this WebApplication app, string[] configuredCorsOrigins)
    {
        GlobalJobFilters.Filters.Add(
            new HangfireJobStateFilter(
                app.Services.GetRequiredService<ILogger<HangfireJobStateFilter>>(),
                app.Services.GetRequiredService<IBackgroundJobClient>(),
                app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<HangfireMonitoringOptions>>(),
                app.Services.GetRequiredService<IServiceScopeFactory>()));

        var allowedCorsOrigins = CorsOriginMatcher.NormalizeAllowedOrigins(configuredCorsOrigins);

        app.Use(async (ctx, next) =>
        {
            var origin = ctx.Request.Headers["Origin"].ToString();
            var isAllowedCorsOrigin = CorsOriginMatcher.IsAllowed(origin, allowedCorsOrigins);
            if (isAllowedCorsOrigin)
            {
                ctx.Response.OnStarting(() =>
                {
                    ApplyCorsHeaders(ctx, origin);
                    return Task.CompletedTask;
                });

                if (HttpMethods.IsOptions(ctx.Request.Method))
                {
                    ApplyCorsHeaders(ctx, origin);
                    ctx.Response.StatusCode = StatusCodes.Status204NoContent;
                    return;
                }
            }

            await next();
        });

        app.UseExceptionHandler(errApp =>
        {
            errApp.Run(async ctx =>
            {
                var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
                var logger = ctx.RequestServices.GetService<ILogger<Program>>();
                var localizationService = ctx.RequestServices.GetRequiredService<ILocalizationService>();
                if (ex != null)
                {
                    logger?.LogError(ex, "Unhandled exception: {Path}", ctx.Request.Path);
                }

                var dbUpdateException = FindDbUpdateException(ex);
                if (dbUpdateException != null &&
                    DbUpdateExceptionHelper.TryGetUniqueViolation(dbUpdateException, out _))
                {
                    var isCountryPath = ctx.Request.Path.StartsWithSegments("/api/Country", StringComparison.OrdinalIgnoreCase);
                    var localizedMessage = isCountryPath
                        ? localizationService.GetLocalizedString("CountryService.CountryNameAlreadyExists")
                        : localizationService.GetLocalizedString("General.RecordAlreadyExists");

                    var conflictResponse = ApiResponse<object>.ErrorResult(
                        localizedMessage,
                        null,
                        StatusCodes.Status409Conflict,
                        errorCode: ApiErrorCodes.UniqueConstraintViolation);
                    conflictResponse.Errors = new List<string> { localizedMessage };
                    conflictResponse.Timestamp = DateTime.UtcNow;
                    conflictResponse.ExceptionMessage = null!;
                    RequestTelemetryContext.SetErrorCode(ctx, ApiErrorCodes.UniqueConstraintViolation);
                    RequestTelemetryContext.SetFailureReason(ctx, "DbUniqueConstraintViolation");
                    if (isCountryPath)
                    {
                        conflictResponse.ClassName = "ApiResponse<CountryGetDto>";
                    }

                    ctx.Response.StatusCode = StatusCodes.Status409Conflict;
                    ctx.Response.ContentType = "application/json";

                    var conflictJson = System.Text.Json.JsonSerializer.Serialize(conflictResponse);
                    await ctx.Response.WriteAsync(conflictJson);
                    return;
                }

                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ctx.Response.ContentType = "application/json";

                var fallbackError = localizationService.GetLocalizedString("General.InternalServerError");
                var message = ex?.Message ?? fallbackError;
                RequestTelemetryContext.SetErrorCode(ctx, ApiErrorCodes.InternalServerError);
                RequestTelemetryContext.SetFailureReason(ctx, ex?.GetType().Name ?? "UnhandledException");

                var serverErrorResponse = ApiResponse<object>.ErrorResult(
                    fallbackError,
                    message,
                    StatusCodes.Status500InternalServerError,
                    errorCode: ApiErrorCodes.InternalServerError);
                serverErrorResponse.TraceId = System.Diagnostics.Activity.Current?.TraceId.ToString() ?? ctx.TraceIdentifier;

                var json = System.Text.Json.JsonSerializer.Serialize(serverErrorResponse);
                await ctx.Response.WriteAsync(json);
            });
        });

        app.Use(async (ctx, next) =>
        {
            if (HttpMethods.IsPost(ctx.Request.Method))
            {
                if (TryResolvePostVerbFallback(ctx.Request.Path, out var normalizedPath, out var normalizedMethod))
                {
                    ctx.Request.Method = normalizedMethod;
                    ctx.Request.Path = normalizedPath;
                }
                else if (ctx.Request.Headers.TryGetValue("X-HTTP-Method-Override", out var overrideMethod))
                {
                    var method = overrideMethod.ToString().Trim().ToUpperInvariant();
                    if (method is "PUT" or "PATCH" or "DELETE")
                    {
                        ctx.Request.Method = method;
                    }
                }
            }

            await next();
        });

        app.UseRouting();
        app.UseCors("DevCors");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SalesDesk Web API v1");
                options.RoutePrefix = "swagger";
            });
        }

        app.UseStaticFiles();

        var uploadsPath = Path.Combine(app.Environment.ContentRootPath, "uploads");
        if (Directory.Exists(uploadsPath))
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/uploads"
            });
        }

        var androidVersionsPath = Path.Combine(
            app.Environment.ContentRootPath,
            "Shared",
            "Host",
            "WebApi",
            "Assets",
            "AndroidVersions");
        if (Directory.Exists(androidVersionsPath))
        {
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            contentTypeProvider.Mappings[".apk"] = "application/vnd.android.package-archive";

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(androidVersionsPath),
                RequestPath = "/android-versions",
                ContentTypeProvider = contentTypeProvider
            });
        }

        app.UseRequestLocalization();
        app.UseMiddleware<BranchCodeMiddleware>();
        app.UseAuthentication();
        app.UseMiddleware<RequestTelemetryMiddleware>();
        app.UseAuthorization();

        app.MapHub<salesdesk_api.Hubs.NotificationHub>("/notificationHub");
        app.MapControllers();

        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangfireAuthorizationFilter() }
        });

        var skipBackgroundBootstrap = string.Equals(
            app.Configuration["SalesdeskRuntime:SkipBackgroundBootstrap"],
            "true",
            StringComparison.OrdinalIgnoreCase);
        if (skipBackgroundBootstrap)
        {
            app.Logger.LogWarning(
                "Hangfire background server is disabled (SalesdeskRuntime:SkipBackgroundBootstrap=true). " +
                "BackgroundJob.Enqueue calls will not run until a Hangfire worker is started.");
        }
        else
        {
            app.Logger.LogInformation(
                "Hangfire background server is enabled (queues: default, dead-letter). " +
                "Critical mail jobs (MailJob) are monitored; failed jobs appear in /hangfire.");
        }

        return app;
    }

    private static void ApplyCorsHeaders(HttpContext ctx, string origin)
    {
        if (ctx.Response.HasStarted)
        {
            return;
        }

        ctx.Response.Headers["Access-Control-Allow-Origin"] = origin;
        ctx.Response.Headers["Access-Control-Allow-Credentials"] = "true";
        ctx.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, PATCH, DELETE, OPTIONS";
        ctx.Response.Headers["Access-Control-Allow-Headers"] =
            "Content-Type, Authorization, X-Branch-Code, Branch-Code, X-Language, x-language, x-branch-code, X-Requested-With, x-requested-with, X-SignalR-User-Agent, x-signalr-user-agent, X-HTTP-Method-Override, x-http-method-override";
        ctx.Response.Headers["Access-Control-Max-Age"] = "86400";

        var vary = ctx.Response.Headers.Vary.ToString();
        if (!vary.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Any(value => string.Equals(value, "Origin", StringComparison.OrdinalIgnoreCase)))
        {
            ctx.Response.Headers.Vary = string.IsNullOrWhiteSpace(vary) ? "Origin" : $"{vary}, Origin";
        }
    }

    private static bool TryResolvePostVerbFallback(PathString requestPath, out PathString normalizedPath, out string normalizedMethod)
    {
        var path = requestPath.Value ?? string.Empty;
        normalizedPath = requestPath;
        normalizedMethod = HttpMethods.Post;

        if (path.EndsWith("/update", StringComparison.OrdinalIgnoreCase))
        {
            normalizedPath = new PathString(path[..^"/update".Length]);
            normalizedMethod = HttpMethods.Put;
            return HasNumericTailSegment(normalizedPath);
        }

        if (path.EndsWith("/delete", StringComparison.OrdinalIgnoreCase))
        {
            normalizedPath = new PathString(path[..^"/delete".Length]);
            normalizedMethod = HttpMethods.Delete;
            return HasNumericTailSegment(normalizedPath);
        }

        return false;
    }

    private static bool HasNumericTailSegment(PathString path)
    {
        var value = path.Value?.TrimEnd('/') ?? string.Empty;
        var lastSlashIndex = value.LastIndexOf('/');
        if (lastSlashIndex < 0 || lastSlashIndex == value.Length - 1)
        {
            return false;
        }

        var tail = value[(lastSlashIndex + 1)..];
        return long.TryParse(tail, out _);
    }

    private static DbUpdateException? FindDbUpdateException(Exception? exception)
    {
        var current = exception;
        while (current != null)
        {
            if (current is DbUpdateException dbUpdateException)
            {
                return dbUpdateException;
            }

            current = current.InnerException;
        }

        return null;
    }
}
