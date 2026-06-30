using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskGmailMessage : BaseEntity
{
    public string GmailMessageId { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string? Preview { get; set; }
    public DateTime ReceivedAt { get; set; }
    public bool IsUnread { get; set; }
    public bool IsMeeting { get; set; }
    public string? ThreadId { get; set; }
}
