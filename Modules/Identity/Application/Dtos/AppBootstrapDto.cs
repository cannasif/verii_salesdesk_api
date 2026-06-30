using salesdesk_api.Modules.AccessControl.Application.Dtos;
using salesdesk_api.Modules.System.Application.Dtos;

namespace salesdesk_api.Modules.Identity.Application.Dtos
{
    public class AppBootstrapUserDto
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class AppBootstrapDto
    {
        public AppBootstrapUserDto User { get; set; } = new();
        public MyPermissionsDto Permissions { get; set; } = new();
        public SystemSettingsDto SystemSettings { get; set; } = new();
    }
}
