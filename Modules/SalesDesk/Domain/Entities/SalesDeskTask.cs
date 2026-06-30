using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskTask : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? GroupName { get; set; }
    public long? CustomerId { get; set; }
    public SalesDeskCustomer? Customer { get; set; }
    public long? AssignedUserId { get; set; }
    public SalesDeskPriority Priority { get; set; } = SalesDeskPriority.Medium;
    public SalesDeskTaskStatus Status { get; set; } = SalesDeskTaskStatus.Open;
    public DateTime? DueDate { get; set; }
}
