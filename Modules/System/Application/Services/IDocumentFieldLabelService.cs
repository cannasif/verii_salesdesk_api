using salesdesk_api.Modules.System.Application.Dtos;

namespace salesdesk_api.Modules.System.Application.Services
{
    public interface IDocumentFieldLabelService
    {
        Task<ApiResponse<List<DocumentFieldLabelDto>>> GetAsync(string? documentType = null, string? scope = null);
        Task<ApiResponse<List<DocumentFieldLabelDto>>> UpdateAsync(UpdateDocumentFieldLabelsRequest request, long userId);
    }
}
