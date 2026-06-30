using salesdesk_api.Modules.AccessControl.Application.Dtos;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public interface IUserPermissionGroupService
    {
        Task<ApiResponse<UserPermissionGroupDto>> GetByUserIdAsync(long userId);
        Task<ApiResponse<UserPermissionGroupDto>> SetUserGroupsAsync(long userId, SetUserPermissionGroupsDto dto);
    }
}
