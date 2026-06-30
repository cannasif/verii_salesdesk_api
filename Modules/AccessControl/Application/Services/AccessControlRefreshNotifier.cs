using salesdesk_api.Hubs;
using salesdesk_api.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public class AccessControlRefreshNotifier : IAccessControlRefreshNotifier
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<AccessControlRefreshNotifier> _logger;

        public AccessControlRefreshNotifier(
            IUnitOfWork unitOfWork,
            IHubContext<NotificationHub> hubContext,
            ILogger<AccessControlRefreshNotifier> logger)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _logger = logger;
        }

        public Task NotifyUserAsync(long userId, string reason, bool forceBootstrapRefresh = true)
            => NotifyUsersAsync([userId], reason, forceBootstrapRefresh);

        public async Task NotifyUsersForPermissionGroupAsync(long permissionGroupId, string reason, bool forceBootstrapRefresh = true)
        {
            var userIds = await _unitOfWork.UserPermissionGroups.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted && x.PermissionGroupId == permissionGroupId)
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);

            await NotifyUsersAsync(userIds, reason, forceBootstrapRefresh).ConfigureAwait(false);
        }

        public async Task NotifyUsersForVisibilityPolicyAsync(long visibilityPolicyId, string reason, bool forceBootstrapRefresh = true)
        {
            var userIds = await _unitOfWork.UserVisibilityPolicies.Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted && x.VisibilityPolicyId == visibilityPolicyId)
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);

            await NotifyUsersAsync(userIds, reason, forceBootstrapRefresh).ConfigureAwait(false);
        }

        public async Task NotifyUsersAsync(IEnumerable<long> userIds, string reason, bool forceBootstrapRefresh = true)
        {
            var distinctUserIds = userIds
                .Where(x => x > 0)
                .Distinct()
                .ToList();

            if (distinctUserIds.Count == 0)
            {
                return;
            }

            var payload = new
            {
                reason,
                forceBootstrapRefresh,
                issuedAt = DateTime.UtcNow,
            };

            foreach (var userId in distinctUserIds)
            {
                try
                {
                    await _hubContext.Clients.Group($"user_{userId}")
                        .SendAsync("AccessControlChanged", payload)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to send access control refresh event to user {UserId}", userId);
                }
            }
        }
    }
}
