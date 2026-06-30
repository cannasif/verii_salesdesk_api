using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Services;

namespace salesdesk_api.Modules.SalesDesk.Api;

[ApiController]
[Authorize]
public abstract class ApiControllerBase : ControllerBase
{
    protected ApiControllerBase(ISalesDeskService service)
    {
        Service = service;
    }

    protected ISalesDeskService Service { get; }

    protected IActionResult Respond<T>(T response)
        where T : class
    {
        var statusCode = response.GetType().GetProperty("StatusCode")?.GetValue(response) as int? ?? StatusCodes.Status200OK;
        return StatusCode(statusCode, response);
    }
}
