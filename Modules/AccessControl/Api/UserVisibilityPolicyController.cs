using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Modules.AccessControl.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Modules.AccessControl.Api
{
    [ApiController]
    [Route("api/user-visibility-policies")]
    [Route("api/[controller]")]
    [Authorize]
    public class UserVisibilityPolicyController : PermissionProtectedControllerBase
    {
        private readonly IUserVisibilityPolicyService _service;

        public UserVisibilityPolicyController(
            IUserVisibilityPolicyService service,
            IPermissionAccessService permissionAccessService,
            ILocalizationService localizationService)
            : base(permissionAccessService, localizationService)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResponse<UserVisibilityPolicyDto>>>> GetAll([FromQuery] PagedRequest request)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-visibility-assignments.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetAllAsync(request).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("query")]
        public async Task<ActionResult<ApiResponse<PagedResponse<UserVisibilityPolicyDto>>>> Query([FromBody] PagedRequest? request)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-visibility-assignments.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetAllAsync(request ?? new PagedRequest()).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<UserVisibilityPolicyDto>>> GetById(long id)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-visibility-assignments.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetByIdAsync(id).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserVisibilityPolicyDto>>> Create([FromBody] CreateUserVisibilityPolicyDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-visibility-assignments.create");
            if (forbidden != null) return forbidden;
            var result = await _service.CreateAsync(dto).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id:long}")]
        [HttpPost("{id:long}/update")]
        public async Task<ActionResult<ApiResponse<UserVisibilityPolicyDto>>> Update(long id, [FromBody] UpdateUserVisibilityPolicyDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-visibility-assignments.update");
            if (forbidden != null) return forbidden;
            var result = await _service.UpdateAsync(id, dto).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-visibility-assignments.delete");
            if (forbidden != null) return forbidden;
            var result = await _service.DeleteAsync(id).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{id:long}/delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteByPost(long id)
        {
            var forbidden = await RequirePermissionAsync("access-control.user-visibility-assignments.delete");
            if (forbidden != null) return forbidden;
            var result = await _service.DeleteAsync(id).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }
    }
}
