using salesdesk_api.Shared.Common.Application.Common;

namespace salesdesk_api.Modules.System.Localization;

public sealed class SystemLocalizationResource : ILocalizationResource
{
    public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> MessagesByCulture { get; } =
        new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["en"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["DashboardController.DashboardRetrieved"] = "Dashboard data retrieved successfully.",
                ["HangfireController.JobIdRequired"] = "Job id is required.",
                ["HangfireController.RecurringJobNotFound"] = "Recurring job not found.",
                ["HangfireController.RecurringJobTriggered"] = "Recurring job triggered successfully.",
                ["MailJob.EmailSendFailed"] = "Email could not be sent: {0}",
            },
            ["tr"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["DashboardController.DashboardRetrieved"] = "Dashboard verileri başarıyla getirildi.",
                ["HangfireController.JobIdRequired"] = "Job id zorunludur.",
                ["HangfireController.RecurringJobNotFound"] = "Zamanlanmış job bulunamadı.",
                ["HangfireController.RecurringJobTriggered"] = "Zamanlanmış job başarıyla tetiklendi.",
                ["MailJob.EmailSendFailed"] = "E-posta gönderilemedi: {0}",
            },
        };
}
