using salesdesk_api.Shared.Common.Application.Common;

namespace salesdesk_api.Modules.SmtpIntegration.Localization;

/// <summary>
/// Localization keys for SMTP mail settings and background mail jobs.
/// </summary>
public sealed class SmtpIntegrationLocalizationResource : ILocalizationResource
{
    public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> MessagesByCulture { get; } =
        new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["en"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["MailController.SmtpSettingsCannotDecryptPassword"] = "SMTP password could not be decrypted (the server key may have changed). Please re-save the password in Mail Settings.",
                ["MailController.SmtpSettingsIncomplete"] = "SMTP settings are incomplete. Please fill Host, Username, Password and From Email.",
                ["MailController.SmtpSettingsMissing"] = "SMTP settings were not found. Please configure them in Mail Settings.",
                ["MailController.TestMailSendFailed"] = "Test email could not be sent. Please check your SMTP settings.",
                ["MailJob.EmailSendFailed"] = "Failed to send email to {0}.",
                ["SmtpSettingsService.GetExceptionMessage"] = "An error occurred during the operation: {0}",
                ["SmtpSettingsService.InternalServerError"] = "An internal server error occurred.",
                ["SmtpSettingsService.SmtpSettingsCreated"] = "Record created successfully.",
                ["SmtpSettingsService.SmtpSettingsMissingInDatabase"] = "SMTP settings could not be found in the database.",
                ["SmtpSettingsService.SmtpSettingsRetrieved"] = "Record retrieved successfully.",
                ["SmtpSettingsService.SmtpSettingsRetrievedDefault"] = "Record retrieved successfully.",
                ["SmtpSettingsService.SmtpSettingsUpdated"] = "Record updated successfully.",
                ["SmtpSettingsService.UpdateExceptionMessage"] = "An error occurred during the operation: {0}",
            },
            ["tr"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["MailController.SmtpSettingsCannotDecryptPassword"] = "SMTP \u015fifresi \u00e7\u00f6z\u00fcmlenemedi (sunucu anahtar\u0131 de\u011fi\u015fmi\u015f olabilir). L\u00fctfen Mail Ayarlar\u0131 ekran\u0131ndan \u015fifreyi tekrar kaydedin.",
                ["MailController.SmtpSettingsIncomplete"] = "SMTP ayarlar\u0131 eksik. Host, Kullan\u0131c\u0131 Ad\u0131, \u015eifre ve G\u00f6nderen E-posta alanlar\u0131n\u0131 doldurun.",
                ["MailController.SmtpSettingsMissing"] = "SMTP ayarlar\u0131 bulunamad\u0131. L\u00fctfen Mail Ayarlar\u0131 ekran\u0131ndan yap\u0131land\u0131r\u0131n.",
                ["MailController.TestMailSendFailed"] = "Test maili g\u00f6nderilemedi. SMTP ayarlar\u0131n\u0131z\u0131 kontrol edin.",
                ["MailJob.EmailSendFailed"] = "E-posta g\u00f6nderilemedi: {0}",
                ["SmtpSettingsService.GetExceptionMessage"] = "\u0130\u015flem s\u0131ras\u0131nda bir hata olu\u015ftu: {0}",
                ["SmtpSettingsService.InternalServerError"] = "Sunucu taraf\u0131nda bir hata olu\u015ftu.",
                ["SmtpSettingsService.SmtpSettingsCreated"] = "SMTP ayarlar\u0131 ba\u015far\u0131yla olu\u015fturuldu.",
                ["SmtpSettingsService.SmtpSettingsMissingInDatabase"] = "SMTP ayarlar\u0131 veritaban\u0131nda bulunamad\u0131.",
                ["SmtpSettingsService.SmtpSettingsRetrieved"] = "SMTP ayarlar\u0131 ba\u015far\u0131yla getirildi.",
                ["SmtpSettingsService.SmtpSettingsRetrievedDefault"] = "SMTP ayarlar\u0131 bulunamad\u0131, varsay\u0131lan de\u011ferler getirildi.",
                ["SmtpSettingsService.SmtpSettingsUpdated"] = "SMTP ayarlar\u0131 ba\u015far\u0131yla g\u00fcncellendi.",
                ["SmtpSettingsService.UpdateExceptionMessage"] = "\u0130\u015flem s\u0131ras\u0131nda bir hata olu\u015ftu: {0}",
            },
            ["de"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["MailController.SmtpSettingsCannotDecryptPassword"] = "SMTP password could not be decrypted (the server key may have changed). Please re-save the password in Mail Settings.",
                ["MailController.SmtpSettingsIncomplete"] = "SMTP settings are incomplete. Please fill Host, Username, Password and From Email.",
                ["MailController.SmtpSettingsMissing"] = "SMTP settings were not found. Please configure them in Mail Settings.",
                ["MailController.TestMailSendFailed"] = "Test email could not be sent. Please check your SMTP settings.",
                ["MailJob.EmailSendFailed"] = "Failed to send email to {0}.",
                ["SmtpSettingsService.GetExceptionMessage"] = "Beim Vorgang ist ein Fehler aufgetreten: {0}",
                ["SmtpSettingsService.InternalServerError"] = "Ein interner Serverfehler ist aufgetreten.",
                ["SmtpSettingsService.SmtpSettingsCreated"] = "Datensatz erfolgreich erstellt.",
                ["SmtpSettingsService.SmtpSettingsMissingInDatabase"] = "SMTP settings could not be found in the database.",
                ["SmtpSettingsService.SmtpSettingsRetrieved"] = "Datensatz erfolgreich abgerufen.",
                ["SmtpSettingsService.SmtpSettingsRetrievedDefault"] = "Datensatz erfolgreich abgerufen.",
                ["SmtpSettingsService.SmtpSettingsUpdated"] = "Datensatz erfolgreich aktualisiert.",
                ["SmtpSettingsService.UpdateExceptionMessage"] = "Beim Vorgang ist ein Fehler aufgetreten: {0}",
            },
            ["fr"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["MailController.SmtpSettingsCannotDecryptPassword"] = "SMTP password could not be decrypted (the server key may have changed). Please re-save the password in Mail Settings.",
                ["MailController.SmtpSettingsIncomplete"] = "SMTP settings are incomplete. Please fill Host, Username, Password and From Email.",
                ["MailController.SmtpSettingsMissing"] = "SMTP settings were not found. Please configure them in Mail Settings.",
                ["MailController.TestMailSendFailed"] = "Test email could not be sent. Please check your SMTP settings.",
                ["MailJob.EmailSendFailed"] = "Failed to send email to {0}.",
                ["SmtpSettingsService.GetExceptionMessage"] = "Une erreur s'est produite pendant l'op\u00e9ration : {0}",
                ["SmtpSettingsService.InternalServerError"] = "Une erreur interne du serveur est survenue.",
                ["SmtpSettingsService.SmtpSettingsCreated"] = "Enregistrement cr\u00e9\u00e9 avec succ\u00e8s.",
                ["SmtpSettingsService.SmtpSettingsMissingInDatabase"] = "SMTP settings could not be found in the database.",
                ["SmtpSettingsService.SmtpSettingsRetrieved"] = "Enregistrement r\u00e9cup\u00e9r\u00e9 avec succ\u00e8s.",
                ["SmtpSettingsService.SmtpSettingsRetrievedDefault"] = "Enregistrement r\u00e9cup\u00e9r\u00e9 avec succ\u00e8s.",
                ["SmtpSettingsService.SmtpSettingsUpdated"] = "Enregistrement mis \u00e0 jour avec succ\u00e8s.",
                ["SmtpSettingsService.UpdateExceptionMessage"] = "Une erreur s'est produite pendant l'op\u00e9ration : {0}",
            },
            ["es"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["MailController.SmtpSettingsCannotDecryptPassword"] = "SMTP password could not be decrypted (the server key may have changed). Please re-save the password in Mail Settings.",
                ["MailController.SmtpSettingsIncomplete"] = "SMTP settings are incomplete. Please fill Host, Username, Password and From Email.",
                ["MailController.SmtpSettingsMissing"] = "SMTP settings were not found. Please configure them in Mail Settings.",
                ["MailController.TestMailSendFailed"] = "Test email could not be sent. Please check your SMTP settings.",
                ["MailJob.EmailSendFailed"] = "Failed to send email to {0}.",
                ["SmtpSettingsService.GetExceptionMessage"] = "Se produjo un error durante la operaci\u00f3n: {0}",
                ["SmtpSettingsService.InternalServerError"] = "Se produjo un error interno del servidor.",
                ["SmtpSettingsService.SmtpSettingsCreated"] = "Registro creado correctamente.",
                ["SmtpSettingsService.SmtpSettingsMissingInDatabase"] = "SMTP settings could not be found in the database.",
                ["SmtpSettingsService.SmtpSettingsRetrieved"] = "Registro recuperado correctamente.",
                ["SmtpSettingsService.SmtpSettingsRetrievedDefault"] = "Registro recuperado correctamente.",
                ["SmtpSettingsService.SmtpSettingsUpdated"] = "Registro actualizado correctamente.",
                ["SmtpSettingsService.UpdateExceptionMessage"] = "Se produjo un error durante la operaci\u00f3n: {0}",
            },
            ["it"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["MailController.SmtpSettingsCannotDecryptPassword"] = "SMTP password could not be decrypted (the server key may have changed). Please re-save the password in Mail Settings.",
                ["MailController.SmtpSettingsIncomplete"] = "SMTP settings are incomplete. Please fill Host, Username, Password and From Email.",
                ["MailController.SmtpSettingsMissing"] = "SMTP settings were not found. Please configure them in Mail Settings.",
                ["MailController.TestMailSendFailed"] = "Test email could not be sent. Please check your SMTP settings.",
                ["MailJob.EmailSendFailed"] = "Failed to send email to {0}.",
                ["SmtpSettingsService.GetExceptionMessage"] = "Si \u00e8 verificato un errore durante l'operazione: {0}",
                ["SmtpSettingsService.InternalServerError"] = "Si \u00e8 verificato un errore interno del server.",
                ["SmtpSettingsService.SmtpSettingsCreated"] = "Record creato con successo.",
                ["SmtpSettingsService.SmtpSettingsMissingInDatabase"] = "SMTP settings could not be found in the database.",
                ["SmtpSettingsService.SmtpSettingsRetrieved"] = "Record recuperato con successo.",
                ["SmtpSettingsService.SmtpSettingsRetrievedDefault"] = "Record recuperato con successo.",
                ["SmtpSettingsService.SmtpSettingsUpdated"] = "Record aggiornato con successo.",
                ["SmtpSettingsService.UpdateExceptionMessage"] = "Si \u00e8 verificato un errore durante l'operazione: {0}",
            },
            ["ar"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["MailController.SmtpSettingsCannotDecryptPassword"] = "SMTP password could not be decrypted (the server key may have changed). Please re-save the password in Mail Settings.",
                ["MailController.SmtpSettingsIncomplete"] = "SMTP settings are incomplete. Please fill Host, Username, Password and From Email.",
                ["MailController.SmtpSettingsMissing"] = "SMTP settings were not found. Please configure them in Mail Settings.",
                ["MailController.TestMailSendFailed"] = "Test email could not be sent. Please check your SMTP settings.",
                ["MailJob.EmailSendFailed"] = "Failed to send email to {0}.",
                ["SmtpSettingsService.GetExceptionMessage"] = "\u062d\u062f\u062b \u062e\u0637\u0623 \u0623\u062b\u0646\u0627\u0621 \u0627\u0644\u0639\u0645\u0644\u064a\u0629: {0}",
                ["SmtpSettingsService.InternalServerError"] = "\u062d\u062f\u062b \u062e\u0637\u0623 \u062f\u0627\u062e\u0644\u064a \u0641\u064a \u0627\u0644\u062e\u0627\u062f\u0645.",
                ["SmtpSettingsService.SmtpSettingsCreated"] = "\u062a\u0645 \u0625\u0646\u0634\u0627\u0621 \u0627\u0644\u0633\u062c\u0644 \u0628\u0646\u062c\u0627\u062d.",
                ["SmtpSettingsService.SmtpSettingsMissingInDatabase"] = "SMTP settings could not be found in the database.",
                ["SmtpSettingsService.SmtpSettingsRetrieved"] = "\u062a\u0645 \u062c\u0644\u0628 \u0627\u0644\u0633\u062c\u0644 \u0628\u0646\u062c\u0627\u062d.",
                ["SmtpSettingsService.SmtpSettingsRetrievedDefault"] = "\u062a\u0645 \u062c\u0644\u0628 \u0627\u0644\u0633\u062c\u0644 \u0628\u0646\u062c\u0627\u062d.",
                ["SmtpSettingsService.SmtpSettingsUpdated"] = "\u062a\u0645 \u062a\u062d\u062f\u064a\u062b \u0627\u0644\u0633\u062c\u0644 \u0628\u0646\u062c\u0627\u062d.",
                ["SmtpSettingsService.UpdateExceptionMessage"] = "\u062d\u062f\u062b \u062e\u0637\u0623 \u0623\u062b\u0646\u0627\u0621 \u0627\u0644\u0639\u0645\u0644\u064a\u0629: {0}",
            },
        };
}
