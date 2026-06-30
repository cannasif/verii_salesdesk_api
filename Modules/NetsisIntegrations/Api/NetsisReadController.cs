using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.NetsisIntegrations.Application.Dtos;
using salesdesk_api.Modules.NetsisIntegrations.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.NetsisIntegrations.Api;

[ApiController]
[Route("api/[controller]")]
public sealed class NetsisReadController : ControllerBase
{
    private readonly INetsisReadService _netsisReadService;

    public NetsisReadController(INetsisReadService netsisReadService)
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
