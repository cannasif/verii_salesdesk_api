using salesdesk_api.Modules.AccessControl.Application.Dtos;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public interface IUserVisibilityPolicyService
    {
        Task<ApiResponse<PagedResponse<UserVisibilityPolicyDto>>> GetAllAsync(PagedRequest request);
        Task<ApiResponse<UserVisibilityPolicyDto>> GetByIdAsync(long id);
        Task<ApiResponse<UserVisibilityPolicyDto>> CreateAsync(CreateUserVisibilityPolicyDto dto);
        Task<ApiResponse<UserVisibilityPolicyDto>> UpdateAsync(long id, UpdateUserVisibilityPolicyDto dto);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
