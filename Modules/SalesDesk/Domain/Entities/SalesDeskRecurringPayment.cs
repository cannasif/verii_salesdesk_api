using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskRecurringPayment : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public SalesDeskRecurringPaymentType Type { get; set; } = SalesDeskRecurringPaymentType.Expense;
    public string? Category { get; set; }
    public int DayOfMonth { get; set; }
    public decimal Amount { get; set; }
    public long? CustomerId { get; set; }
    public SalesDeskCustomer? Customer { get; set; }
    public bool IsActive { get; set; } = true;
}
