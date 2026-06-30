using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/erp-news")]
public sealed class ErpNewsController : ApiControllerBase
{
    public ErpNewsController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetErpNewsAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskErpNewsItemUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateErpNewsAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskErpNewsItemUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateErpNewsAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteErpNewsAsync(id, cancellationToken);
        return Respond(result);
    }
}
