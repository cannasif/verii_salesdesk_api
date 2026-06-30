using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/invoices")]
public sealed class InvoicesController : ApiControllerBase
{
    public InvoicesController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetInvoicesAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var result = await Service.GetInvoiceAsync(id, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskInvoiceUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateInvoiceAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskInvoiceUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateInvoiceAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteInvoiceAsync(id, cancellationToken);
        return Respond(result);
    }
}
