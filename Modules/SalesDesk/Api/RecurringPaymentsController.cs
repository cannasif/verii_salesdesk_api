using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/recurring-payments")]
public sealed class RecurringPaymentsController : ApiControllerBase
{
    public RecurringPaymentsController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetRecurringPaymentsAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskRecurringPaymentUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateRecurringPaymentAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskRecurringPaymentUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateRecurringPaymentAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteRecurringPaymentAsync(id, cancellationToken);
        return Respond(result);
    }
}
