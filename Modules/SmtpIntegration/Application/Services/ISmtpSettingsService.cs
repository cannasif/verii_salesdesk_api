namespace salesdesk_api.Modules.SmtpIntegration.Application.Services;

public interface ISmtpSettingsService
{
    Task<ApiResponse<SmtpSettingsDto>> GetAsync();
    Task<ApiResponse<SmtpSettingsDto>> UpdateAsync(UpdateSmtpSettingsDto dto, long userId);

    Task<SmtpSettingsRuntimeDto> GetRuntimeAsync();

    void InvalidateCache();
}
