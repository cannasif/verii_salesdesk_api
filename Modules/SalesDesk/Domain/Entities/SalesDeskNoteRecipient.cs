using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskNoteRecipient : BaseEntity
{
    public long NoteId { get; set; }
    public SalesDeskNote? Note { get; set; }
    public long UserId { get; set; }
}
