using System.Text.Json;
using salesdesk_api.Helpers;
using salesdesk_api.Modules.Audit.Application.Dtos;
using salesdesk_api.Shared.Domain.Entities;
using salesdesk_api.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.Audit.Application.Services;

public sealed class AuditLogQueryService : IAuditLogQueryService
{
    private static readonly string[] SearchableColumns =
    [
        nameof(AuditLog.TraceId),
        nameof(AuditLog.ActionType),
        nameof(AuditLog.EntityType),
        nameof(AuditLog.EntityId),
        nameof(AuditLog.Result),
        nameof(AuditLog.Source),
        nameof(AuditLog.PerformedByUserEmail),
        nameof(AuditLog.BranchCode),
        nameof(AuditLog.RequestPath)
    ];

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILocalizationService _localizationService;

    public AuditLogQueryService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
    {
        _unitOfWork = unitOfWork;
        _localizationService = localizationService;
    }

    public async Task<ApiResponse<PagedResponse<AuditLogDto>>> GetPagedAsync(PagedRequest request)
    {
        try
        {
            request ??= new PagedRequest();
            request.Filters ??= new List<Filter>();

            var columnMapping = BuildColumnMapping();
            var query = _unitOfWork.Repository<AuditLog>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ApplySearch(request.Search, SearchableColumns)
                .ApplyFilters(request.Filters, request.FilterLogic, columnMapping)
                .ApplySorting(request.SortBy ?? nameof(AuditLog.CreatedDate), request.SortDirection, columnMapping);

            var page = await query.ToPagedItemsAsync(request).ConfigureAwait(false);
            var response = page.ToPagedResponse(MapToDto);

            return ApiResponse<PagedResponse<AuditLogDto>>.SuccessResult(
                response,
                _localizationService.GetLocalizedString("General.OperationSuccessful"));
        }
        catch (Exception ex)
        {
            return ApiResponse<PagedResponse<AuditLogDto>>.ErrorResult(
                _localizationService.GetLocalizedString("General.InternalServerError"),
                ex.Message,
                StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ApiResponse<AuditLogDto>> GetByIdAsync(long id)
    {
        try
        {
            var entity = await _unitOfWork.Repository<AuditLog>()
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                .ConfigureAwait(false);

            if (entity == null)
            {
                return ApiResponse<AuditLogDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.ValidationError"),
                    _localizationService.GetLocalizedString("General.ValidationError"),
                    StatusCodes.Status404NotFound);
            }

            return ApiResponse<AuditLogDto>.SuccessResult(
                MapToDto(entity),
                _localizationService.GetLocalizedString("General.OperationSuccessful"));
        }
        catch (Exception ex)
        {
            return ApiResponse<AuditLogDto>.ErrorResult(
                _localizationService.GetLocalizedString("General.InternalServerError"),
                ex.Message,
                StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ApiResponse<PagedResponse<AuditLogDto>>> GetByTraceIdAsync(string traceId, PagedRequest request)
    {
        try
        {
            request ??= new PagedRequest();
            request.Filters ??= new List<Filter>();

            var columnMapping = BuildColumnMapping();
            var query = _unitOfWork.Repository<AuditLog>()
                .Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted && x.TraceId == traceId)
                .ApplySearch(request.Search, SearchableColumns)
                .ApplyFilters(request.Filters, request.FilterLogic, columnMapping)
                .ApplySorting(request.SortBy ?? nameof(AuditLog.CreatedDate), request.SortDirection, columnMapping);

            var page = await query.ToPagedItemsAsync(request).ConfigureAwait(false);


            var items = page.Items;

            var response = new PagedResponse<AuditLogDto>
            {
                Items = items.Select(MapToDto).ToList(),
                TotalCount = page.TotalCount,
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            };

            return ApiResponse<PagedResponse<AuditLogDto>>.SuccessResult(
                response,
                _localizationService.GetLocalizedString("General.OperationSuccessful"));
        }
        catch (Exception ex)
        {
            return ApiResponse<PagedResponse<AuditLogDto>>.ErrorResult(
                _localizationService.GetLocalizedString("General.InternalServerError"),
                ex.Message,
                StatusCodes.Status500InternalServerError);
        }
    }

    private static AuditLogDto MapToDto(AuditLog entity)
    {
        return new AuditLogDto
        {
            Id = entity.Id,
            TraceId = entity.TraceId,
            ActionType = entity.ActionType,
            EntityType = entity.EntityType,
            EntityId = entity.EntityId,
            Result = entity.Result,
            Source = entity.Source,
            Reason = entity.Reason,
            FailureReason = entity.FailureReason,
            BranchCode = entity.BranchCode,
            RequestPath = entity.RequestPath,
            RequestMethod = entity.RequestMethod,
            PerformedByUserId = entity.PerformedByUserId,
            PerformedByUserEmail = entity.PerformedByUserEmail,
            OldValuesJson = entity.OldValuesJson,
            NewValuesJson = entity.NewValuesJson,
            ChangedFieldsJson = entity.ChangedFieldsJson,
            ChangedFields = DeserializeChangedFields(entity.ChangedFieldsJson),
            CreatedDate = entity.CreatedDate
        };
    }

    private static IReadOnlyList<string> DeserializeChangedFields(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return Array.Empty<string>();
        }

        try
        {
            var items = JsonSerializer.Deserialize<List<string>>(json);
            return items?.ToArray() ?? Array.Empty<string>();
        }
        catch
        {
            return Array.Empty<string>();
        }
    }

    private static Dictionary<string, string> BuildColumnMapping()
    {
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["traceId"] = nameof(AuditLog.TraceId),
            ["actionType"] = nameof(AuditLog.ActionType),
            ["entityType"] = nameof(AuditLog.EntityType),
            ["entityId"] = nameof(AuditLog.EntityId),
            ["result"] = nameof(AuditLog.Result),
            ["source"] = nameof(AuditLog.Source),
            ["branchCode"] = nameof(AuditLog.BranchCode),
            ["requestPath"] = nameof(AuditLog.RequestPath),
            ["requestMethod"] = nameof(AuditLog.RequestMethod),
            ["performedByUserId"] = nameof(AuditLog.PerformedByUserId),
            ["performedByUserEmail"] = nameof(AuditLog.PerformedByUserEmail),
            ["createdDate"] = nameof(AuditLog.CreatedDate)
        };
    }
}
