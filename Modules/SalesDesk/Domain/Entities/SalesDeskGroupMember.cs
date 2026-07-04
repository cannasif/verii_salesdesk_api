using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskGroupMember : BaseEntity
{
    public long GroupId { get; set; }
    public SalesDeskGroup? Group { get; set; }
    public long UserId { get; set; }
}
