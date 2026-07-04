using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/notes")]
public sealed class NotesController : ApiControllerBase
{
    public NotesController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] long userId, CancellationToken cancellationToken)
    {
        var result = await Service.GetNotesForUserAsync(userId, cancellationToken);
        return Respond(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var result = await Service.GetNoteAsync(id, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskNoteCreateDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateNoteAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskNoteUpdateDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateNoteAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteNoteAsync(id, cancellationToken);
        return Respond(result);
    }

    [HttpGet("notifications/pending")]
    public async Task<IActionResult> PendingNotifications([FromQuery] long userId, CancellationToken cancellationToken)
    {
        var result = await Service.PullPendingNoteNotificationsAsync(userId, cancellationToken);
        return Respond(result);
    }

    [HttpPost("notifications/{id:long}/ack")]
    public async Task<IActionResult> AcknowledgeNotification(long id, CancellationToken cancellationToken)
    {
        var result = await Service.AcknowledgeNoteNotificationAsync(id, cancellationToken);
        return Respond(result);
    }
}
