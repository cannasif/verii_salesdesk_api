namespace salesdesk_api.Modules.Audit.Application.Dtos;

public sealed class AuditLogDto
{
    public long Id { get; set; }
    public string TraceId { get; set; } = string.Empty;
    public string ActionType { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public string? FailureReason { get; set; }
    public string? BranchCode { get; set; }
    public string? RequestPath { get; set; }
    public string? RequestMethod { get; set; }
    public long? PerformedByUserId { get; set; }
    public string? PerformedByUserEmail { get; set; }
    public string? OldValuesJson { get; set; }
    public string? NewValuesJson { get; set; }
    public string? ChangedFieldsJson { get; set; }
    public IReadOnlyList<string> ChangedFields { get; set; } = Array.Empty<string>();
    public DateTime? CreatedDate { get; set; }
}
