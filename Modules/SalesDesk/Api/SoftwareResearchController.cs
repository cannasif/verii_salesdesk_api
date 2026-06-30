using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/software-research")]
public sealed class SoftwareResearchController : ApiControllerBase
{
    public SoftwareResearchController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetSoftwareResearchesAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskSoftwareResearchUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateSoftwareResearchAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskSoftwareResearchUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateSoftwareResearchAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteSoftwareResearchAsync(id, cancellationToken);
        return Respond(result);
    }
}
