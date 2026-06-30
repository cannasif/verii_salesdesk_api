using Microsoft.AspNetCore.Http;

namespace salesdesk_api.Shared.Host.WebApi.Telemetry;

public static class RequestTelemetryContext
{
    private const string ErrorCodeKey = "__request_error_code";
    private const string FailureReasonKey = "__request_failure_reason";

    public static void SetErrorCode(HttpContext httpContext, string errorCode)
    {
        httpContext.Items[ErrorCodeKey] = errorCode;
    }

    public static string? GetErrorCode(HttpContext httpContext)
    {
        return httpContext.Items.TryGetValue(ErrorCodeKey, out var value)
            ? value?.ToString()
            : null;
    }

    public static void SetFailureReason(HttpContext httpContext, string failureReason)
    {
        httpContext.Items[FailureReasonKey] = failureReason;
    }

    public static string? GetFailureReason(HttpContext httpContext)
    {
        return httpContext.Items.TryGetValue(FailureReasonKey, out var value)
            ? value?.ToString()
            : null;
    }
}
