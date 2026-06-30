using salesdesk_api.Modules.AccessControl.Domain.Enums;

namespace salesdesk_api.Modules.AccessControl.Domain.Entities
{
    public class VisibilityPolicy : BaseEntity
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string EntityType { get; set; } = string.Empty;

        public string? Description { get; set; }

        public VisibilityScopeType ScopeType { get; set; } = VisibilityScopeType.Self;

        public bool IncludeSelf { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public virtual ICollection<UserVisibilityPolicy> UserAssignments { get; set; } = new List<UserVisibilityPolicy>();
    }
}
