using System.ComponentModel.DataAnnotations;

namespace salesdesk_api.Modules.AccessControl.Application.Dtos
{
    public class VisibilityPolicyDto : BaseEntityDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ScopeType { get; set; }
        public bool IncludeSelf { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateVisibilityPolicyDto
    {
        [Required]
        [StringLength(120)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(60)]
        public string EntityType { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 4)]
        public int ScopeType { get; set; } = 1;

        public bool IncludeSelf { get; set; } = true;

        public bool IsActive { get; set; } = true;
    }

    public class UpdateVisibilityPolicyDto
    {
        [StringLength(120)]
        public string? Code { get; set; }

        [StringLength(150)]
        public string? Name { get; set; }

        [StringLength(60)]
        public string? EntityType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 4)]
        public int? ScopeType { get; set; }

        public bool? IncludeSelf { get; set; }

        public bool? IsActive { get; set; }
    }

    public class UserVisibilityPolicyDto : BaseEntityDto
    {
        public long UserId { get; set; }
        public string UserDisplayName { get; set; } = string.Empty;
        public long VisibilityPolicyId { get; set; }
        public string VisibilityPolicyName { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public int ScopeType { get; set; }
    }

    public class CreateUserVisibilityPolicyDto
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long VisibilityPolicyId { get; set; }
    }

    public class UpdateUserVisibilityPolicyDto
    {
        public long? UserId { get; set; }
        public long? VisibilityPolicyId { get; set; }
    }
}
