using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskInvoiceLine : BaseEntity
{
    public long InvoiceId { get; set; }
    public SalesDeskInvoice? Invoice { get; set; }
    public long ProductId { get; set; }
    public SalesDeskProduct? Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; }
    public decimal LineTotal { get; set; }
}
