namespace salesdesk_api.Shared.Host.WebApi.Telemetry;

public static class ApiErrorCodes
{
    public const string ValidationFailed = "validation_failed";
    public const string Unauthorized = "unauthorized";
    public const string Forbidden = "forbidden";
    public const string NotFound = "not_found";
    public const string Conflict = "conflict";
    public const string UniqueConstraintViolation = "unique_constraint_violation";
    public const string ClientError = "client_error";
    public const string InternalServerError = "internal_server_error";
    public const string RequestCancelled = "request_cancelled";
    public const string UnhandledException = "unhandled_exception";
}
