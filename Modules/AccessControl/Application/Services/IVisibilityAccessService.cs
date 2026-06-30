namespace salesdesk_api.Modules.AccessControl.Application.Services
{
    public sealed class VisibilityPreviewUser
    {
        public long UserId { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string? Email { get; init; }
    }

    public sealed class VisibilityPreviewPolicy
    {
        public long PolicyId { get; init; }
        public string Code { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public int ScopeType { get; init; }
        public bool IncludeSelf { get; init; }
    }

    public sealed class ActionSimulationResult
    {
        public string Action { get; init; } = string.Empty;
        public bool Allowed { get; init; }
        public string Reason { get; init; } = string.Empty;
    }

    public sealed class VisibilityActionSimulationResult
    {
        public long UserId { get; init; }
        public string EntityType { get; init; } = string.Empty;
        public long EntityId { get; init; }
        public IReadOnlyCollection<ActionSimulationResult> Actions { get; init; } = Array.Empty<ActionSimulationResult>();
    }

    public sealed class VisibilityPreviewResult
    {
        public long UserId { get; init; }
        public string EntityType { get; init; } = string.Empty;
        public bool HasExplicitPolicy { get; init; }
        public bool IsUnrestricted { get; init; }
        public IReadOnlyCollection<long> VisibleUserIds { get; init; } = Array.Empty<long>();
        public IReadOnlyCollection<VisibilityPreviewUser> VisibleUsers { get; init; } = Array.Empty<VisibilityPreviewUser>();
        public IReadOnlyCollection<VisibilityPreviewPolicy> Policies { get; init; } = Array.Empty<VisibilityPreviewPolicy>();
    }

    public sealed class VisibilityResolution
    {
        public bool HasExplicitPolicy { get; init; }
        public bool IsUnrestricted { get; init; }
        public IReadOnlyCollection<long> VisibleUserIds { get; init; } = Array.Empty<long>();
    }

    public interface IVisibilityAccessService
    {
        Task<VisibilityResolution> ResolveVisibleUsersAsync(long userId, string entityType);
        Task<VisibilityPreviewResult> PreviewVisibilityAsync(long userId, string entityType);
        Task<VisibilityActionSimulationResult> SimulateRecordAccessAsync(long userId, string entityType, long entityId);
        Task<bool> CanAccessActivityAsync(long userId, long activityId);
    }
}
