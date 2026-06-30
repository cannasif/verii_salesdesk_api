using salesdesk_api.Modules.System.Infrastructure.Monitoring;
using salesdesk_api.Shared.Infrastructure.Persistence;
using Hangfire;
using Hangfire.Storage;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.System.Application.Services;

public class HangfireMonitoringService : IHangfireMonitoringService
{
    private const int DefaultCount = 50;
    private const int MaxCount = 200;
    private readonly SalesDeskDbContext _db;
    private readonly ILocalizationService _localizationService;

    public HangfireMonitoringService(SalesDeskDbContext db, ILocalizationService localizationService)
    {
        _db = db;
        _localizationService = localizationService;
    }

    public Task<HangfireRecurringJobsResponse> GetRecurringJobsAsync()
    {
        using var connection = JobStorage.Current.GetConnection();
        var jobs = connection.GetRecurringJobs()
            .OrderBy(x => x.Id)
            .Select(x => new HangfireRecurringJobDto(
                x.Id,
                HangfireJobDisplayNameResolver.Resolve(x.Id, x.Job?.Type, x.Job?.Method?.Name, x.Job?.Type?.Name ?? x.Id),
                x.Job?.Type?.FullName ?? x.Id,
                x.Job?.Method?.Name,
                x.Cron,
                x.Queue,
                x.NextExecution?.ToString("o"),
                x.LastExecution?.ToString("o"),
                x.LastJobId,
                x.Error))
            .ToList();

        return Task.FromResult(new HangfireRecurringJobsResponse(jobs, jobs.Count, DateTime.UtcNow));
    }

    public Task<HangfireTriggerResponse?> TriggerRecurringJobAsync(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
        {
            return Task.FromResult<HangfireTriggerResponse?>(null);
        }

        using var connection = JobStorage.Current.GetConnection();
        var exists = connection.GetRecurringJobs()
            .Any(x => string.Equals(x.Id, jobId, StringComparison.OrdinalIgnoreCase));

        if (!exists)
        {
            return Task.FromResult<HangfireTriggerResponse?>(null);
        }

        RecurringJob.TriggerJob(jobId);

        return Task.FromResult<HangfireTriggerResponse?>(new HangfireTriggerResponse(
            jobId,
            DateTime.UtcNow,
            _localizationService.GetLocalizedString("HangfireController.RecurringJobTriggered")));
    }

    public async Task<HangfireStatsResponse> GetStatsAsync()
    {
        var executions = _db.JobExecutionLogs
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        var failed = await executions.CountAsync(x => x.Status == "Failed").ConfigureAwait(false);
        var succeeded = await executions.CountAsync(x => x.Status == "Succeeded").ConfigureAwait(false);
        var queues = await executions
            .Where(x => !string.IsNullOrWhiteSpace(x.Queue))
            .Select(x => x.Queue!)
            .Distinct()
            .CountAsync()
            .ConfigureAwait(false);

        return new HangfireStatsResponse(
            Enqueued: 0,
            Processing: 0,
            Scheduled: 0,
            Succeeded: succeeded,
            Failed: failed,
            Deleted: 0,
            Servers: 0,
            Queues: queues,
            Timestamp: DateTime.UtcNow);
    }

    public async Task<HangfireLogPageResponse<HangfireFailureDto>> GetFailuresAsync(int from, int count)
    {
        var (skip, take) = NormalizeWindow(from, count);

        var items = await _db.JobFailureLogs
            .AsNoTracking()
            .OrderByDescending(x => x.FailedAt)
            .Skip(skip)
            .Take(take)
            .Select(x => new HangfireFailureDto(
                x.JobId,
                HangfireJobDisplayNameResolver.Resolve(null, x.JobName),
                x.JobName,
                x.FailedAt.ToString("o"),
                "Failed",
                x.ExceptionMessage ?? x.Reason,
                x.ExceptionType,
                x.RetryCount,
                x.Queue))
            .ToListAsync()
            .ConfigureAwait(false);

        var total = await _db.JobFailureLogs.CountAsync().ConfigureAwait(false);
        return new HangfireLogPageResponse<HangfireFailureDto>(items, total, DateTime.UtcNow);
    }

    public async Task<HangfireLogPageResponse<HangfireSuccessDto>> GetSuccessesAsync(int from, int count)
    {
        var (skip, take) = NormalizeWindow(from, count);
        var successQuery = _db.JobExecutionLogs
            .AsNoTracking()
            .Where(x => x.Status == "Succeeded");

        var items = await successQuery
            .OrderByDescending(x => x.FinishedAt)
            .Skip(skip)
            .Take(take)
            .Select(x => new HangfireSuccessDto(
                x.JobId,
                x.RecurringJobId,
                HangfireJobDisplayNameResolver.Resolve(x.RecurringJobId, x.JobName),
                x.JobName,
                x.FinishedAt.ToString("o"),
                x.DurationMs,
                x.Queue,
                x.RetryCount))
            .ToListAsync()
            .ConfigureAwait(false);

        var total = await successQuery.CountAsync().ConfigureAwait(false);
        return new HangfireLogPageResponse<HangfireSuccessDto>(items, total, DateTime.UtcNow);
    }

    public async Task<HangfireDeadLetterResponse> GetDeadLetterAsync(int from, int count)
    {
        var (skip, take) = NormalizeWindow(from, count);
        var deadLetterQuery = _db.JobFailureLogs
            .AsNoTracking()
            .Where(x => x.Queue == "dead-letter");

        var items = await deadLetterQuery
            .OrderByDescending(x => x.FailedAt)
            .Skip(skip)
            .Take(take)
            .Select(x => new HangfireDeadLetterDto(
                x.JobId,
                HangfireJobDisplayNameResolver.Resolve(null, x.JobName),
                x.JobName,
                x.FailedAt.ToString("o"),
                "Enqueued",
                x.ExceptionMessage ?? x.Reason))
            .ToListAsync()
            .ConfigureAwait(false);

        var total = await deadLetterQuery.CountAsync().ConfigureAwait(false);
        return new HangfireDeadLetterResponse("dead-letter", total, items, DateTime.UtcNow);
    }

    private static (int Skip, int Take) NormalizeWindow(int from, int count)
    {
        var skip = Math.Max(0, from);
        var take = count <= 0 ? DefaultCount : Math.Min(count, MaxCount);
        return (skip, take);
    }
}
