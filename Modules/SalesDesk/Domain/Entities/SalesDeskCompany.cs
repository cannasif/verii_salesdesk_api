using salesdesk_api.Shared.Domain.Entities.Common;

namespace salesdesk_api.Modules.SalesDesk.Domain.Entities;

public class SalesDeskCompany : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string IpUsername { get; set; } = string.Empty;
    public string IpPassword { get; set; } = string.Empty;
    public string VpnName { get; set; } = string.Empty;
    public string VpnUsername { get; set; } = string.Empty;
    public string VpnPassword { get; set; } = string.Empty;
    public string VpnIpAddress { get; set; } = string.Empty;
    public string VpnPort { get; set; } = string.Empty;
    public string DatabaseUsername { get; set; } = string.Empty;
    public string DatabasePassword { get; set; } = string.Empty;
    public string LoginUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Description1 { get; set; } = string.Empty;
}
