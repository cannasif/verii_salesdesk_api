namespace salesdesk_api.Modules.AccessControl.Domain.Entities
{
    public class UserVisibilityPolicy : BaseEntity
    {
        public long UserId { get; set; }

        public User User { get; set; } = null!;

        public long VisibilityPolicyId { get; set; }

        public VisibilityPolicy VisibilityPolicy { get; set; } = null!;
    }
}
