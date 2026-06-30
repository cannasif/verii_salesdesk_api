using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using salesdesk_api.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace salesdesk_api.Modules.SmtpIntegration.Application.Services;

public class MailService : IMailService
{
    private readonly ILogger<MailService> _logger;
    private readonly ISmtpSettingsService _smtpSettingsService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILocalizationService _localizationService;

    public MailService(
        ISmtpSettingsService smtpSettingsService,
        ILogger<MailService> logger,
        IUnitOfWork unitOfWork,
        ILocalizationService localizationService)
    {
        _smtpSettingsService = smtpSettingsService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _localizationService = localizationService;
    }

    public async Task<bool> SendEmailAsync(
        string to,
        string subject,
        string body,
        bool isHtml = true,
        string? cc = null,
        string? bcc = null,
        List<string>? attachments = null)
    {
        return await SendEmailAsync(to, subject, body, null, null, isHtml, cc, bcc, attachments).ConfigureAwait(false);
    }

    public async Task<bool> SendEmailAsync(
        string to,
        string subject,
        string body,
        string? fromEmail = null,
        string? fromName = null,
        bool isHtml = true,
        string? cc = null,
        string? bcc = null,
        List<string>? attachments = null)
    {
        try
        {
            var smtp = await _smtpSettingsService.GetRuntimeAsync().ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(smtp.Host))
            {
                _logger.LogError("SMTP Host is not configured (DB).");
                return false;
            }

            if (string.IsNullOrWhiteSpace(smtp.Username) || string.IsNullOrWhiteSpace(smtp.Password))
            {
                _logger.LogError("SMTP Username or Password is not configured (DB).");
                return false;
            }

            using var client = new SmtpClient(smtp.Host, smtp.Port)
            {
                EnableSsl = smtp.EnableSsl,
                Credentials = new NetworkCredential(smtp.Username, smtp.Password),
                Timeout = smtp.Timeout * 1000
            };

            using var message = new MailMessage();

            var resolvedFromEmail = !string.IsNullOrWhiteSpace(fromEmail) ? fromEmail : smtp.FromEmail;
            var resolvedFromName = !string.IsNullOrWhiteSpace(fromName) ? fromName : smtp.FromName;

            if (string.IsNullOrWhiteSpace(resolvedFromEmail))
            {
                _logger.LogError("FromEmail is not configured (DB).");
                return false;
            }

            message.From = new MailAddress(resolvedFromEmail, resolvedFromName);

            message.To.Add(to);

            if (!string.IsNullOrWhiteSpace(cc))
                message.CC.Add(cc);

            if (!string.IsNullOrWhiteSpace(bcc))
                message.Bcc.Add(bcc);

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            if (attachments != null && attachments.Any())
            {
                foreach (var attachmentPath in attachments)
                {
                    if (File.Exists(attachmentPath))
                        message.Attachments.Add(new Attachment(attachmentPath));
                    else
                        _logger.LogWarning($"Attachment file not found: {attachmentPath}");
                }
            }

            await client.SendMailAsync(message).ConfigureAwait(false);
            _logger.LogInformation($"Email sent successfully to {to} with subject: {subject}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send email to {to}. Error: {ex.Message}");
            return false;
        }
    }

    public async Task<ApiResponse<bool>> SendTestEmailAsync(SendTestMailDto dto, long userId)
    {
        try
        {
            SmtpSettingsRuntimeDto smtp;
            try
            {
                smtp = await _smtpSettingsService.GetRuntimeAsync().ConfigureAwait(false);
            }
            catch (InvalidOperationException)
            {
                return Error("MailController.SmtpSettingsMissing", StatusCodes.Status400BadRequest);
            }
            catch (CryptographicException)
            {
                return Error("MailController.SmtpSettingsCannotDecryptPassword", StatusCodes.Status400BadRequest);
            }
            catch (Exception ex) when (ex.Message.Contains("key ring", StringComparison.OrdinalIgnoreCase))
            {
                return Error("MailController.SmtpSettingsCannotDecryptPassword", StatusCodes.Status400BadRequest);
            }

            if (string.IsNullOrWhiteSpace(smtp.Host) ||
                string.IsNullOrWhiteSpace(smtp.Username) ||
                string.IsNullOrWhiteSpace(smtp.Password) ||
                string.IsNullOrWhiteSpace(smtp.FromEmail))
            {
                return Error("MailController.SmtpSettingsIncomplete", StatusCodes.Status400BadRequest);
            }

            var to = dto.To;
            if (string.IsNullOrWhiteSpace(to))
            {
                var user = await _unitOfWork.Users.Query()
                    .AsNoTracking()
                    .Where(x => x.Id == userId && !x.IsDeleted)
                    .Select(x => new { x.Email })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (user == null || string.IsNullOrWhiteSpace(user.Email))
                {
                    return Error("UserService.UserNotFound", StatusCodes.Status404NotFound);
                }

                to = user.Email;
            }

            var subject = "SMTP Test Mail";
            var body = $"SMTP test email sent at {DateTime.UtcNow:O}";
            var ok = await SendEmailAsync(to, subject, body, false, null, null, null).ConfigureAwait(false);

            if (!ok)
            {
                return Error("MailController.TestMailSendFailed", StatusCodes.Status400BadRequest);
            }

            return ApiResponse<bool>.SuccessResult(
                true,
                _localizationService.GetLocalizedString("General.OperationSuccessful"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending SMTP test email");
            return ApiResponse<bool>.ErrorResult(
                _localizationService.GetLocalizedString("General.InternalServerError"),
                _localizationService.GetLocalizedString("General.InternalServerError"),
                StatusCodes.Status500InternalServerError);
        }
    }

    private ApiResponse<bool> Error(string key, int statusCode)
    {
        var message = _localizationService.GetLocalizedString(key);
        return ApiResponse<bool>.ErrorResult(message, message, statusCode);
    }
}
