using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace salesdesk_api.Shared.Host.WebApi.Telemetry;

public static class SalesDeskTelemetry
{
    public const string DefaultServiceName = "verii-salesdesk-api";
    public const string DefaultServiceNamespace = "v3rii";
    public const string ActivitySourceName = "V3RII.SalesDesk.Api";
    public const string MeterName = "V3RII.SalesDesk.Api";

    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    public static readonly Meter Meter = new(MeterName, "1.0.0");

    private static readonly Counter<long> RequestCompletionCounter = Meter.CreateCounter<long>(
        "salesdesk.request.completions",
        unit: "{request}",
        description: "Counts completed SalesDesk API requests by route, outcome, and status.");

    private static readonly Counter<long> AuditWriteCounter = Meter.CreateCounter<long>(
        "salesdesk.audit.writes",
        unit: "{audit_log}",
        description: "Counts persisted SalesDesk audit log writes.");

    public static void RecordRequestCompletion(string method, string route, string outcome, int statusCode, string? errorCode)
    {
        var tags = new TagList
        {
            { "http.request.method", method },
            { "http.route", route },
            { "salesdesk.request.outcome", outcome },
            { "http.response.status_code", statusCode }
        };

        if (!string.IsNullOrWhiteSpace(errorCode))
        {
            tags.Add("salesdesk.error_code", errorCode);
        }

        RequestCompletionCounter.Add(1, tags);
    }

    public static void RecordAuditWrite(string actionType, string entityType, string result, string? source)
    {
        var tags = new TagList
        {
            { "salesdesk.audit.action_type", actionType },
            { "salesdesk.audit.entity_type", entityType },
            { "salesdesk.audit.result", result }
        };

        if (!string.IsNullOrWhiteSpace(source))
        {
            tags.Add("salesdesk.audit.source", source);
        }

        AuditWriteCounter.Add(1, tags);
    }
}
