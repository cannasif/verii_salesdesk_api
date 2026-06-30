using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskProduct : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string Unit { get; set; } = "Adet";
    public decimal SalesPrice { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal MinimumStockQuantity { get; set; }
    public string SearchText { get; set; } = string.Empty;

    public ICollection<SalesDeskProductCustomer> ProductCustomers { get; set; } = new List<SalesDeskProductCustomer>();
}
