using salesdesk_api.Modules.NetsisIntegrations.Application.Dtos;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.NetsisIntegrations.Application.Services;

public interface INetsisReadService
{
    Task<ApiResponse<List<BranchDto>>> GetBranchesAsync(int? branchNo = null, CancellationToken cancellationToken = default);
}
