
namespace salesdesk_api.Modules.AccessControl.Domain.Entities
{
    public class PermissionDefinition : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool AvailableOnWeb { get; set; } = true;
        public bool AvailableOnMobile { get; set; } = false;

        public virtual ICollection<PermissionGroupPermission> GroupPermissions { get; set; } = new List<PermissionGroupPermission>();
    }
}
