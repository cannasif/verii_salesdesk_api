using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Modules.AccessControl.Domain.Entities;
using salesdesk_api.Modules.AccessControl.Domain.Enums;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public class VisibilityPolicyService : IVisibilityPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizationService _localizationService;
        private readonly IAccessControlRefreshNotifier _accessControlRefreshNotifier;
        private readonly IAuditLogWriter _auditLogWriter;

        public VisibilityPolicyService(
            IUnitOfWork unitOfWork,
            ILocalizationService localizationService,
            IAccessControlRefreshNotifier accessControlRefreshNotifier,
            IAuditLogWriter auditLogWriter)
        {
            _unitOfWork = unitOfWork;
            _localizationService = localizationService;
            _accessControlRefreshNotifier = accessControlRefreshNotifier;
            _auditLogWriter = auditLogWriter;
        }

        public async Task<ApiResponse<PagedResponse<VisibilityPolicyDto>>> GetAllAsync(PagedRequest request)
        {
            try
            {
                request ??= new PagedRequest();
                request.Filters ??= new List<Filter>();

                var query = _unitOfWork.VisibilityPolicies.Query()
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .ApplySearch(request.Search, [nameof(VisibilityPolicy.Code), nameof(VisibilityPolicy.Name), nameof(VisibilityPolicy.EntityType)])
                    .ApplyFilters(request.Filters, request.FilterLogic);

                query = query.ApplySorting(request.SortBy ?? nameof(VisibilityPolicy.Id), request.SortDirection);

                var page = await query.ToPagedItemsAsync(request).ConfigureAwait(false);


                var items = page.Items;

                var dtos = items.Select(MapDto).ToList();
                return ApiResponse<PagedResponse<VisibilityPolicyDto>>.SuccessResult(
                    new PagedResponse<VisibilityPolicyDto>
                    {
                        Items = dtos,
                        TotalCount = page.TotalCount,
                        PageNumber = page.PageNumber,
                        PageSize = page.PageSize
                    },
                    _localizationService.GetLocalizedString("VisibilityPolicyService.ListRetrieved"));
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResponse<VisibilityPolicyDto>>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<VisibilityPolicyDto>> GetByIdAsync(long id)
        {
            try
            {
                var entity = await _unitOfWork.VisibilityPolicies.Query()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                    .ConfigureAwait(false);

                if (entity == null)
                {
                    var msg = _localizationService.GetLocalizedString("VisibilityPolicyService.NotFound");
                    return ApiResponse<VisibilityPolicyDto>.ErrorResult(msg, msg, StatusCodes.Status404NotFound);
                }

                return ApiResponse<VisibilityPolicyDto>.SuccessResult(
                    MapDto(entity),
                    _localizationService.GetLocalizedString("VisibilityPolicyService.Retrieved"));
            }
            catch (Exception ex)
            {
                return ApiResponse<VisibilityPolicyDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<VisibilityPolicyDto>> CreateAsync(CreateVisibilityPolicyDto dto)
        {
            try
            {
                var validation = ValidateScopeType(dto.ScopeType);
                if (validation != null)
                {
                    return validation;
                }

                var normalizedEntityType = VisibilityAccessService.NormalizeEntityType(dto.EntityType);
                if (!VisibilityAccessService.IsSupportedEntityType(normalizedEntityType))
                {
                    var msg = _localizationService.GetLocalizedString("VisibilityPolicyService.InvalidEntityType");
                    return ApiResponse<VisibilityPolicyDto>.ErrorResult(msg, msg, StatusCodes.Status400BadRequest);
                }

                var exists = await _unitOfWork.VisibilityPolicies.Query()
                    .AsNoTracking()
                    .AnyAsync(x => !x.IsDeleted && x.Code == dto.Code)
                    .ConfigureAwait(false);

                if (exists)
                {
                    var msg = _localizationService.GetLocalizedString("VisibilityPolicyService.CodeAlreadyExists");
                    return ApiResponse<VisibilityPolicyDto>.ErrorResult(msg, msg, StatusCodes.Status400BadRequest);
                }

                var entity = new VisibilityPolicy
                {
                    Code = dto.Code.Trim(),
                    Name = dto.Name.Trim(),
                    EntityType = normalizedEntityType,
                    Description = dto.Description?.Trim(),
                    ScopeType = (VisibilityScopeType)dto.ScopeType,
                    IncludeSelf = dto.IncludeSelf,
                    IsActive = dto.IsActive
                };

                await _unitOfWork.VisibilityPolicies.AddAsync(entity).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "visibility-policy.create",
                    "VisibilityPolicy",
                    entity.Id.ToString(),
                    "Succeeded",
                    "access-control",
                    NewValues: new
                    {
                        entity.Code,
                        entity.Name,
                        entity.EntityType,
                        entity.Description,
                        ScopeType = entity.ScopeType.ToString(),
                        entity.IncludeSelf,
                        entity.IsActive
                    },
                    ChangedFields: ["Code", "Name", "EntityType", "Description", "ScopeType", "IncludeSelf", "IsActive"])).ConfigureAwait(false);

                return ApiResponse<VisibilityPolicyDto>.SuccessResult(
                    MapDto(entity),
                    _localizationService.GetLocalizedString("VisibilityPolicyService.Created"));
            }
            catch (Exception ex)
            {
                return ApiResponse<VisibilityPolicyDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<VisibilityPolicyDto>> UpdateAsync(long id, UpdateVisibilityPolicyDto dto)
        {
            try
            {
                var entity = await _unitOfWork.VisibilityPolicies.GetByIdForUpdateAsync(id).ConfigureAwait(false);
                if (entity == null || entity.IsDeleted)
                {
                    var msg = _localizationService.GetLocalizedString("VisibilityPolicyService.NotFound");
                    return ApiResponse<VisibilityPolicyDto>.ErrorResult(msg, msg, StatusCodes.Status404NotFound);
                }

                if (dto.ScopeType.HasValue && !Enum.IsDefined(typeof(VisibilityScopeType), dto.ScopeType.Value))
                {
                    var inv = _localizationService.GetLocalizedString("VisibilityPolicyService.InvalidScopeType");
                    return ApiResponse<VisibilityPolicyDto>.ErrorResult(inv, inv, StatusCodes.Status400BadRequest);
                }

                if (!string.IsNullOrWhiteSpace(dto.Code) && !dto.Code.Equals(entity.Code, StringComparison.Ordinal))
                {
                    var exists = await _unitOfWork.VisibilityPolicies.Query()
                        .AsNoTracking()
                        .AnyAsync(x => !x.IsDeleted && x.Id != id && x.Code == dto.Code)
                        .ConfigureAwait(false);

                    if (exists)
                    {
                        var msg = _localizationService.GetLocalizedString("VisibilityPolicyService.CodeAlreadyExists");
                        return ApiResponse<VisibilityPolicyDto>.ErrorResult(msg, msg, StatusCodes.Status400BadRequest);
                    }
                }

                var oldValues = new
                {
                    entity.Code,
                    entity.Name,
                    entity.EntityType,
                    entity.Description,
                    ScopeType = entity.ScopeType.ToString(),
                    entity.IncludeSelf,
                    entity.IsActive
                };
                var changedFields = new List<string>();

                if (!string.IsNullOrWhiteSpace(dto.Code)) { entity.Code = dto.Code.Trim(); changedFields.Add("Code"); }
                if (!string.IsNullOrWhiteSpace(dto.Name)) { entity.Name = dto.Name.Trim(); changedFields.Add("Name"); }
                if (!string.IsNullOrWhiteSpace(dto.EntityType))
                {
                    var normalizedEntityType = VisibilityAccessService.NormalizeEntityType(dto.EntityType);
                    if (!VisibilityAccessService.IsSupportedEntityType(normalizedEntityType))
                    {
                        var msg = _localizationService.GetLocalizedString("VisibilityPolicyService.InvalidEntityType");
                        return ApiResponse<VisibilityPolicyDto>.ErrorResult(msg, msg, StatusCodes.Status400BadRequest);
                    }

                    entity.EntityType = normalizedEntityType;
                    changedFields.Add("EntityType");
                }
                if (dto.Description != null) { entity.Description = dto.Description.Trim(); changedFields.Add("Description"); }
                if (dto.ScopeType.HasValue) { entity.ScopeType = (VisibilityScopeType)dto.ScopeType.Value; changedFields.Add("ScopeType"); }
                if (dto.IncludeSelf.HasValue) { entity.IncludeSelf = dto.IncludeSelf.Value; changedFields.Add("IncludeSelf"); }
                if (dto.IsActive.HasValue) { entity.IsActive = dto.IsActive.Value; changedFields.Add("IsActive"); }

                await _unitOfWork.VisibilityPolicies.UpdateAsync(entity).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                await _accessControlRefreshNotifier
                    .NotifyUsersForVisibilityPolicyAsync(entity.Id, "visibility-policy.updated")
                    .ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "visibility-policy.update",
                    "VisibilityPolicy",
                    entity.Id.ToString(),
                    "Succeeded",
                    "access-control",
                    OldValues: oldValues,
                    NewValues: new
                    {
                        entity.Code,
                        entity.Name,
                        entity.EntityType,
                        entity.Description,
                        ScopeType = entity.ScopeType.ToString(),
                        entity.IncludeSelf,
                        entity.IsActive
                    },
                    ChangedFields: changedFields)).ConfigureAwait(false);

                return ApiResponse<VisibilityPolicyDto>.SuccessResult(
                    MapDto(entity),
                    _localizationService.GetLocalizedString("VisibilityPolicyService.Updated"));
            }
            catch (Exception ex)
            {
                return ApiResponse<VisibilityPolicyDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            try
            {
                var entity = await _unitOfWork.VisibilityPolicies.GetByIdAsync(id).ConfigureAwait(false);
                if (entity == null || entity.IsDeleted)
                {
                    var msg = _localizationService.GetLocalizedString("VisibilityPolicyService.NotFound");
                    return ApiResponse<bool>.ErrorResult(msg, msg, StatusCodes.Status404NotFound);
                }

                var oldValues = new
                {
                    entity.Code,
                    entity.Name,
                    entity.EntityType,
                    entity.Description,
                    ScopeType = entity.ScopeType.ToString(),
                    entity.IncludeSelf,
                    entity.IsActive
                };

                await _unitOfWork.VisibilityPolicies.SoftDeleteAsync(id).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                await _accessControlRefreshNotifier
                    .NotifyUsersForVisibilityPolicyAsync(id, "visibility-policy.deleted")
                    .ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "visibility-policy.delete",
                    "VisibilityPolicy",
                    id.ToString(),
                    "Succeeded",
                    "access-control",
                    OldValues: oldValues,
                    ChangedFields: ["IsDeleted"])).ConfigureAwait(false);

                return ApiResponse<bool>.SuccessResult(
                    true,
                    _localizationService.GetLocalizedString("VisibilityPolicyService.Deleted"));
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        private ApiResponse<VisibilityPolicyDto>? ValidateScopeType(int scopeType)
        {
            return !Enum.IsDefined(typeof(VisibilityScopeType), scopeType)
                ? ApiResponse<VisibilityPolicyDto>.ErrorResult(
                    _localizationService.GetLocalizedString("VisibilityPolicyService.InvalidScopeType"),
                    _localizationService.GetLocalizedString("VisibilityPolicyService.InvalidScopeType"),
                    StatusCodes.Status400BadRequest)
                : null;
        }

        private static VisibilityPolicyDto MapDto(VisibilityPolicy entity) => new()
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            EntityType = entity.EntityType,
            Description = entity.Description,
            ScopeType = (int)entity.ScopeType,
            IncludeSelf = entity.IncludeSelf,
            IsActive = entity.IsActive,
            CreatedDate = entity.CreatedDate,
            UpdatedDate = entity.UpdatedDate,
            DeletedDate = entity.DeletedDate,
            IsDeleted = entity.IsDeleted
        };
    }
}
