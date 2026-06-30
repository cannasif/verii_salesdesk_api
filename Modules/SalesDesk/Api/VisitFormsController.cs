using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/visit-forms")]
public sealed class VisitFormsController : ApiControllerBase
{
    public VisitFormsController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetVisitFormsAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskVisitFormUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateVisitFormAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskVisitFormUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateVisitFormAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteVisitFormAsync(id, cancellationToken);
        return Respond(result);
    }
}
