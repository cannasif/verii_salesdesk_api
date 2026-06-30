using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public class UserVisibilityPolicyService : IUserVisibilityPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizationService _localizationService;
        private readonly IAccessControlRefreshNotifier _accessControlRefreshNotifier;
        private readonly IAuditLogWriter _auditLogWriter;

        public UserVisibilityPolicyService(
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

        public async Task<ApiResponse<PagedResponse<UserVisibilityPolicyDto>>> GetAllAsync(PagedRequest request)
        {
            try
            {
                request ??= new PagedRequest();
                request.Filters ??= new List<Filter>();

                var query = _unitOfWork.UserVisibilityPolicies.Query()
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted
                        && !x.VisibilityPolicy.IsDeleted
                        && x.VisibilityPolicy.IsActive)
                    .Include(x => x.User)
                    .Include(x => x.VisibilityPolicy)
                    .ApplySearch(request.Search, ["User.Username", "User.FirstName", "User.LastName", "VisibilityPolicy.Name", "VisibilityPolicy.EntityType"])
                    .ApplyFilters(request.Filters, request.FilterLogic);

                query = query.ApplySorting(request.SortBy ?? nameof(UserVisibilityPolicy.Id), request.SortDirection);

                var page = await query.ToPagedItemsAsync(request).ConfigureAwait(false);


                var items = page.Items;

                var dtos = items.Select(MapDto).ToList();
                return ApiResponse<PagedResponse<UserVisibilityPolicyDto>>.SuccessResult(
                    new PagedResponse<UserVisibilityPolicyDto>
                    {
                        Items = dtos,
                        TotalCount = page.TotalCount,
                        PageNumber = page.PageNumber,
                        PageSize = page.PageSize
                    },
                    _localizationService.GetLocalizedString("UserVisibilityPolicyService.ListRetrieved"));
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResponse<UserVisibilityPolicyDto>>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<UserVisibilityPolicyDto>> GetByIdAsync(long id)
        {
            try
            {
                var entity = await _unitOfWork.UserVisibilityPolicies.Query()
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.VisibilityPolicy)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
                    .ConfigureAwait(false);

                if (entity == null)
                {
                    var msg = _localizationService.GetLocalizedString("UserVisibilityPolicyService.NotFound");
                    return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(msg, msg, StatusCodes.Status404NotFound);
                }

                return ApiResponse<UserVisibilityPolicyDto>.SuccessResult(
                    MapDto(entity),
                    _localizationService.GetLocalizedString("UserVisibilityPolicyService.Retrieved"));
            }
            catch (Exception ex)
            {
                return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<UserVisibilityPolicyDto>> CreateAsync(CreateUserVisibilityPolicyDto dto)
        {
            try
            {
                var validation = await ValidateReferencesAsync(dto.UserId, dto.VisibilityPolicyId).ConfigureAwait(false);
                if (validation != null)
                {
                    return validation;
                }

                var existing = await _unitOfWork.UserVisibilityPolicies
                    .Query(tracking: true, ignoreQueryFilters: true)
                    .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.VisibilityPolicyId == dto.VisibilityPolicyId)
                    .ConfigureAwait(false);

                if (existing != null && !existing.IsDeleted)
                {
                    var dup = _localizationService.GetLocalizedString("UserVisibilityPolicyService.UserAlreadyHasPolicy");
                    return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(dup, dup, StatusCodes.Status400BadRequest);
                }

                UserVisibilityPolicy entity;
                if (existing != null)
                {
                    existing.IsDeleted = false;
                    existing.DeletedDate = null;
                    existing.DeletedBy = null;
                    entity = await _unitOfWork.UserVisibilityPolicies.UpdateAsync(existing).ConfigureAwait(false);
                }
                else
                {
                    entity = new UserVisibilityPolicy
                    {
                        UserId = dto.UserId,
                        VisibilityPolicyId = dto.VisibilityPolicyId
                    };

                    await _unitOfWork.UserVisibilityPolicies.AddAsync(entity).ConfigureAwait(false);
                }

                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                await _accessControlRefreshNotifier
                    .NotifyUserAsync(dto.UserId, "user-visibility-policy.created")
                    .ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    existing != null ? "user-visibility-policy.revive" : "user-visibility-policy.create",
                    "UserVisibilityPolicy",
                    entity.Id.ToString(),
                    "Succeeded",
                    "access-control",
                    NewValues: new
                    {
                        entity.UserId,
                        entity.VisibilityPolicyId
                    },
                    ChangedFields: ["UserId", "VisibilityPolicyId"])).ConfigureAwait(false);

                var created = await _unitOfWork.UserVisibilityPolicies.Query()
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.VisibilityPolicy)
                    .FirstAsync(x => x.Id == entity.Id && !x.IsDeleted)
                    .ConfigureAwait(false);

                return ApiResponse<UserVisibilityPolicyDto>.SuccessResult(
                    MapDto(created),
                    _localizationService.GetLocalizedString("UserVisibilityPolicyService.Created"));
            }
            catch (Exception ex)
            {
                return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<UserVisibilityPolicyDto>> UpdateAsync(long id, UpdateUserVisibilityPolicyDto dto)
        {
            try
            {
                var entity = await _unitOfWork.UserVisibilityPolicies.GetByIdForUpdateAsync(id).ConfigureAwait(false);
                if (entity == null || entity.IsDeleted)
                {
                    var msg = _localizationService.GetLocalizedString("UserVisibilityPolicyService.NotFound");
                    return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(msg, msg, StatusCodes.Status404NotFound);
                }

                var nextUserId = dto.UserId ?? entity.UserId;
                var nextPolicyId = dto.VisibilityPolicyId ?? entity.VisibilityPolicyId;

                var validation = await ValidateReferencesAsync(nextUserId, nextPolicyId).ConfigureAwait(false);
                if (validation != null)
                {
                    return validation;
                }

                var duplicate = await _unitOfWork.UserVisibilityPolicies
                    .Query(tracking: true, ignoreQueryFilters: true)
                    .FirstOrDefaultAsync(x => x.Id != id && x.UserId == nextUserId && x.VisibilityPolicyId == nextPolicyId)
                    .ConfigureAwait(false);

                if (duplicate != null && !duplicate.IsDeleted)
                {
                    var dup = _localizationService.GetLocalizedString("UserVisibilityPolicyService.UserAlreadyHasPolicy");
                    return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(dup, dup, StatusCodes.Status400BadRequest);
                }

                var previousUserId = entity.UserId;
                var oldValues = new
                {
                    entity.UserId,
                    entity.VisibilityPolicyId
                };

                if (duplicate != null)
                {
                    duplicate.IsDeleted = false;
                    duplicate.DeletedDate = null;
                    duplicate.DeletedBy = null;
                    await _unitOfWork.UserVisibilityPolicies.UpdateAsync(duplicate).ConfigureAwait(false);
                    await _unitOfWork.UserVisibilityPolicies.SoftDeleteAsync(entity.Id).ConfigureAwait(false);
                    await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                    await _accessControlRefreshNotifier
                        .NotifyUsersAsync([previousUserId, nextUserId], "user-visibility-policy.updated")
                        .ConfigureAwait(false);

                    await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                        "user-visibility-policy.merge-update",
                        "UserVisibilityPolicy",
                        duplicate.Id.ToString(),
                        "Succeeded",
                        "access-control",
                        OldValues: oldValues,
                        NewValues: new
                        {
                            duplicate.UserId,
                            duplicate.VisibilityPolicyId
                        },
                        ChangedFields: ["UserId", "VisibilityPolicyId"])).ConfigureAwait(false);

                    var revived = await _unitOfWork.UserVisibilityPolicies.Query()
                        .AsNoTracking()
                        .Include(x => x.User)
                        .Include(x => x.VisibilityPolicy)
                        .FirstAsync(x => x.Id == duplicate.Id && !x.IsDeleted)
                        .ConfigureAwait(false);

                    return ApiResponse<UserVisibilityPolicyDto>.SuccessResult(
                        MapDto(revived),
                        _localizationService.GetLocalizedString("UserVisibilityPolicyService.Updated"));
                }

                entity.UserId = nextUserId;
                entity.VisibilityPolicyId = nextPolicyId;

                await _unitOfWork.UserVisibilityPolicies.UpdateAsync(entity).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                await _accessControlRefreshNotifier
                    .NotifyUsersAsync([previousUserId, nextUserId], "user-visibility-policy.updated")
                    .ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "user-visibility-policy.update",
                    "UserVisibilityPolicy",
                    entity.Id.ToString(),
                    "Succeeded",
                    "access-control",
                    OldValues: oldValues,
                    NewValues: new
                    {
                        entity.UserId,
                        entity.VisibilityPolicyId
                    },
                    ChangedFields: ["UserId", "VisibilityPolicyId"])).ConfigureAwait(false);

                var updated = await _unitOfWork.UserVisibilityPolicies.Query()
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.VisibilityPolicy)
                    .FirstAsync(x => x.Id == entity.Id && !x.IsDeleted)
                    .ConfigureAwait(false);

                return ApiResponse<UserVisibilityPolicyDto>.SuccessResult(
                    MapDto(updated),
                    _localizationService.GetLocalizedString("UserVisibilityPolicyService.Updated"));
            }
            catch (Exception ex)
            {
                return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            try
            {
                var entity = await _unitOfWork.UserVisibilityPolicies.GetByIdAsync(id).ConfigureAwait(false);
                if (entity == null || entity.IsDeleted)
                {
                    var msg = _localizationService.GetLocalizedString("UserVisibilityPolicyService.NotFound");
                    return ApiResponse<bool>.ErrorResult(msg, msg, StatusCodes.Status404NotFound);
                }

                var oldValues = new
                {
                    entity.UserId,
                    entity.VisibilityPolicyId
                };

                await _unitOfWork.UserVisibilityPolicies.SoftDeleteAsync(id).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                await _accessControlRefreshNotifier
                    .NotifyUserAsync(entity.UserId, "user-visibility-policy.deleted")
                    .ConfigureAwait(false);

                await _auditLogWriter.WriteAsync(new AuditLogWriteEntry(
                    "user-visibility-policy.delete",
                    "UserVisibilityPolicy",
                    id.ToString(),
                    "Succeeded",
                    "access-control",
                    OldValues: oldValues,
                    ChangedFields: ["IsDeleted"])).ConfigureAwait(false);

                return ApiResponse<bool>.SuccessResult(
                    true,
                    _localizationService.GetLocalizedString("UserVisibilityPolicyService.Deleted"));
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(
                    _localizationService.GetLocalizedString("General.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<ApiResponse<UserVisibilityPolicyDto>?> ValidateReferencesAsync(long userId, long visibilityPolicyId)
        {
            var userExists = await _unitOfWork.Users.Query().AsNoTracking().AnyAsync(x => !x.IsDeleted && x.Id == userId).ConfigureAwait(false);
            if (!userExists)
            {
                var u = _localizationService.GetLocalizedString("AuthUserNotFound");
                return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(u, u, StatusCodes.Status400BadRequest);
            }

            var policyExists = await _unitOfWork.VisibilityPolicies.Query().AsNoTracking().AnyAsync(x => !x.IsDeleted && x.IsActive && x.Id == visibilityPolicyId).ConfigureAwait(false);
            if (!policyExists)
            {
                var p = _localizationService.GetLocalizedString("UserVisibilityPolicyService.VisibilityPolicyNotFound");
                return ApiResponse<UserVisibilityPolicyDto>.ErrorResult(p, p, StatusCodes.Status400BadRequest);
            }

            return null;
        }

        private static UserVisibilityPolicyDto MapDto(UserVisibilityPolicy entity) => new()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            UserDisplayName = entity.User?.FullName?.Length > 0 == true ? entity.User.FullName : entity.User?.Username ?? string.Empty,
            VisibilityPolicyId = entity.VisibilityPolicyId,
            VisibilityPolicyName = entity.VisibilityPolicy?.Name ?? string.Empty,
            EntityType = entity.VisibilityPolicy?.EntityType ?? string.Empty,
            ScopeType = entity.VisibilityPolicy != null ? (int)entity.VisibilityPolicy.ScopeType : 0,
            CreatedDate = entity.CreatedDate,
            UpdatedDate = entity.UpdatedDate,
            DeletedDate = entity.DeletedDate,
            IsDeleted = entity.IsDeleted
        };
    }
}
