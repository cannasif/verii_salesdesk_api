using Microsoft.Extensions.Configuration;

namespace salesdesk_api.Modules.Identity.Application;

public static class PasswordResetLinkBuilder
{
    public static string Build(string? resetUrl, string? resetPasswordUrl, string token, IConfiguration configuration)
    {
        var clientBase = !string.IsNullOrWhiteSpace(resetUrl)
            ? resetUrl
            : resetPasswordUrl;

        if (!string.IsNullOrWhiteSpace(clientBase))
        {
            var basePath = clientBase.Trim();
            var queryIndex = basePath.IndexOf('?', StringComparison.Ordinal);
            if (queryIndex >= 0)
            {
                basePath = basePath[..queryIndex];
            }

            return $"{basePath.TrimEnd('/')}?token={token}";
        }

        var frontendBaseUrl = (configuration["FrontendSettings:BaseUrl"] ?? "http://localhost:5173").TrimEnd('/');
        var resetPasswordPath = configuration["FrontendSettings:ResetPasswordPath"] ?? "/reset-password";
        if (!resetPasswordPath.StartsWith("/"))
        {
            resetPasswordPath = "/" + resetPasswordPath;
        }

        return $"{frontendBaseUrl}{resetPasswordPath}?token={token}";
    }
}
