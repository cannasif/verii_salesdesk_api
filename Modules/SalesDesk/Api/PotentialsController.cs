using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/potentials")]
public sealed class PotentialsController : ApiControllerBase
{
    public PotentialsController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetPotentialCustomersAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] PagedRequest? request, CancellationToken cancellationToken)
    {
        var result = await Service.GetPotentialCustomersAsync(request ?? new PagedRequest(), cancellationToken);
        return Respond(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var result = await Service.GetPotentialCustomerAsync(id, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskPotentialCustomerUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreatePotentialCustomerAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskPotentialCustomerUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdatePotentialCustomerAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeletePotentialCustomerAsync(id, cancellationToken);
        return Respond(result);
    }
}
