using salesdesk_api.Modules.AccessControl.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Modules.AccessControl.Api
{
    public abstract class PermissionProtectedControllerBase : ControllerBase
    {
        private readonly IPermissionAccessService _permissionAccessService;
        private readonly ILocalizationService _localizationService;

        protected PermissionProtectedControllerBase(
            IPermissionAccessService permissionAccessService,
            ILocalizationService localizationService)
        {
            _permissionAccessService = permissionAccessService;
            _localizationService = localizationService;
        }

        protected async Task<ActionResult?> RequirePermissionAsync(string permissionCode)
        {
            if (await _permissionAccessService.HasPermissionAsync(permissionCode).ConfigureAwait(false))
            {
                return null;
            }

            return ForbiddenResult();
        }

        protected async Task<ActionResult?> RequireAnyPermissionAsync(params string[] permissionCodes)
        {
            foreach (var permissionCode in permissionCodes)
            {
                if (await _permissionAccessService.HasPermissionAsync(permissionCode).ConfigureAwait(false))
                {
                    return null;
                }
            }

            return ForbiddenResult();
        }

        private ActionResult ForbiddenResult()
        {
            var message = _localizationService.GetLocalizedString("General.Forbidden");
            return StatusCode(
                StatusCodes.Status403Forbidden,
                ApiResponse<object>.ErrorResult(
                    message,
                    message,
                    StatusCodes.Status403Forbidden,
                    errorCode: "forbidden"));
        }
    }
}
