namespace salesdesk_api.Modules.SmtpIntegration.Application.Services;

public interface IMailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true, string? cc = null, string? bcc = null, List<string>? attachments = null);
    Task<bool> SendEmailAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null, bool isHtml = true, string? cc = null, string? bcc = null, List<string>? attachments = null);
    Task<ApiResponse<bool>> SendTestEmailAsync(SendTestMailDto dto, long userId);
}
