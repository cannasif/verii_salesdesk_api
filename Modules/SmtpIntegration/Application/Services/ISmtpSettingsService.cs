namespace salesdesk_api.Modules.SmtpIntegration.Application.Services;

public interface ISmtpSettingsService
{
    Task<ApiResponse<SmtpSettingsDto>> GetAsync();
    Task<ApiResponse<SmtpSettingsDto>> UpdateAsync(UpdateSmtpSettingsDto dto, long userId);

    Task<SmtpSettingsRuntimeDto> GetRuntimeAsync();

    /// <summary>Returns false when SMTP row is missing or Host/Username/Password/FromEmail are not set.</summary>
    Task<bool> IsRuntimeMailConfiguredAsync();

    void InvalidateCache();
}
