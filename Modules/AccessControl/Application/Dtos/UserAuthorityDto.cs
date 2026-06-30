using System.ComponentModel.DataAnnotations;

namespace salesdesk_api.Modules.AccessControl.Application.Dtos
{
    public class UserAuthorityDto : BaseEntityDto
    {
        public string Title { get; set; } = string.Empty;
    }

    public class CreateUserAuthorityDto
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; } = string.Empty;
    }

    public class UpdateUserAuthorityDto
    {
        [StringLength(30)]
        public string? Title { get; set; }
    }
}
