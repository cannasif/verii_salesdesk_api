namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public interface IAccessControlRefreshNotifier
    {
        Task NotifyUsersAsync(IEnumerable<long> userIds, string reason, bool forceBootstrapRefresh = true);
        Task NotifyUserAsync(long userId, string reason, bool forceBootstrapRefresh = true);
        Task NotifyUsersForPermissionGroupAsync(long permissionGroupId, string reason, bool forceBootstrapRefresh = true);
        Task NotifyUsersForVisibilityPolicyAsync(long visibilityPolicyId, string reason, bool forceBootstrapRefresh = true);
    }
}
