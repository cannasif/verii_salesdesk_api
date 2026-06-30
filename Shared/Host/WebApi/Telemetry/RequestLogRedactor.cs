using Microsoft.AspNetCore.Http;

namespace salesdesk_api.Shared.Host.WebApi.Telemetry;

public static class RequestLogRedactor
{
    public static IReadOnlyDictionary<string, object?> SanitizeHeaders(
        IHeaderDictionary headers,
        RequestTelemetryOptions options)
    {
        var sensitiveHeaders = new HashSet<string>(options.SensitiveHeaderNames, StringComparer.OrdinalIgnoreCase);
        var summary = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

        foreach (var header in headers)
        {
            if (summary.Count >= options.MaxLoggedCollectionItems)
            {
                break;
            }

            summary[header.Key] = sensitiveHeaders.Contains(header.Key)
                ? "[REDACTED]"
                : Truncate(header.Value.ToString(), options.MaxLoggedValueLength);
        }

        return summary;
    }

    public static IReadOnlyDictionary<string, object?> SanitizeQuery(
        IQueryCollection query,
        RequestTelemetryOptions options)
    {
        var sensitiveQueryKeys = new HashSet<string>(options.SensitiveQueryKeys, StringComparer.OrdinalIgnoreCase);
        var summary = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

        foreach (var entry in query)
        {
            if (summary.Count >= options.MaxLoggedCollectionItems)
            {
                break;
            }

            summary[entry.Key] = sensitiveQueryKeys.Contains(entry.Key)
                ? "[REDACTED]"
                : Truncate(entry.Value.ToString(), options.MaxLoggedValueLength);
        }

        return summary;
    }

    public static string ResolveBodyLoggingMode(HttpRequest request, RequestTelemetryOptions options)
    {
        if (request.ContentLength is null or 0)
        {
            return "NoBody";
        }

        if (request.HasFormContentType || IsMultipartRequest(request))
        {
            return "DisabledForFormOrMultipart";
        }

        var normalizedPath = request.Path.Value ?? "/";
        var bodyLoggingAllowed = options.AllowedBodyLoggingPaths.Any(path =>
            normalizedPath.StartsWith(path, StringComparison.OrdinalIgnoreCase));

        return bodyLoggingAllowed ? "AllowedByPolicy" : "DisabledByPolicy";
    }

    private static bool IsMultipartRequest(HttpRequest request)
    {
        var contentType = request.ContentType;
        return !string.IsNullOrWhiteSpace(contentType)
            && contentType.Contains("multipart/", StringComparison.OrdinalIgnoreCase);
    }

    private static string? Truncate(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length <= maxLength)
        {
            return value;
        }

        return value[..maxLength];
    }
}
