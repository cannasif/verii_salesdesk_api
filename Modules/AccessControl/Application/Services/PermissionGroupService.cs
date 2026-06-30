using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Helpers;
using salesdesk_api.Modules.AccessControl.Domain.Entities;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public class PermissionGroupService : IPermissionGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizationService _localizationService;
        private readonly IAccessControlRefreshNotifier _accessControlRefreshNotifier;
        private readonly IAuditLogWriter _auditLogWriter;

        public PermissionGroupService(
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

        public async Task<ApiResponse<PagedResponse<PermissionGroupDto>>> GetAllAsync(PagedRequest request)
        {
            try
            {
                request ??= new PagedRequest();
                request.Filters ??= new List<Filter>();

                var query = _unitOfWork.PermissionGroups.Query()
                    .AsNoTracking()
                    .Include(x => x.CreatedByUser)
                    .Include(x => x.UpdatedByUser)
                    .Include(x => x.DeletedByUser)
                    .Include(x => x.GroupPermissions.Where(gp => !gp.IsDeleted))
                    .ThenInclude(x => x.PermissionDefinition)
                    .ApplySearch(request.Search, QueryHelper.CommonSearchableColumns)
                    .ApplyFilters(request.Filters, request.FilterLogic)
                    .ApplySorting(request.SortBy ?? nameof(PermissionGroup.Id), request.SortDirection);

                var page = await query.ToPagedItemsAsync(request).ConfigureAwait(false);
                var items = page.Items;

                return ApiResponse<PagedResponse<PermissionGroupDto>>.SuccessResult(
                    new PagedResponse<PermissionGroupDto>
                    {
                        Items = items.Select(MapToDto).ToList(),
                        TotalCount = page.TotalCount,
                        PageNumber = page.PageNumber,
                        PageSize = page.PageSize
                    },
                    _localizationService.GetLocalizedString("General.OperationSuccessful"));
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResponse<PermissionGroupDto>>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<PermissionGroupDto>> GetByIdAsync(long id)
        {
            try
            {
                var entity = await _unitOfWork.PermissionGroups.Query()
                    .AsNoTracking()
                    .Include(x => x.CreatedByUser)
                    .Include(x => x.UpdatedByUser)
                    .Include(x => x.DeletedByUser)
                    .Include(x => x.GroupPermissions.Where(gp => !gp.IsDeleted))
                    .ThenInclude(x => x.PermissionDefinition)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted).ConfigureAwait(false);

                if (entity == null)
                {
                    return ApiResponse<PermissionGroupDto>.ErrorResult(
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        StatusCodes.Status404NotFound);
                }

                return ApiResponse<PermissionGroupDto>.SuccessResult(
                    MapToDto(entity),
                    _localizationService.GetLocalizedString("General.OperationSuccessful"));
            }
            catch (Exception ex)
            {
                return ApiResponse<PermissionGroupDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<PermissionGroupDto>> CreateAsync(CreatePermissionGroupDto dto)
        {
            try
            {
                var duplicate = await _unitOfWork.PermissionGroups.Query()
                    .AsNoTracking()
                    .AnyAsync(x => !x.IsDeleted && x.Name == dto.Name).ConfigureAwait(false);

                if (duplicate)
                {
                    return ApiResponse<PermissionGroupDto>.ErrorResult(
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        StatusCodes.Status400BadRequest);
                }

                var entity = new PermissionGroup
                {
                    Name = dto.Name.Trim(),
                    Description = dto.Description?.Trim(),
                    IsSystemAdmin = dto.IsSystemAdmin,
                    IsActive = dto.IsActive
                };

                await _unitOfWork.PermissionGroups.AddAsync(entity).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                if (dto.PermissionDefinitionIds.Count > 0)
                {
                    var linkResult = await SetPermissionsInternalAsync(entity.Id, dto.PermissionDefinitionIds).ConfigureAwait(false);
                    if (!linkResult.Success)
                    {
                        return ApiResponse<PermissionGroupDto>.ErrorResult(linkResult.Message, linkResult.ExceptionMessage, linkResult.StatusCode);
                    }
                }

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "permission-group.create",
                    "PermissionGroup",
                    entity.Id.ToString(),
                    "Succeeded",
                    "access-control",
                    NewValues: new
                    {
                        entity.Name,
                        entity.Description,
                        entity.IsSystemAdmin,
                        entity.IsActive,
                        PermissionDefinitionIds = dto.PermissionDefinitionIds.Distinct().OrderBy(x => x).ToList()
                    },
                    ChangedFields: ["Name", "Description", "IsSystemAdmin", "IsActive", "PermissionDefinitionIds"])).ConfigureAwait(false);

                return await GetByIdAsync(entity.Id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ApiResponse<PermissionGroupDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<PermissionGroupDto>> UpdateAsync(long id, UpdatePermissionGroupDto dto)
        {
            try
            {
                var entity = await _unitOfWork.PermissionGroups.GetByIdForUpdateAsync(id).ConfigureAwait(false);
                if (entity == null)
                {
                    return ApiResponse<PermissionGroupDto>.ErrorResult(
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        StatusCodes.Status404NotFound);
                }

                if (entity.IsSystemAdmin)
                {
                    return ApiResponse<PermissionGroupDto>.ErrorResult(
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        "System Admin permission group cannot be modified.",
                        StatusCodes.Status403Forbidden);
                }

                var oldValues = new
                {
                    entity.Name,
                    entity.Description,
                    entity.IsSystemAdmin,
                    entity.IsActive
                };
                var changedFields = new List<string>();

                if (!string.IsNullOrWhiteSpace(dto.Name) && !dto.Name.Equals(entity.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var duplicate = await _unitOfWork.PermissionGroups.Query()
                        .AsNoTracking()
                        .AnyAsync(x => !x.IsDeleted && x.Id != id && x.Name == dto.Name).ConfigureAwait(false);

                    if (duplicate)
                    {
                        return ApiResponse<PermissionGroupDto>.ErrorResult(
                            _localizationService.GetLocalizedString("General.ValidationError"),
                            _localizationService.GetLocalizedString("General.ValidationError"),
                            StatusCodes.Status400BadRequest);
                    }

                    entity.Name = dto.Name.Trim();
                    changedFields.Add("Name");
                }

                if (dto.Description != null)
                {
                    entity.Description = dto.Description.Trim();
                    changedFields.Add("Description");
                }

                if (dto.IsSystemAdmin.HasValue)
                {
                    entity.IsSystemAdmin = dto.IsSystemAdmin.Value;
                    changedFields.Add("IsSystemAdmin");
                }

                if (dto.IsActive.HasValue)
                {
                    entity.IsActive = dto.IsActive.Value;
                    changedFields.Add("IsActive");
                }

                await _unitOfWork.PermissionGroups.UpdateAsync(entity).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                await _accessControlRefreshNotifier
                    .NotifyUsersForPermissionGroupAsync(entity.Id, "permission-group.updated")
                    .ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "permission-group.update",
                    "PermissionGroup",
                    entity.Id.ToString(),
                    "Succeeded",
                    "access-control",
                    OldValues: oldValues,
                    NewValues: new
                    {
                        entity.Name,
                        entity.Description,
                        entity.IsSystemAdmin,
                        entity.IsActive
                    },
                    ChangedFields: changedFields)).ConfigureAwait(false);

                return await GetByIdAsync(entity.Id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ApiResponse<PermissionGroupDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<PermissionGroupDto>> SetPermissionsAsync(long id, SetPermissionGroupPermissionsDto dto)
        {
            var currentPermissionIds = await _unitOfWork.PermissionGroupPermissions.Query()
                .AsNoTracking()
                .Where(x => x.PermissionGroupId == id && !x.IsDeleted)
                .Select(x => x.PermissionDefinitionId)
                .OrderBy(x => x)
                .ToListAsync()
                .ConfigureAwait(false);

            var group = await _unitOfWork.PermissionGroups.GetByIdAsync(id).ConfigureAwait(false);
            if (group != null && group.IsSystemAdmin)
            {
                return ApiResponse<PermissionGroupDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.ValidationError"),
                    "System Admin permission group cannot be modified.",
                    StatusCodes.Status403Forbidden);
            }

            var result = await SetPermissionsInternalAsync(id, dto.PermissionDefinitionIds).ConfigureAwait(false);
            if (!result.Success)
            {
                return ApiResponse<PermissionGroupDto>.ErrorResult(result.Message, result.ExceptionMessage, result.StatusCode);
            }

            await _accessControlRefreshNotifier
                .NotifyUsersForPermissionGroupAsync(id, "permission-group.permissions-updated")
                .ConfigureAwait(false);

            var nextPermissionIds = dto.PermissionDefinitionIds.Distinct().OrderBy(x => x).ToList();
            await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                "permission-group.permissions.update",
                "PermissionGroup",
                id.ToString(),
                "Succeeded",
                "access-control",
                OldValues: new { PermissionDefinitionIds = currentPermissionIds },
                NewValues: new { PermissionDefinitionIds = nextPermissionIds },
                ChangedFields: ["PermissionDefinitionIds"])).ConfigureAwait(false);

            return await GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task<ApiResponse<bool>> SoftDeleteAsync(long id)
        {
            try
            {
                var entity = await _unitOfWork.PermissionGroups.GetByIdAsync(id).ConfigureAwait(false);
                if (entity == null)
                {
                    return ApiResponse<bool>.ErrorResult(
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        StatusCodes.Status404NotFound);
                }

                if (entity.IsSystemAdmin)
                {
                    return ApiResponse<bool>.ErrorResult(
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        "System Admin permission group cannot be deleted.",
                        StatusCodes.Status403Forbidden);
                }

                var oldValues = new
                {
                    entity.Name,
                    entity.Description,
                    entity.IsSystemAdmin,
                    entity.IsActive
                };

                await _unitOfWork.PermissionGroups.SoftDeleteAsync(id).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                await _accessControlRefreshNotifier
                    .NotifyUsersForPermissionGroupAsync(id, "permission-group.deleted")
                    .ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "permission-group.delete",
                    "PermissionGroup",
                    id.ToString(),
                    "Succeeded",
                    "access-control",
                    OldValues: oldValues,
                    ChangedFields: ["IsDeleted"])).ConfigureAwait(false);

                return ApiResponse<bool>.SuccessResult(true, _localizationService.GetLocalizedString("General.OperationSuccessful"));
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<ApiResponse<bool>> SetPermissionsInternalAsync(long groupId, List<long> permissionIds)
        {
            try
            {
                var group = await _unitOfWork.PermissionGroups.GetByIdAsync(groupId).ConfigureAwait(false);
                if (group == null)
                {
                    return ApiResponse<bool>.ErrorResult(
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        _localizationService.GetLocalizedString("General.ValidationError"),
                        StatusCodes.Status404NotFound);
                }

                var distinctPermissionIds = permissionIds.Distinct().ToList();
                if (distinctPermissionIds.Count > 0)
                {
                    var validCount = await _unitOfWork.PermissionDefinitions.Query()
                        .AsNoTracking()
                        .CountAsync(x => !x.IsDeleted && distinctPermissionIds.Contains(x.Id)).ConfigureAwait(false);

                    if (validCount != distinctPermissionIds.Count)
                    {
                        return ApiResponse<bool>.ErrorResult(
                            _localizationService.GetLocalizedString("General.ValidationError"),
                            _localizationService.GetLocalizedString("General.ValidationError"),
                            StatusCodes.Status400BadRequest);
                    }
                }

                var currentLinks = await _unitOfWork.PermissionGroupPermissions
                    .Query(tracking: true, ignoreQueryFilters: true)
                    .Where(x => x.PermissionGroupId == groupId)
                    .ToListAsync().ConfigureAwait(false);

                foreach (var link in currentLinks.Where(x => !x.IsDeleted && !distinctPermissionIds.Contains(x.PermissionDefinitionId)))
                {
                    await _unitOfWork.PermissionGroupPermissions.SoftDeleteAsync(link.Id).ConfigureAwait(false);
                }

                foreach (var permissionId in distinctPermissionIds)
                {
                    var existing = currentLinks.FirstOrDefault(x => x.PermissionDefinitionId == permissionId);
                    if (existing == null)
                    {
                        await _unitOfWork.PermissionGroupPermissions.AddAsync(new PermissionGroupPermission
                        {
                            PermissionGroupId = groupId,
                            PermissionDefinitionId = permissionId
                        }).ConfigureAwait(false);
                        continue;
                    }

                    if (existing.IsDeleted)
                    {
                        existing.IsDeleted = false;
                        existing.DeletedDate = null;
                        existing.DeletedBy = null;
                        await _unitOfWork.PermissionGroupPermissions.UpdateAsync(existing).ConfigureAwait(false);
                    }
                }

                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                return ApiResponse<bool>.SuccessResult(true, _localizationService.GetLocalizedString("General.OperationSuccessful"));
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        private static PermissionGroupDto MapToDto(PermissionGroup entity)
        {
            var groupPermissions = entity.GroupPermissions
                .Where(x => !x.IsDeleted && x.PermissionDefinition != null && !x.PermissionDefinition.IsDeleted && x.PermissionDefinition.IsActive)
                .ToList();

            return new PermissionGroupDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsSystemAdmin = entity.IsSystemAdmin,
                IsActive = entity.IsActive,
                PermissionDefinitionIds = groupPermissions.Select(x => x.PermissionDefinitionId).Distinct().OrderBy(x => x).ToList(),
                PermissionCodes = groupPermissions.Select(x => x.PermissionDefinition.Code).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x).ToList(),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                DeletedDate = entity.DeletedDate,
                IsDeleted = entity.IsDeleted,
                CreatedByFullUser = entity.CreatedByUser != null ? $"{entity.CreatedByUser.FirstName} {entity.CreatedByUser.LastName}".Trim() : null,
                UpdatedByFullUser = entity.UpdatedByUser != null ? $"{entity.UpdatedByUser.FirstName} {entity.UpdatedByUser.LastName}".Trim() : null,
                DeletedByFullUser = entity.DeletedByUser != null ? $"{entity.DeletedByUser.FirstName} {entity.DeletedByUser.LastName}".Trim() : null
            };
        }
    }
}
