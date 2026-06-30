using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Modules.AccessControl.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Modules.AccessControl.Api
{
    [ApiController]
    [Route("api/permission-definitions")]
    [Authorize]
    public class PermissionDefinitionController : PermissionProtectedControllerBase
    {
        private readonly IPermissionDefinitionService _permissionDefinitionService;

        public PermissionDefinitionController(
            IPermissionDefinitionService permissionDefinitionService,
            IPermissionAccessService permissionAccessService,
            ILocalizationService localizationService)
            : base(permissionAccessService, localizationService)
        {
            _permissionDefinitionService = permissionDefinitionService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResponse<PermissionDefinitionDto>>>> GetAll([FromQuery] PagedRequest request)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-definitions.view");
            if (forbidden != null) return forbidden;
            var result = await _permissionDefinitionService.GetAllAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("query")]
        public async Task<ActionResult<ApiResponse<PagedResponse<PermissionDefinitionDto>>>> Query([FromBody] PagedRequest? request)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-definitions.view");
            if (forbidden != null) return forbidden;
            var result = await _permissionDefinitionService.GetAllAsync(request ?? new PagedRequest());
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<PermissionDefinitionDto>>> GetById(long id)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-definitions.view");
            if (forbidden != null) return forbidden;
            var result = await _permissionDefinitionService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PermissionDefinitionDto>>> Create([FromBody] CreatePermissionDefinitionDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-definitions.create");
            if (forbidden != null) return forbidden;
            var result = await _permissionDefinitionService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id:long}")]
        [HttpPost("{id:long}/update")]
        public async Task<ActionResult<ApiResponse<PermissionDefinitionDto>>> Update(long id, [FromBody] UpdatePermissionDefinitionDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-definitions.update");
            if (forbidden != null) return forbidden;
            var result = await _permissionDefinitionService.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("sync")]
        public async Task<ActionResult<ApiResponse<PermissionDefinitionSyncResultDto>>> Sync([FromBody] SyncPermissionDefinitionsDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-definitions.update");
            if (forbidden != null) return forbidden;
            var result = await _permissionDefinitionService.SyncAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:long}")]
        [HttpPost("{id:long}/delete")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDelete(long id)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-definitions.delete");
            if (forbidden != null) return forbidden;
            var result = await _permissionDefinitionService.SoftDeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
