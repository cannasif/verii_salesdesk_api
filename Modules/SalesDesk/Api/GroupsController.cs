using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/groups")]
public sealed class GroupsController : ApiControllerBase
{
    public GroupsController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await Service.GetGroupsAsync(cancellationToken);
        return Respond(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var result = await Service.GetGroupAsync(id, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskGroupCreateDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateGroupAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskGroupUpdateDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateGroupAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}/members")]
    public async Task<IActionResult> SetMembers(long id, [FromBody] SalesDeskGroupMembersDto request, CancellationToken cancellationToken)
    {
        var result = await Service.SetGroupMembersAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteGroupAsync(id, cancellationToken);
        return Respond(result);
    }
}
