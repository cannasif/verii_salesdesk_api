using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskVisit : BaseEntity
{
    public DateTime VisitDate { get; set; }
    public TimeSpan? VisitTime { get; set; }
    public string Title { get; set; } = string.Empty;
    public long? CustomerId { get; set; }
    public SalesDeskCustomer? Customer { get; set; }
    public string VisitType { get; set; } = "Yuz Yuze";
    public SalesDeskVisitStatus Status { get; set; } = SalesDeskVisitStatus.Planned;
    public string? Notes { get; set; }
}
