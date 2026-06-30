namespace salesdesk_api.Shared.Host.WebApi.Telemetry;

public sealed class RequestTelemetryOptions
{
    public const string SectionName = "RequestTelemetry";

    public int MaxLoggedValueLength { get; set; } = 128;

    public int MaxLoggedCollectionItems { get; set; } = 20;

    public List<string> SensitiveHeaderNames { get; set; } =
    [
        "Authorization",
        "Cookie",
        "Set-Cookie",
        "X-Api-Key"
    ];

    public List<string> SensitiveQueryKeys { get; set; } =
    [
        "token",
        "access_token",
        "refresh_token",
        "password",
        "secret",
        "code"
    ];

    public List<string> AllowedBodyLoggingPaths { get; set; } = [];
}
