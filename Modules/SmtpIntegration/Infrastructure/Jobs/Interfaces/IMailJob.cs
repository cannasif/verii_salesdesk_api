using Hangfire;

namespace Infrastructure.BackgroundJobs.Interfaces
{
    public interface IMailJob
    {
        [JobDisplayName("E-posta gönderimi")]
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, string? cc = null, string? bcc = null, List<string>? attachments = null);

        [JobDisplayName("Ekli e-posta gönderimi")]
        Task SendEmailWithAttachmentsAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null, bool isHtml = true, string? cc = null, string? bcc = null, List<string>? attachments = null);

        [JobDisplayName("Yeni kullanıcı bilgilendirme e-postası")]
        Task SendUserCreatedEmailAsync(string email, string username, string password, string? firstName, string? lastName, string baseUrl);

        [JobDisplayName("Şifre sıfırlama e-postası")]
        Task SendPasswordResetEmailAsync(string email, string fullName, string resetLink, string emailSubject);

        [JobDisplayName("Şifre sıfırlama tamamlandı e-postası")]
        Task SendPasswordResetCompletedEmailAsync(string email, string displayName, string baseUrl);

        [JobDisplayName("Şifre değişikliği bilgilendirme e-postası")]
        Task SendPasswordChangedEmailAsync(string email, string displayName, string baseUrl);
    }
}
