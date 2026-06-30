using salesdesk_api.Modules.Audit.Application.Dtos;

namespace salesdesk_api.Modules.Audit.Application.Services;

public interface IAuditLogQueryService
{
    Task<ApiResponse<PagedResponse<AuditLogDto>>> GetPagedAsync(PagedRequest request);
    Task<ApiResponse<AuditLogDto>> GetByIdAsync(long id);
    Task<ApiResponse<PagedResponse<AuditLogDto>>> GetByTraceIdAsync(string traceId, PagedRequest request);
}
