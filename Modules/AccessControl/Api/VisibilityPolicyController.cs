using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Modules.AccessControl.Application.Services;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Modules.AccessControl.Api
{
    [ApiController]
    [Route("api/visibility-policies")]
    [Route("api/[controller]")]
    [Authorize]
    public class VisibilityPolicyController : PermissionProtectedControllerBase
    {
        private readonly IVisibilityPolicyService _service;
        private readonly IVisibilityAccessService _visibilityAccessService;
        private readonly ILocalizationService _localizationService;

        public VisibilityPolicyController(
            IVisibilityPolicyService service,
            IVisibilityAccessService visibilityAccessService,
            IPermissionAccessService permissionAccessService,
            ILocalizationService localizationService)
            : base(permissionAccessService, localizationService)
        {
            _service = service;
            _visibilityAccessService = visibilityAccessService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResponse<VisibilityPolicyDto>>>> GetAll([FromQuery] PagedRequest request)
        {
            var forbidden = await RequireAnyPermissionAsync(
                "access-control.visibility-policies.view",
                "access-control.user-visibility-assignments.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetAllAsync(request).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("query")]
        public async Task<ActionResult<ApiResponse<PagedResponse<VisibilityPolicyDto>>>> Query([FromBody] PagedRequest? request)
        {
            var forbidden = await RequireAnyPermissionAsync(
                "access-control.visibility-policies.view",
                "access-control.user-visibility-assignments.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetAllAsync(request ?? new PagedRequest()).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<VisibilityPolicyDto>>> GetById(long id)
        {
            var forbidden = await RequireAnyPermissionAsync(
                "access-control.visibility-policies.view",
                "access-control.user-visibility-assignments.view");
            if (forbidden != null) return forbidden;
            var result = await _service.GetByIdAsync(id).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<VisibilityPolicyDto>>> Create([FromBody] CreateVisibilityPolicyDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.visibility-policies.create");
            if (forbidden != null) return forbidden;
            var result = await _service.CreateAsync(dto).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id:long}")]
        [HttpPost("{id:long}/update")]
        public async Task<ActionResult<ApiResponse<VisibilityPolicyDto>>> Update(long id, [FromBody] UpdateVisibilityPolicyDto dto)
        {
            var forbidden = await RequirePermissionAsync("access-control.visibility-policies.update");
            if (forbidden != null) return forbidden;
            var result = await _service.UpdateAsync(id, dto).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:long}")]
        [HttpPost("{id:long}/delete")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id)
        {
            var forbidden = await RequirePermissionAsync("access-control.visibility-policies.delete");
            if (forbidden != null) return forbidden;
            var result = await _service.DeleteAsync(id).ConfigureAwait(false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("preview")]
        public async Task<ActionResult<ApiResponse<VisibilityPreviewResult>>> Preview([FromQuery] long userId, [FromQuery] string entityType)
        {
            var forbidden = await RequirePermissionAsync("access-control.visibility-simulator.view");
            if (forbidden != null) return forbidden;
            if (userId <= 0 || string.IsNullOrWhiteSpace(entityType))
            {
                var msg = _localizationService.GetLocalizedString("VisibilityPolicyController.UserIdEntityTypeRequired");
                var detail = _localizationService.GetLocalizedString("VisibilityPolicyController.PreviewValidationFailed");
                return ApiResponse<VisibilityPreviewResult>.ErrorResult(
                    msg,
                    detail,
                    StatusCodes.Status400BadRequest);
            }

            var result = await _visibilityAccessService.PreviewVisibilityAsync(userId, entityType.Trim()).ConfigureAwait(false);
            return ApiResponse<VisibilityPreviewResult>.SuccessResult(
                result,
                _localizationService.GetLocalizedString("VisibilityPolicyController.PreviewRetrieved"));
        }

        [HttpGet("simulate")]
        public async Task<ActionResult<ApiResponse<VisibilityActionSimulationResult>>> Simulate([FromQuery] long userId, [FromQuery] string entityType, [FromQuery] long entityId)
        {
            var forbidden = await RequirePermissionAsync("access-control.visibility-simulator.view");
            if (forbidden != null) return forbidden;
            if (userId <= 0 || entityId <= 0 || string.IsNullOrWhiteSpace(entityType))
            {
                var msg = _localizationService.GetLocalizedString("VisibilityPolicyController.SimulationParamsRequired");
                var detail = _localizationService.GetLocalizedString("VisibilityPolicyController.SimulationValidationFailed");
                return ApiResponse<VisibilityActionSimulationResult>.ErrorResult(
                    msg,
                    detail,
                    StatusCodes.Status400BadRequest);
            }

            var result = await _visibilityAccessService.SimulateRecordAccessAsync(userId, entityType.Trim(), entityId).ConfigureAwait(false);
            return ApiResponse<VisibilityActionSimulationResult>.SuccessResult(
                result,
                _localizationService.GetLocalizedString("VisibilityPolicyController.SimulationRetrieved"));
        }
    }
}
