using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using salesdesk_api.Shared.Domain.Entities;
using salesdesk_api.Shared.Host.WebApi.Telemetry;

namespace salesdesk_api.Shared.Infrastructure.Services.Auditing;

public sealed class AuditLogWriter : IAuditLogWriter
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly SalesDeskDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuditLogWriter> _logger;

    public AuditLogWriter(
        SalesDeskDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuditLogWriter> logger)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task WriteAsync(AuditLogWriteEntry entry, CancellationToken cancellationToken = default)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        using var activity = SalesDeskTelemetry.ActivitySource.StartActivity("audit.log.write", System.Diagnostics.ActivityKind.Internal);
        activity?.SetTag("salesdesk.audit.action_type", entry.ActionType);
        activity?.SetTag("salesdesk.audit.entity_type", entry.EntityType);
        activity?.SetTag("salesdesk.audit.entity_id", entry.EntityId);
        activity?.SetTag("salesdesk.audit.result", entry.Result);
        activity?.SetTag("salesdesk.audit.source", entry.Source);

        var auditLog = new AuditLog
        {
            TraceId = System.Diagnostics.Activity.Current?.TraceId.ToString() ?? httpContext?.TraceIdentifier ?? string.Empty,
            ActionType = entry.ActionType,
            EntityType = entry.EntityType,
            EntityId = entry.EntityId,
            Result = entry.Result,
            Source = entry.Source,
            Reason = entry.Reason,
            FailureReason = entry.FailureReason,
            BranchCode = httpContext?.Items["BranchCode"]?.ToString(),
            RequestPath = httpContext?.Request.Path.Value,
            RequestMethod = httpContext?.Request.Method,
            PerformedByUserId = ResolveUserId(httpContext),
            PerformedByUserEmail = httpContext?.User.FindFirst(ClaimTypes.Email)?.Value,
            OldValuesJson = Serialize(entry.OldValues),
            NewValuesJson = Serialize(entry.NewValues),
            ChangedFieldsJson = Serialize(entry.ChangedFields),
            CreatedDate = DateTimeProvider.Now,
            CreatedBy = ResolveUserId(httpContext),
            IsDeleted = false
        };

        try
        {
            await _dbContext.AuditLogs.AddAsync(auditLog, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            SalesDeskTelemetry.RecordAuditWrite(entry.ActionType, entry.EntityType, entry.Result, entry.Source);
        }
        catch (Exception ex)
        {
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Error, ex.GetType().Name);
            _logger.LogError(
                ex,
                "Audit log write failed for {EntityType}/{EntityId} action {ActionType}.",
                entry.EntityType,
                entry.EntityId,
                entry.ActionType);
        }
    }

    private static long? ResolveUserId(HttpContext? httpContext)
    {
        var userIdValue = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return long.TryParse(userIdValue, out var userId) ? userId : null;
    }

    private static string? Serialize(object? value)
    {
        if (value == null)
        {
            return null;
        }

        return JsonSerializer.Serialize(value, SerializerOptions);
    }
}
