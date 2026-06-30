using Hangfire;
using Infrastructure.BackgroundJobs.Interfaces;
using salesdesk_api.Modules.SmtpIntegration.Application.Services;
using salesdesk_api.Shared.Infrastructure.Abstractions;

namespace Infrastructure.BackgroundJobs
{
    [DisableConcurrentExecution(timeoutInSeconds: 60)]
    [AutomaticRetry(Attempts = 3, DelaysInSeconds = new[] { 30, 60, 120 })]
    public class MailJob : IMailJob
    {
        private readonly IMailService _mailService;
        private readonly ILogger<MailJob> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILocalizationService _localizationService;

        public MailJob(
            IMailService mailService,
            ILogger<MailJob> logger,
            IConfiguration configuration,
            ILocalizationService localizationService)
        {
            _mailService = mailService;
            _logger = logger;
            _configuration = configuration;
            _localizationService = localizationService;
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, string? cc = null, string? bcc = null, List<string>? attachments = null)
        {
            try
            {
                _logger.LogInformation("MailJob: Sending email to {To} with subject: {Subject}", to, subject);

                var result = await _mailService.SendEmailAsync(to, subject, body, isHtml, cc, bcc, attachments);
                if (!result)
                {
                    _logger.LogWarning("MailJob: Failed to send email to {To}", to);
                    throw new InvalidOperationException(_localizationService.GetLocalizedString("MailJob.EmailSendFailed", to));
                }

                _logger.LogInformation("MailJob: Email sent successfully to {To}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MailJob: Error sending email to {To}", to);
                throw;
            }
        }

        public async Task SendEmailWithAttachmentsAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null, bool isHtml = true, string? cc = null, string? bcc = null, List<string>? attachments = null)
        {
            try
            {
                _logger.LogInformation("MailJob: Sending email to {To} with subject: {Subject}", to, subject);

                var result = await _mailService.SendEmailAsync(to, subject, body, fromEmail, fromName, isHtml, cc, bcc, attachments);
                if (!result)
                {
                    _logger.LogWarning("MailJob: Failed to send email to {To}", to);
                    throw new InvalidOperationException(_localizationService.GetLocalizedString("MailJob.EmailSendFailed", to));
                }

                _logger.LogInformation("MailJob: Email sent successfully to {To}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MailJob: Error sending email to {To}", to);
                throw;
            }
        }

        public async Task SendUserCreatedEmailAsync(string email, string username, string password, string? firstName, string? lastName, string baseUrl)
        {
            var effectiveBaseUrl = GetFrontendBaseUrl(baseUrl);
            var emailSubject = "Kullanıcınız oluşturulmuştur";
            var displayName = string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName)
                ? username
                : $"{firstName} {lastName}".Trim();

            var content = $@"
                <p>Sayın {displayName},</p>
                <p>Kullanıcınız başarıyla oluşturulmuştur. Giriş bilgileriniz aşağıdadır:</p>
                <div class=""info-box"">
                    <p><strong>Login için E-postanız:</strong> {email}</p>
                    <p><strong>Şifreniz:</strong> {password}</p>
                </div>
                <p>Yukarıdaki bilgilerle giriş yapıp menü üzerinden kullanıcı şifrenizi değiştirebilirsiniz.</p>
                <div style=""text-align: center; margin-top: 30px;"">
                    <a href=""{effectiveBaseUrl}"" class=""btn"">Giriş Yap</a>
                </div>";

