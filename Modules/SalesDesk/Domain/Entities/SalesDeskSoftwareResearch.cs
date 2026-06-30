using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskSoftwareResearch : BaseEntity
{
    public long? PotentialCustomerId { get; set; }
    public SalesDeskPotentialCustomer? PotentialCustomer { get; set; }
    public string Provider { get; set; } = "Netsis";
    public string? Keywords { get; set; }
    public string? Host { get; set; }
    public string? SourceUrl { get; set; }
    public int Score { get; set; }
    public SalesDeskPotentialStatus Status { get; set; } = SalesDeskPotentialStatus.Waiting;
    public DateTime? ResearchedAt { get; set; }
}
