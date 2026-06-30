using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskCustomer : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public SalesDeskCustomerKind Kind { get; set; } = SalesDeskCustomerKind.Customer;
    public decimal Balance { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }

    public ICollection<SalesDeskQuote> Quotes { get; set; } = new List<SalesDeskQuote>();
    public ICollection<SalesDeskInvoice> Invoices { get; set; } = new List<SalesDeskInvoice>();
}
