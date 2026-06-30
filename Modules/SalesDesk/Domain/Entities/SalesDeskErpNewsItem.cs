using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskErpNewsItem : BaseEntity
{
    public string Topic { get; set; } = "Netsis";
    public string Title { get; set; } = string.Empty;
    public string? Source { get; set; }
    public string? SourceUrl { get; set; }
    public int Score { get; set; }
    public bool IsCritical { get; set; }
    public bool IsRead { get; set; }
    public DateTime PublishedAt { get; set; }
}
