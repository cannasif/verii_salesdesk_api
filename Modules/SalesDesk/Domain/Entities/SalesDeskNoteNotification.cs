using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskNoteNotification : BaseEntity
{
    public long NoteId { get; set; }
    public SalesDeskNote? Note { get; set; }
    public long RecipientUserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public long CreatedByUserId { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime? DeliveredAt { get; set; }
}