            await SendEmailAsync(email, emailSubject, GetEmailTemplate("Kullanıcınız Oluşturuldu", content), true);
        }

        public async Task SendPasswordResetEmailAsync(string email, string fullName, string resetLink, string emailSubject)
        {
            var content = $@"
                <p>Sayın {fullName},</p>
                <p>Şifre sıfırlama talebiniz alınmıştır. Aşağıdaki butona tıklayarak şifrenizi sıfırlayabilirsiniz:</p>
                <div style=""text-align: center; margin: 30px 0;"">
                    <a href=""{resetLink}"" class=""btn"">Şifremi Sıfırla</a>
                </div>
                <p>Veya aşağıdaki linki tarayıcınıza kopyalayabilirsiniz:</p>
                <p style=""word-break: break-all; color: #fb923c; font-size: 14px;"">{resetLink}</p>
                <div style=""margin-top: 20px; padding-top: 20px; border-top: 1px solid rgba(255,255,255,0.1);"">
                    <p style=""font-size: 13px; color: #94a3b8; margin: 0;"">Bu link 30 dakika süreyle geçerlidir.</p>
                    <p style=""font-size: 13px; color: #94a3b8; margin: 5px 0 0 0;"">Eğer şifre sıfırlama talebinde bulunmadıysanız, lütfen bu e-postayı dikkate almayınız.</p>
                </div>";

            await SendEmailAsync(email, emailSubject, GetEmailTemplate("Şifre Sıfırlama Talebi", content), true);
        }

        public async Task SendPasswordChangedEmailAsync(string email, string displayName, string baseUrl)
        {
            var effectiveBaseUrl = GetFrontendBaseUrl(baseUrl);
            var content = $@"
                <p>Sayın {displayName},</p>
                <p>Eski şifreniz başarılı şekilde güncellenmiştir.</p>
                <p>Hesabınıza güvenli şekilde devam edebilirsiniz.</p>
                <div style=""text-align: center; margin-top: 30px;"">
                    <a href=""{effectiveBaseUrl}"" class=""btn"">Giriş Yap</a>
                </div>";

            await SendEmailAsync(email, "Şifreniz Güncellendi", GetEmailTemplate("Şifre Güncelleme Bildirimi", content), true);
        }

        public async Task SendPasswordResetCompletedEmailAsync(string email, string displayName, string baseUrl)
        {
            var effectiveBaseUrl = GetFrontendBaseUrl(baseUrl);
            var content = $@"
                <p>Sayın {displayName},</p>
                <p>Şifre resetleme işlemi başarılı şekilde tamamlanmıştır.</p>
                <p>Yeni şifreniz ile güvenli şekilde giriş yapabilirsiniz.</p>
                <div style=""text-align: center; margin-top: 30px;"">
                    <a href=""{effectiveBaseUrl}"" class=""btn"">Giriş Yap</a>
                </div>";

            await SendEmailAsync(email, "Şifre Sıfırlama İşlemi Tamamlandı", GetEmailTemplate("Şifre Sıfırlama Tamamlandı", content), true);
        }

        private string GetFrontendBaseUrl(string? fallback = null)
        {
            return (_configuration["FrontendSettings:BaseUrl"] ?? fallback ?? "http://localhost:5173").TrimEnd('/');
        }

        private static string GetEmailTemplate(string title, string content)
        {
            var year = DateTime.UtcNow.Year;
            return $@"
<!DOCTYPE html>
<html>
<head>
<meta charset=""utf-8"">
<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
<style>
    body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #090b14; margin: 0; padding: 0; color: #ffffff; }}
    .wrapper {{ width: 100%; table-layout: fixed; background-color: #090b14; padding-bottom: 40px; }}
    .container {{ max-width: 600px; margin: 0 auto; background-color: #111420; border-radius: 16px; border: 1px solid rgba(148,163,184,0.18); overflow: hidden; box-shadow: 0 20px 40px rgba(0,0,0,0.35); }}
    .header {{ padding: 36px 40px 18px 40px; text-align: center; background: linear-gradient(135deg, rgba(99,102,241,0.18), rgba(14,165,233,0.10)); }}
    .header h2 {{ margin: 0; font-size: 24px; font-weight: 700; color: #ffffff; }}
    .content {{ padding: 20px 40px 40px 40px; color: #e2e8f0; line-height: 1.6; font-size: 16px; }}
    .footer {{ padding: 20px; text-align: center; color: #64748b; font-size: 12px; border-top: 1px solid rgba(255,255,255,0.05); background-color: #080a12; }}
    .btn {{ display: inline-block; padding: 13px 28px; color: #ffffff !important; text-decoration: none; border-radius: 10px; font-weight: 700; margin: 10px 5px; background: linear-gradient(90deg, #6366f1, #8b5cf6); }}
    .info-box {{ background-color: rgba(0,0,0,0.24); padding: 18px; border-radius: 12px; margin: 20px 0; border: 1px solid rgba(255,255,255,0.1); }}
    strong {{ color: #a5b4fc; }}
    a {{ color: #a5b4fc; text-decoration: none; }}
    p {{ margin-bottom: 15px; }}
</style>
</head>
<body>
    <div class=""wrapper"">
        <br>
        <div class=""container"">
            <div class=""header""><h2>{title}</h2></div>
            <div class=""content"">{content}</div>
            <div class=""footer"">
                <p>Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayınız.</p>
                <p>&copy; {year} v3rii SalesDesk</p>
            </div>
        </div>
        <br>
    </div>
</body>
</html>";
        }
    }
}
