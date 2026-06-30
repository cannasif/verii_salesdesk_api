using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Modules.AccessControl.Domain.Entities;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public class UserPermissionGroupService : IUserPermissionGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizationService _localizationService;
        private readonly IAccessControlRefreshNotifier _accessControlRefreshNotifier;
        private readonly IAuditLogWriter _auditLogWriter;

        public UserPermissionGroupService(
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

        public async Task<ApiResponse<UserPermissionGroupDto>> GetByUserIdAsync(long userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId).ConfigureAwait(false);
                if (user == null)
                {
                    return ApiResponse<UserPermissionGroupDto>.ErrorResult(
                        _localizationService.GetLocalizedString("UserService.UserNotFound"),
                        _localizationService.GetLocalizedString("UserService.UserNotFound"),
                        StatusCodes.Status404NotFound);
                }

                var links = await _unitOfWork.UserPermissionGroups.Query()
                    .AsNoTracking()
                    .Where(x => x.UserId == userId && !x.IsDeleted)
                    .Include(x => x.PermissionGroup)
                    .ToListAsync().ConfigureAwait(false);

                var dto = new UserPermissionGroupDto
                {
                    UserId = userId,
                    PermissionGroupIds = links.Select(x => x.PermissionGroupId).Distinct().OrderBy(x => x).ToList(),
                    PermissionGroupNames = links
                        .Where(x => x.PermissionGroup != null)
                        .Select(x => x.PermissionGroup.Name)
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .OrderBy(x => x)
                        .ToList()
                };

                return ApiResponse<UserPermissionGroupDto>.SuccessResult(dto, _localizationService.GetLocalizedString("General.OperationSuccessful"));
            }
            catch (Exception ex)
            {
                return ApiResponse<UserPermissionGroupDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<UserPermissionGroupDto>> SetUserGroupsAsync(long userId, SetUserPermissionGroupsDto dto)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId).ConfigureAwait(false);
                if (user == null)
                {
                    return ApiResponse<UserPermissionGroupDto>.ErrorResult(
                        _localizationService.GetLocalizedString("UserService.UserNotFound"),
                        _localizationService.GetLocalizedString("UserService.UserNotFound"),
                        StatusCodes.Status404NotFound);
                }

                var distinctGroupIds = dto.PermissionGroupIds.Distinct().ToList();
                if (distinctGroupIds.Count > 0)
                {
                    var validCount = await _unitOfWork.PermissionGroups.Query()
                        .AsNoTracking()
                        .CountAsync(x => !x.IsDeleted && distinctGroupIds.Contains(x.Id)).ConfigureAwait(false);

                    if (validCount != distinctGroupIds.Count)
                    {
                        return ApiResponse<UserPermissionGroupDto>.ErrorResult(
                            _localizationService.GetLocalizedString("General.ValidationError"),
                            _localizationService.GetLocalizedString("General.ValidationError"),
                            StatusCodes.Status400BadRequest);
                    }
                }

                var previousGroupIds = await _unitOfWork.UserPermissionGroups.Query()
                    .AsNoTracking()
                    .Where(x => x.UserId == userId && !x.IsDeleted)
                    .Select(x => x.PermissionGroupId)
                    .OrderBy(x => x)
                    .ToListAsync()
                    .ConfigureAwait(false);

                var affectedGroupIds = previousGroupIds
                    .Concat(distinctGroupIds)
                    .Distinct()
                    .ToList();
                var previousAffectedUserIds = await GetUsersInPermissionGroupsAsync(affectedGroupIds).ConfigureAwait(false);

                var currentLinks = await _unitOfWork.UserPermissionGroups
                    .Query(tracking: true, ignoreQueryFilters: true)
                    .Where(x => x.UserId == userId)
                    .ToListAsync().ConfigureAwait(false);

                foreach (var link in currentLinks.Where(x => !x.IsDeleted && !distinctGroupIds.Contains(x.PermissionGroupId)))
                {
                    await _unitOfWork.UserPermissionGroups.SoftDeleteAsync(link.Id).ConfigureAwait(false);
                }

                foreach (var groupId in distinctGroupIds)
                {
                    var existing = currentLinks.FirstOrDefault(x => x.PermissionGroupId == groupId);
                    if (existing == null)
                    {
                        await _unitOfWork.UserPermissionGroups.AddAsync(new UserPermissionGroup
                        {
                            UserId = userId,
                            PermissionGroupId = groupId
                        }).ConfigureAwait(false);
                        continue;
                    }

                    if (existing.IsDeleted)
                    {
                        existing.IsDeleted = false;
                        existing.DeletedDate = null;
                        existing.DeletedBy = null;
                        await _unitOfWork.UserPermissionGroups.UpdateAsync(existing).ConfigureAwait(false);
                    }
                }

                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                var nextAffectedUserIds = await GetUsersInPermissionGroupsAsync(affectedGroupIds).ConfigureAwait(false);
                await _accessControlRefreshNotifier
                    .NotifyUsersAsync(
                        previousAffectedUserIds.Concat(nextAffectedUserIds).Append(userId),
                        "user-permission-groups.changed")
                    .ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "user-permission-groups.set",
                    "UserPermissionGroup",
                    userId.ToString(),
                    "Succeeded",
                    "access-control",
                    OldValues: new { PermissionGroupIds = previousGroupIds },
                    NewValues: new { PermissionGroupIds = distinctGroupIds.OrderBy(x => x).ToList() },
                    ChangedFields: ["PermissionGroupIds"])).ConfigureAwait(false);

                return await GetByUserIdAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserPermissionGroupDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<IReadOnlyCollection<long>> GetUsersInPermissionGroupsAsync(IReadOnlyCollection<long> permissionGroupIds)
        {
            if (permissionGroupIds.Count == 0)
            {
                return Array.Empty<long>();
            }

            return await _unitOfWork.UserPermissionGroups.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted && permissionGroupIds.Contains(x.PermissionGroupId))
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
