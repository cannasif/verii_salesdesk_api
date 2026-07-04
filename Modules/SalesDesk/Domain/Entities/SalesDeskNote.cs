using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskNote : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public long CreatedByUserId { get; set; }
    public string CreatedByName { get; set; } = string.Empty;

    public ICollection<SalesDeskNoteRecipient> Recipients { get; set; } = new List<SalesDeskNoteRecipient>();
}
