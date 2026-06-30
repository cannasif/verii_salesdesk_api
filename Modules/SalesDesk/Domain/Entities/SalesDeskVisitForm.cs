using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskVisitForm : BaseEntity
{
    public long? VisitId { get; set; }
    public SalesDeskVisit? Visit { get; set; }
    public long? CustomerId { get; set; }
    public SalesDeskCustomer? Customer { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime FormDate { get; set; }
    public string? Content { get; set; }
    public long? OwnerUserId { get; set; }
}
