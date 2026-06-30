using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.AccessControl.Api;
using salesdesk_api.Modules.AccessControl.Application.Services;
using salesdesk_api.Modules.NetsisIntegrations.Application.Dtos;
using salesdesk_api.Modules.NetsisIntegrations.Application.Services;
using salesdesk_api.Shared.Common.Application;
using salesdesk_api.Shared.Infrastructure.Services.Localization;

namespace salesdesk_api.Modules.NetsisIntegrations.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class NetsisReadController : PermissionProtectedControllerBase
{
    private readonly INetsisReadService _netsisReadService;

    public NetsisReadController(
        INetsisReadService netsisReadService,
        IPermissionAccessService permissionAccessService,
        ILocalizationService localizationService)
        : base(permissionAccessService, localizationService)
    {
        _netsisReadService = netsisReadService;
    }

    [HttpGet("getBranches")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<List<BranchDto>>>> GetBranches(
        [FromQuery] int? branchNo = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _netsisReadService.GetBranchesAsync(branchNo, cancellationToken).ConfigureAwait(false);
        return StatusCode(result.StatusCode, result);
    }
}
