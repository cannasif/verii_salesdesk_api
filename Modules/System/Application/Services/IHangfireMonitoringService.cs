namespace salesdesk_api.Modules.System.Application.Services;

public interface IHangfireMonitoringService
{
    Task<HangfireRecurringJobsResponse> GetRecurringJobsAsync();
    Task<HangfireTriggerResponse?> TriggerRecurringJobAsync(string jobId);
    Task<HangfireStatsResponse> GetStatsAsync();
    Task<HangfireLogPageResponse<HangfireFailureDto>> GetFailuresAsync(int from, int count);
    Task<HangfireLogPageResponse<HangfireSuccessDto>> GetSuccessesAsync(int from, int count);
    Task<HangfireDeadLetterResponse> GetDeadLetterAsync(int from, int count);
}

public sealed record HangfireRecurringJobsResponse(
    IReadOnlyList<HangfireRecurringJobDto> Items,
    int Total,
    DateTime Timestamp);

public sealed record HangfireRecurringJobDto(
    string Id,
    string JobName,
    string TechnicalJobName,
    string? Method,
    string? Cron,
    string? Queue,
    string? NextExecution,
    string? LastExecution,
    string? LastJobId,
    string? Error);

public sealed record HangfireTriggerResponse(
    string JobId,
    DateTime TriggeredAt,
    string Message);

public sealed record HangfireStatsResponse(
    int Enqueued,
    int Processing,
    int Scheduled,
    int Succeeded,
    int Failed,
    int Deleted,
    int Servers,
    int Queues,
    DateTime Timestamp);

public sealed record HangfireLogPageResponse<T>(
    IReadOnlyList<T> Items,
    int Total,
    DateTime Timestamp);

public sealed record HangfireFailureDto(
    string JobId,
    string JobName,
    string TechnicalJobName,
    string FailedAt,
    string State,
    string? Reason,
    string? ExceptionType,
    int RetryCount,
    string? Queue);

public sealed record HangfireSuccessDto(
    string JobId,
    string? RecurringJobId,
    string JobName,
    string TechnicalJobName,
    string FinishedAt,
    int DurationMs,
    string? Queue,
    int RetryCount);

public sealed record HangfireDeadLetterResponse(
    string Queue,
    int Enqueued,
    IReadOnlyList<HangfireDeadLetterDto> Items,
    DateTime Timestamp);

public sealed record HangfireDeadLetterDto(
    string JobId,
    string JobName,
    string TechnicalJobName,
    string EnqueuedAt,
    string State,
    string? Reason);
