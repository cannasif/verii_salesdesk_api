using salesdesk_api.Modules.AccessControl.Application.Dtos;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public interface IVisibilityPolicyService
    {
        Task<ApiResponse<PagedResponse<VisibilityPolicyDto>>> GetAllAsync(PagedRequest request);
        Task<ApiResponse<VisibilityPolicyDto>> GetByIdAsync(long id);
        Task<ApiResponse<VisibilityPolicyDto>> CreateAsync(CreateVisibilityPolicyDto dto);
        Task<ApiResponse<VisibilityPolicyDto>> UpdateAsync(long id, UpdateVisibilityPolicyDto dto);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
