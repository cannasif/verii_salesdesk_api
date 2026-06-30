using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using salesdesk_api.Modules.AccessControl.Api;

namespace salesdesk_api.Modules.Identity.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : PermissionProtectedControllerBase
    {
        private readonly IUserService _service;

        public UserController(
            IUserService service,
            IPermissionAccessService permissionAccessService,
            ILocalizationService localizationService)
            : base(permissionAccessService, localizationService)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagedRequest request)
        {
            var forbidden = await RequirePermissionAsync("users.user-management.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetAllUsersAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("query")]
        public async Task<IActionResult> Query([FromBody] PagedRequest? request)
        {
            var forbidden = await RequirePermissionAsync("users.user-management.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetAllUsersAsync(request ?? new PagedRequest());
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var forbidden = await RequirePermissionAsync("users.user-management.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetUserByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
        {
            var forbidden = await RequirePermissionAsync("users.user-management.create");
            if (forbidden != null) return forbidden;
            if (dto.PermissionGroupIds != null)
            {
                forbidden = await RequirePermissionAsync("access-control.user-group-assignments.update");
                if (forbidden != null) return forbidden;
            }
            var result = await _service.CreateUserAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateUserDto dto)
        {
            var forbidden = await RequirePermissionAsync("users.user-management.update");
            if (forbidden != null) return forbidden;
            if (dto.PermissionGroupIds != null)
            {
                forbidden = await RequirePermissionAsync("access-control.user-group-assignments.update");
                if (forbidden != null) return forbidden;
            }
            var result = await _service.UpdateUserAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var forbidden = await RequirePermissionAsync("users.user-management.delete");
            if (forbidden != null) return forbidden;
            var result = await _service.DeleteUserAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
