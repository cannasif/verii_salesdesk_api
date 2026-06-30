using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/tasks")]
public sealed class TasksController : ApiControllerBase
{
    public TasksController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetTasksAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpGet("open-items")]
    public async Task<IActionResult> OpenItems([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetOpenTasksAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskTaskUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateTaskAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskTaskUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateTaskAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteTaskAsync(id, cancellationToken);
        return Respond(result);
    }
}
