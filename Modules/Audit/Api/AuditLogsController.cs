using salesdesk_api.Modules.AccessControl.Api;
using salesdesk_api.Modules.Audit.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Modules.Audit.Api;

[ApiController]
[Route("api/audit-logs")]
[Authorize]
public sealed class AuditLogsController : PermissionProtectedControllerBase
{
    private readonly IAuditLogQueryService _auditLogQueryService;

    public AuditLogsController(
        IAuditLogQueryService auditLogQueryService,
        IPermissionAccessService permissionAccessService,
        ILocalizationService localizationService)
        : base(permissionAccessService, localizationService)
    {
        _auditLogQueryService = auditLogQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PagedRequest request)
    {
        var forbidden = await RequirePermissionAsync("access-control.audit-logs.view");
        if (forbidden != null) return forbidden;
        var result = await _auditLogQueryService.GetPagedAsync(request).ConfigureAwait(false);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] PagedRequest? request)
    {
        var forbidden = await RequirePermissionAsync("access-control.audit-logs.view");
        if (forbidden != null) return forbidden;
        var result = await _auditLogQueryService.GetPagedAsync(request ?? new PagedRequest()).ConfigureAwait(false);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var forbidden = await RequirePermissionAsync("access-control.audit-logs.view");
        if (forbidden != null) return forbidden;
        var result = await _auditLogQueryService.GetByIdAsync(id).ConfigureAwait(false);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("trace/{traceId}")]
    public async Task<IActionResult> GetByTraceId(string traceId, [FromQuery] PagedRequest request)
    {
        var forbidden = await RequirePermissionAsync("access-control.audit-logs.view");
        if (forbidden != null) return forbidden;
        var result = await _auditLogQueryService.GetByTraceIdAsync(traceId, request).ConfigureAwait(false);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("trace/{traceId}/query")]
    public async Task<IActionResult> QueryByTraceId(string traceId, [FromBody] PagedRequest? request)
    {
        var forbidden = await RequirePermissionAsync("access-control.audit-logs.view");
        if (forbidden != null) return forbidden;
        var result = await _auditLogQueryService.GetByTraceIdAsync(traceId, request ?? new PagedRequest()).ConfigureAwait(false);
        return StatusCode(result.StatusCode, result);
    }
}
