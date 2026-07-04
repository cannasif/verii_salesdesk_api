using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskInvoice : BaseEntity
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public SalesDeskInvoiceType InvoiceType { get; set; } = SalesDeskInvoiceType.Sales;
    public long CustomerId { get; set; }
    public SalesDeskCustomer? Customer { get; set; }
    public long? QuoteId { get; set; }
    public SalesDeskQuote? Quote { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public SalesDeskDocumentStatus Status { get; set; } = SalesDeskDocumentStatus.ToBeIssued;
    public decimal DiscountRate { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal SubTotal { get; set; }
    public decimal VatTotal { get; set; }
    public decimal GrandTotal { get; set; }
    public string? Notes { get; set; }

    public ICollection<SalesDeskInvoiceLine> Lines { get; set; } = new List<SalesDeskInvoiceLine>();
}
