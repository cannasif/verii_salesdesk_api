using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskQuoteLine : BaseEntity
{
    public long QuoteId { get; set; }
    public SalesDeskQuote? Quote { get; set; }
    public long ProductId { get; set; }
    public SalesDeskProduct? Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; }
    public decimal LineTotal { get; set; }
}
