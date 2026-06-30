using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Services;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk")]
public sealed class OverviewController : ApiControllerBase
{
    public OverviewController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard(CancellationToken cancellationToken)
    {
        var result = await Service.GetDashboardAsync(cancellationToken);
        return Respond(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? q, [FromQuery] int take = 12, CancellationToken cancellationToken = default)
    {
        var result = await Service.SearchAsync(q, take, cancellationToken);
        return Respond(result);
    }
}
