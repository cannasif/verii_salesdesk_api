using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskProductCustomer : BaseEntity
{
    public long ProductId { get; set; }
    public SalesDeskProduct? Product { get; set; }

    public long? CustomerId { get; set; }
    public SalesDeskCustomer? Customer { get; set; }

    public long? PotentialCustomerId { get; set; }
    public SalesDeskPotentialCustomer? PotentialCustomer { get; set; }
}
