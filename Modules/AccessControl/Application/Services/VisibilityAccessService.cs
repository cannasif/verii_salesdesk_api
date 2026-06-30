using Microsoft.EntityFrameworkCore;
using salesdesk_api.Modules.AccessControl.Domain.Enums;
using salesdesk_api.UnitOfWork;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public class VisibilityAccessService : IVisibilityAccessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VisibilityAccessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VisibilityResolution> ResolveVisibleUsersAsync(long userId, string entityType)
        {
            var normalizedEntityType = NormalizeEntityType(entityType);
            var assignments = await _unitOfWork.UserVisibilityPolicies.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted
                    && x.UserId == userId
                    && !x.VisibilityPolicy.IsDeleted
                    && x.VisibilityPolicy.IsActive
                    && x.VisibilityPolicy.EntityType == normalizedEntityType)
                .Include(x => x.VisibilityPolicy)
                .ToListAsync()
                .ConfigureAwait(false);

            if (assignments.Count == 0)
            {
                return new VisibilityResolution
                {
                    HasExplicitPolicy = false,
                    IsUnrestricted = true
                };
            }

            var visibleUserIds = new HashSet<long>();
            var isUnrestricted = false;

            foreach (var assignment in assignments)
            {
                var policy = assignment.VisibilityPolicy;
                if (policy.IncludeSelf)
                {
                    visibleUserIds.Add(userId);
                }

                switch (policy.ScopeType)
                {
                    case VisibilityScopeType.Self:
                        visibleUserIds.Add(userId);
                        break;
                    case VisibilityScopeType.ManagerHierarchy:
                        foreach (var childUserId in await GetManagerHierarchyUserIdsAsync(userId).ConfigureAwait(false))
                        {
                            visibleUserIds.Add(childUserId);
                        }
                        break;
                    case VisibilityScopeType.PermissionGroup:
                        foreach (var peerUserId in await GetSamePermissionGroupUserIdsAsync(userId).ConfigureAwait(false))
                        {
                            visibleUserIds.Add(peerUserId);
                        }
                        break;
                    case VisibilityScopeType.Company:
                        isUnrestricted = true;
                        break;
                }
            }

            return new VisibilityResolution
            {
                HasExplicitPolicy = true,
                IsUnrestricted = isUnrestricted,
                VisibleUserIds = visibleUserIds.ToList()
            };
        }

        public async Task<VisibilityPreviewResult> PreviewVisibilityAsync(long userId, string entityType)
        {
            var normalizedEntityType = NormalizeEntityType(entityType);
            var resolution = await ResolveVisibleUsersAsync(userId, normalizedEntityType).ConfigureAwait(false);
            var visibleUserIds = resolution.IsUnrestricted
                ? await _unitOfWork.Users.Query()
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted && x.IsActive)
                    .Select(x => x.Id)
                    .ToListAsync()
                    .ConfigureAwait(false)
                : resolution.VisibleUserIds.ToList();

            var visibleUsers = await _unitOfWork.Users.Query()
                .AsNoTracking()
                .Where(x => visibleUserIds.Contains(x.Id))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Select(x => new VisibilityPreviewUser
                {
                    UserId = x.Id,
                    FullName = ((x.FirstName ?? string.Empty) + " " + (x.LastName ?? string.Empty)).Trim(),
                    Email = x.Email
                })
                .ToListAsync()
                .ConfigureAwait(false);

            var policies = await _unitOfWork.UserVisibilityPolicies.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted
                    && x.UserId == userId
                    && !x.VisibilityPolicy.IsDeleted
                    && x.VisibilityPolicy.IsActive
                    && x.VisibilityPolicy.EntityType == normalizedEntityType)
                .Select(x => new VisibilityPreviewPolicy
                {
                    PolicyId = x.VisibilityPolicyId,
                    Code = x.VisibilityPolicy.Code,
                    Name = x.VisibilityPolicy.Name,
                    ScopeType = (int)x.VisibilityPolicy.ScopeType,
                    IncludeSelf = x.VisibilityPolicy.IncludeSelf
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return new VisibilityPreviewResult
            {
                UserId = userId,
                EntityType = normalizedEntityType,
                HasExplicitPolicy = resolution.HasExplicitPolicy,
                IsUnrestricted = resolution.IsUnrestricted,
                VisibleUserIds = visibleUserIds,
                VisibleUsers = visibleUsers,
                Policies = policies
            };
        }

        public Task<VisibilityActionSimulationResult> SimulateRecordAccessAsync(long userId, string entityType, long entityId)
        {
            return Task.FromResult(new VisibilityActionSimulationResult
            {
                UserId = userId,
                EntityType = NormalizeEntityType(entityType),
                EntityId = entityId,
                Actions = new[]
                {
                    new ActionSimulationResult
                    {
                        Action = "view",
                        Allowed = true,
                        Reason = "SalesDesk visibility policy is evaluated at list/query level."
                    }
                }
            });
        }

        public Task<bool> CanAccessActivityAsync(long userId, long activityId)
        {
            return Task.FromResult(true);
        }

        private async Task<IReadOnlyCollection<long>> GetManagerHierarchyUserIdsAsync(long userId)
        {
            var users = await _unitOfWork.Users.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted && x.IsActive)
                .Select(x => new { x.Id, x.ManagerUserId })
                .ToListAsync()
                .ConfigureAwait(false);

            var directReportsByManager = users
                .Where(x => x.ManagerUserId.HasValue)
                .GroupBy(x => x.ManagerUserId!.Value)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Id).ToList());

            var result = new HashSet<long> { userId };
            var queue = new Queue<long>();
            queue.Enqueue(userId);

            while (queue.Count > 0)
            {
                var managerId = queue.Dequeue();
                if (!directReportsByManager.TryGetValue(managerId, out var directReports))
                {
                    continue;
                }

                foreach (var directReport in directReports)
                {
                    if (result.Add(directReport))
                    {
                        queue.Enqueue(directReport);
                    }
                }
            }

            return result.ToList();
        }

        private async Task<IReadOnlyCollection<long>> GetSamePermissionGroupUserIdsAsync(long userId)
        {
            var myGroupIds = await _unitOfWork.UserPermissionGroups.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted && x.UserId == userId)
                .Select(x => x.PermissionGroupId)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);

            if (myGroupIds.Count == 0)
            {
                return new[] { userId };
            }

            return await _unitOfWork.UserPermissionGroups.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted && myGroupIds.Contains(x.PermissionGroupId))
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public static bool IsSupportedEntityType(string entityType)
        {
            var normalized = NormalizeEntityType(entityType);
            return normalized is "SalesDesk"
                or "SalesDeskCustomer"
                or "SalesDeskPotentialCustomer"
                or "SalesDeskProduct"
                or "SalesDeskQuote"
                or "SalesDeskInvoice"
                or "SalesDeskTask"
                or "SalesDeskVisit"
                or "SalesDeskAsset"
                or "SalesDeskRecurringPayment";
        }

        public static string NormalizeEntityType(string entityType)
        {
            return string.IsNullOrWhiteSpace(entityType) ? "SalesDesk" : entityType.Trim();
        }
    }
}
