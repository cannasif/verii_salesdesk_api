using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskPotentialCustomer : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public SalesDeskPotentialStatus Status { get; set; } = SalesDeskPotentialStatus.Waiting;
    public int MatchScore { get; set; }
    public DateTime? LastResearchDate { get; set; }
}
