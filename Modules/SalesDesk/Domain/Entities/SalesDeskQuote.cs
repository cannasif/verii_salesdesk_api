using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskQuote : BaseEntity
{
    public string QuoteNumber { get; set; } = string.Empty;
    public long CustomerId { get; set; }
    public SalesDeskCustomer? Customer { get; set; }
    public DateTime QuoteDate { get; set; }
    public SalesDeskDocumentStatus Status { get; set; } = SalesDeskDocumentStatus.Draft;
    public decimal SubTotal { get; set; }
    public decimal VatTotal { get; set; }
    public decimal GrandTotal { get; set; }
    public string? Notes { get; set; }

    public ICollection<SalesDeskQuoteLine> Lines { get; set; } = new List<SalesDeskQuoteLine>();
}
