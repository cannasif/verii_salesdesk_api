using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskFixedAsset : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Value { get; set; }
    public SalesDeskFixedAssetStatus Status { get; set; } = SalesDeskFixedAssetStatus.Active;
}
