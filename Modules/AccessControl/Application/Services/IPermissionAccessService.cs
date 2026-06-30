using salesdesk_api.Modules.AccessControl.Application.Dtos;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public interface IPermissionAccessService
    {
        Task<ApiResponse<MyPermissionsDto>> GetMyPermissionsAsync();
        Task<bool> HasPermissionAsync(string permissionCode);
    }
}
