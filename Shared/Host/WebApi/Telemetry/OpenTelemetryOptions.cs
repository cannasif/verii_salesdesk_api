namespace salesdesk_api.Shared.Host.WebApi.Telemetry;

public sealed class OpenTelemetryOptions
{
    public const string SectionName = "OpenTelemetry";

    public bool Enabled { get; set; } = true;
    public string? ServiceName { get; set; }
    public string? ServiceNamespace { get; set; }
    public string? OtlpEndpoint { get; set; }
    public bool EnableConsoleExporter { get; set; }
    public bool EnableRuntimeMetrics { get; set; } = true;
}
