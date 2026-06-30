using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace salesdesk_api.Shared.Host.WebApi.Telemetry;

public sealed class RequestTelemetryMiddleware
{
    private const string TraceIdHeaderName = "X-Trace-Id";

    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTelemetryMiddleware> _logger;
    private readonly RequestTelemetryOptions _options;

    public RequestTelemetryMiddleware(
        RequestDelegate next,
        ILogger<RequestTelemetryMiddleware> logger,
        IOptions<RequestTelemetryOptions> options)
    {
        _next = next;
        _logger = logger;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = ResolveTraceId(context);
        context.TraceIdentifier = traceId;
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[TraceIdHeaderName] = traceId;
            return Task.CompletedTask;
        });

        var request = context.Request;
        var endpoint = context.GetEndpoint();
        var queryKeys = request.Query.Keys
            .OrderBy(static key => key, StringComparer.OrdinalIgnoreCase)
            .ToArray();
        var headerSummary = RequestLogRedactor.SanitizeHeaders(request.Headers, _options);
        var querySummary = RequestLogRedactor.SanitizeQuery(request.Query, _options);
        var startedAtUtc = DateTime.UtcNow;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var requestSummary = new
        {
            TraceId = traceId,
            Method = request.Method,
            Path = request.Path.Value ?? "/",
            Endpoint = endpoint?.DisplayName,
            QueryKeys = queryKeys,
            QueryCount = queryKeys.Length,
            Query = querySummary,
            Headers = headerSummary,
            ContentLength = request.ContentLength,
            ContentType = request.ContentType,
            BodyLogging = RequestLogRedactor.ResolveBodyLoggingMode(request, _options),
            Scheme = request.Scheme,
            BranchCode = ResolveBranchCode(context),
            UserId = ResolveUserId(context),
            UserEmail = ResolveUserEmail(context),
            RemoteIp = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = Truncate(request.Headers.UserAgent.ToString(), 256)
        };

        EnrichCurrentActivity(context, traceId, endpoint?.DisplayName);

        using var logScope = _logger.BeginScope(new Dictionary<string, object?>
        {
            ["TraceId"] = traceId,
            ["RequestMethod"] = request.Method,
            ["RequestPath"] = request.Path.Value ?? "/",
            ["Endpoint"] = endpoint?.DisplayName,
            ["BranchCode"] = ResolveBranchCode(context),
            ["UserId"] = ResolveUserId(context),
            ["UserEmail"] = ResolveUserEmail(context)
        });

        _logger.LogInformation(
            "HTTP request started. {@RequestSummary}",
            requestSummary);

        try
        {
            await _next(context);

            stopwatch.Stop();
            var completion = BuildCompletion(context, traceId, stopwatch.ElapsedMilliseconds, startedAtUtc);
            EnrichCompletionActivity(completion);
            SalesDeskTelemetry.RecordRequestCompletion(
                completion.Method,
                ResolveRouteTemplate(context),
                completion.Outcome,
                completion.StatusCode,
                completion.ErrorCode);
            LogCompletion(completion);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            stopwatch.Stop();
            var completion = BuildCompletion(
                context,
                traceId,
                stopwatch.ElapsedMilliseconds,
                startedAtUtc,
                forcedOutcome: "Cancelled");
            EnrichCompletionActivity(completion);
            SalesDeskTelemetry.RecordRequestCompletion(
                completion.Method,
                ResolveRouteTemplate(context),
                completion.Outcome,
                completion.StatusCode,
                completion.ErrorCode);
            _logger.LogWarning(
                "HTTP request cancelled. {@RequestCompletion}",
                completion);
            throw;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            var failure = BuildCompletion(
                context,
                traceId,
                stopwatch.ElapsedMilliseconds,
                startedAtUtc,
                forcedOutcome: "UnhandledException");
            EnrichCompletionActivity(failure);
            SalesDeskTelemetry.RecordRequestCompletion(
                failure.Method,
                ResolveRouteTemplate(context),
                failure.Outcome,
                failure.StatusCode,
                failure.ErrorCode);
            _logger.LogError(
                ex,
                "HTTP request failed with unhandled exception. {@RequestCompletion}",
                failure);
            throw;
        }
    }

    private void LogCompletion(RequestCompletion completion)
    {
        if (completion.StatusCode >= 500)
        {
            _logger.LogError("HTTP request completed with server error. {@RequestCompletion}", completion);
            return;
        }

        if (completion.StatusCode >= 400)
        {
            _logger.LogWarning("HTTP request completed with client/business error. {@RequestCompletion}", completion);
            return;
        }

        _logger.LogInformation("HTTP request completed successfully. {@RequestCompletion}", completion);
    }

    private static RequestCompletion BuildCompletion(
        HttpContext context,
        string traceId,
        long durationMs,
        DateTime startedAtUtc,
        string? forcedOutcome = null)
    {
        var request = context.Request;
        var statusCode = context.Response.StatusCode;

        return new RequestCompletion(
            traceId,
            request.Method,
            request.Path.Value ?? "/",
            context.GetEndpoint()?.DisplayName,
            statusCode,
            forcedOutcome ?? ClassifyOutcome(statusCode),
            RequestTelemetryContext.GetErrorCode(context) ?? InferErrorCode(statusCode, forcedOutcome),
            RequestTelemetryContext.GetFailureReason(context),
            durationMs,
            startedAtUtc,
            DateTime.UtcNow,
            context.Response.ContentLength,
            ResolveBranchCode(context),
            ResolveUserId(context),
            ResolveUserEmail(context));
    }

    private static string ClassifyOutcome(int statusCode)
    {
        if (statusCode >= 500)
        {
            return "Failed";
        }

        if (statusCode == StatusCodes.Status401Unauthorized)
        {
            return "Unauthorized";
        }

        if (statusCode == StatusCodes.Status403Forbidden)
        {
            return "Forbidden";
        }

        if (statusCode == StatusCodes.Status404NotFound)
        {
            return "NotFound";
        }

        if (statusCode == StatusCodes.Status409Conflict)
        {
            return "Conflict";
        }

        if (statusCode == StatusCodes.Status400BadRequest || statusCode == StatusCodes.Status422UnprocessableEntity)
        {
            return "ValidationFailed";
        }

        if (statusCode >= 400)
        {
            return "ClientError";
        }

        return "Succeeded";
    }

    private static string? InferErrorCode(int statusCode, string? forcedOutcome)
    {
        if (string.Equals(forcedOutcome, "Cancelled", StringComparison.Ordinal))
        {
            return ApiErrorCodes.RequestCancelled;
        }

        if (string.Equals(forcedOutcome, "UnhandledException", StringComparison.Ordinal))
        {
            return ApiErrorCodes.UnhandledException;
        }

        return statusCode switch
        {
            StatusCodes.Status400BadRequest => ApiErrorCodes.ValidationFailed,
            StatusCodes.Status401Unauthorized => ApiErrorCodes.Unauthorized,
            StatusCodes.Status403Forbidden => ApiErrorCodes.Forbidden,
            StatusCodes.Status404NotFound => ApiErrorCodes.NotFound,
            StatusCodes.Status409Conflict => ApiErrorCodes.Conflict,
            >= 500 => ApiErrorCodes.InternalServerError,
            >= 400 => ApiErrorCodes.ClientError,
            _ => null
        };
    }

    private static string ResolveTraceId(HttpContext context)
    {
        var activityTraceId = System.Diagnostics.Activity.Current?.TraceId.ToString();
        if (!string.IsNullOrWhiteSpace(activityTraceId))
        {
            return activityTraceId;
        }

        return context.TraceIdentifier ?? string.Empty;
    }

    private static string? ResolveBranchCode(HttpContext context)
    {
        return context.Items.TryGetValue("BranchCode", out var branchCode)
            ? branchCode?.ToString()
            : null;
    }

    private static long? ResolveUserId(HttpContext context)
    {
        var claim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return long.TryParse(claim, out var userId) ? userId : null;
    }

    private static string? ResolveUserEmail(HttpContext context)
    {
        return context.User.FindFirst(ClaimTypes.Email)?.Value;
    }

    private static string? Truncate(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length <= maxLength)
        {
            return value;
        }

        return value[..maxLength];
    }

    private static void EnrichCurrentActivity(HttpContext context, string traceId, string? endpointDisplayName)
    {
        var activity = System.Diagnostics.Activity.Current;
        if (activity == null)
        {
            return;
        }

        activity.SetTag("salesdesk.trace_id", traceId);
        activity.SetTag("enduser.id", ResolveUserId(context));
        activity.SetTag("user.email", ResolveUserEmail(context));
        activity.SetTag("salesdesk.branch_code", ResolveBranchCode(context));
        activity.SetTag("salesdesk.endpoint_display_name", endpointDisplayName);
    }

    private static void EnrichCompletionActivity(RequestCompletion completion)
    {
        var activity = System.Diagnostics.Activity.Current;
        if (activity == null)
        {
            return;
        }

        activity.SetTag("salesdesk.request.outcome", completion.Outcome);
        activity.SetTag("salesdesk.error_code", completion.ErrorCode);
        activity.SetTag("salesdesk.failure_reason", completion.FailureReason);
        activity.SetTag("salesdesk.duration_ms", completion.DurationMs);

        if (completion.StatusCode >= StatusCodes.Status400BadRequest)
        {
            activity.SetStatus(System.Diagnostics.ActivityStatusCode.Error, completion.ErrorCode ?? completion.FailureReason ?? completion.Outcome);
        }
    }

    private static string ResolveRouteTemplate(HttpContext context)
    {
        if (context.GetEndpoint() is Microsoft.AspNetCore.Routing.RouteEndpoint routeEndpoint)
        {
            return routeEndpoint.RoutePattern.RawText ?? context.Request.Path.Value ?? "/";
        }

        return context.Request.Path.Value ?? "/";
    }

    private sealed record RequestCompletion(
        string TraceId,
        string Method,
        string Path,
        string? Endpoint,
        int StatusCode,
        string Outcome,
        string? ErrorCode,
        string? FailureReason,
        long DurationMs,
        DateTime StartedAtUtc,
        DateTime CompletedAtUtc,
        long? ResponseContentLength,
        string? BranchCode,
        long? UserId,
        string? UserEmail);
}
