using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Modules.AccessControl.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Modules.AccessControl.Api
{
    [ApiController]
    [Route("api/user-permission-groups")]
    [Authorize]
    public class UserPermissionGroupController : PermissionProtectedControllerBase
    {
        private readonly IUserPermissionGroupService _userPermissionGroupService;

        public UserPermissionGroupController(
            IUserPermissionGroupService userPermissionGroupService,
            IPermissionAccessService permissionAccessService,
            ILocalizationService localizationService)
            : base(permissionAccessService, localizationService)
        {
            _userPermissionGroupService = userPermissionGroupService;
        }

        [HttpGet("{userId:long}")]
        public async Task<ActionResult<ApiResponse<UserPermissionGroupDto>>> GetByUserId(long userId)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-group-assignments.view");
            if (forbidden != null) return forbidden;
            var result = await _userPermissionGroupService.GetByUserIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{userId:long}")]
        [HttpPost("{userId:long}/update")]
        public async Task<ActionResult<ApiResponse<UserPermissionGroupDto>>> SetUserGroups(long userId, [FromBody] SetUserPermissionGroupsDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-group-assignments.update");
            if (forbidden != null) return forbidden;
            var result = await _userPermissionGroupService.SetUserGroupsAsync(userId, dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
