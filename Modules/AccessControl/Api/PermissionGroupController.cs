using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Modules.AccessControl.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Modules.AccessControl.Api
{
    [ApiController]
    [Route("api/permission-groups")]
    [Authorize]
    public class PermissionGroupController : PermissionProtectedControllerBase
    {
        private readonly IPermissionGroupService _permissionGroupService;

        public PermissionGroupController(
            IPermissionGroupService permissionGroupService,
            IPermissionAccessService permissionAccessService,
            ILocalizationService localizationService)
            : base(permissionAccessService, localizationService)
        {
            _permissionGroupService = permissionGroupService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResponse<PermissionGroupDto>>>> GetAll([FromQuery] PagedRequest request)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-groups.view");
            if (forbidden != null) return forbidden;
            var result = await _permissionGroupService.GetAllAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("query")]
        public async Task<ActionResult<ApiResponse<PagedResponse<PermissionGroupDto>>>> Query([FromBody] PagedRequest? request)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-groups.view");
            if (forbidden != null) return forbidden;
            var result = await _permissionGroupService.GetAllAsync(request ?? new PagedRequest());
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<PermissionGroupDto>>> GetById(long id)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-groups.view");
            if (forbidden != null) return forbidden;
            var result = await _permissionGroupService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PermissionGroupDto>>> Create([FromBody] CreatePermissionGroupDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-groups.create");
            if (forbidden != null) return forbidden;
            var result = await _permissionGroupService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id:long}")]
        [HttpPost("{id:long}/update")]
        public async Task<ActionResult<ApiResponse<PermissionGroupDto>>> Update(long id, [FromBody] UpdatePermissionGroupDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-groups.update");
            if (forbidden != null) return forbidden;
            var result = await _permissionGroupService.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id:long}/permissions")]
        [HttpPost("{id:long}/permissions/update")]
        public async Task<ActionResult<ApiResponse<PermissionGroupDto>>> SetPermissions(long id, [FromBody] SetPermissionGroupPermissionsDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-groups.update");
            if (forbidden != null) return forbidden;
            var result = await _permissionGroupService.SetPermissionsAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:long}")]
        [HttpPost("{id:long}/delete")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDelete(long id)
        {
            var forbidden = await RequirePermissionAsync("access-control.permission-groups.delete");
            if (forbidden != null) return forbidden;
            var result = await _permissionGroupService.SoftDeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
