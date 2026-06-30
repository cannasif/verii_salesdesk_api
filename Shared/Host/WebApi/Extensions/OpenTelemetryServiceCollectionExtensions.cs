using System.Reflection;
using salesdesk_api.Shared.Host.WebApi.Telemetry;
using Microsoft.Extensions.Options;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace salesdesk_api.Shared.Host.WebApi.Extensions;

public static class OpenTelemetryServiceCollectionExtensions
{
    public static IServiceCollection AddSalesDeskOpenTelemetry(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.Configure<OpenTelemetryOptions>(
            configuration.GetSection(OpenTelemetryOptions.SectionName));

        var telemetryOptions = configuration
            .GetSection(OpenTelemetryOptions.SectionName)
            .Get<OpenTelemetryOptions>() ?? new OpenTelemetryOptions();

        if (!telemetryOptions.Enabled)
        {
            return services;
        }

        var serviceName = string.IsNullOrWhiteSpace(telemetryOptions.ServiceName)
            ? SalesDeskTelemetry.DefaultServiceName
            : telemetryOptions.ServiceName.Trim();
        var serviceNamespace = string.IsNullOrWhiteSpace(telemetryOptions.ServiceNamespace)
            ? SalesDeskTelemetry.DefaultServiceNamespace
            : telemetryOptions.ServiceNamespace.Trim();
        var serviceVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
        var enableConsoleExporter = telemetryOptions.EnableConsoleExporter || environment.IsDevelopment();
        var hasOtlpEndpoint = Uri.TryCreate(telemetryOptions.OtlpEndpoint, UriKind.Absolute, out var otlpEndpoint);

        Action<ResourceBuilder> configureResource = resource => resource
            .AddService(
                serviceName: serviceName,
                serviceNamespace: serviceNamespace,
                serviceVersion: serviceVersion,
                serviceInstanceId: Environment.MachineName)
            .AddAttributes(new Dictionary<string, object>
            {
                ["deployment.environment.name"] = environment.EnvironmentName,
                ["host.name"] = Environment.MachineName
            });

        services.AddOpenTelemetry()
            .ConfigureResource(configureResource)
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource(SalesDeskTelemetry.ActivitySourceName)
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.RecordException = true;
                        options.Filter = static httpContext =>
                            !httpContext.Request.Path.StartsWithSegments("/hangfire", StringComparison.OrdinalIgnoreCase);
                    })
                    .AddHttpClientInstrumentation(options =>
                    {
                        options.RecordException = true;
                    })
                    .AddSqlClientInstrumentation(options =>
                    {
                        options.RecordException = true;
                    });

                if (hasOtlpEndpoint)
                {
                    tracing.AddOtlpExporter(options =>
                    {
                        options.Endpoint = otlpEndpoint!;
                    });
                }

                if (enableConsoleExporter)
                {
                    tracing.AddConsoleExporter();
                }
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddMeter(SalesDeskTelemetry.MeterName)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                if (telemetryOptions.EnableRuntimeMetrics)
                {
                    metrics.AddRuntimeInstrumentation();
                }

                if (hasOtlpEndpoint)
                {
                    metrics.AddOtlpExporter(options =>
                    {
                        options.Endpoint = otlpEndpoint!;
                    });
                }

                if (enableConsoleExporter)
                {
                    metrics.AddConsoleExporter();
                }
            });

        services.AddSingleton<IConfigureOptions<OpenTelemetryLoggerOptions>>(
            new ConfigureNamedOptions<OpenTelemetryLoggerOptions>(
                Options.DefaultName,
                logging =>
                {
                    logging.IncludeScopes = true;
                    logging.IncludeFormattedMessage = true;
                    logging.ParseStateValues = true;

                    if (hasOtlpEndpoint)
                    {
                        logging.AddOtlpExporter(options =>
                        {
                            options.Endpoint = otlpEndpoint!;
                        });
                    }

                    if (enableConsoleExporter)
                    {
                        logging.AddConsoleExporter();
                    }
                }));

        services.AddLogging(logging =>
        {
            logging.AddOpenTelemetry();
        });

        return services;
    }
}
