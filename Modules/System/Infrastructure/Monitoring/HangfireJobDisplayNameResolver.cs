namespace salesdesk_api.Modules.System.Infrastructure.Monitoring
{
    public static class HangfireJobDisplayNameResolver
    {
        private static readonly IReadOnlyDictionary<string, string> TypeNames =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["HangfireDeadLetterJob"] = "Başarısız job arşivleme",
                ["MailJob"] = "E-posta gönderimi",
            };

        private static readonly IReadOnlyDictionary<string, string> MethodNames =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["SendEmailAsync"] = "E-posta gönderimi",
                ["SendEmailWithAttachmentsAsync"] = "Ekli e-posta gönderimi",
                ["SendUserCreatedEmailAsync"] = "Yeni kullanıcı bilgilendirme e-postası",
                ["SendPasswordResetEmailAsync"] = "Şifre sıfırlama e-postası",
                ["SendPasswordResetCompletedEmailAsync"] = "Şifre sıfırlama tamamlandı e-postası",
                ["SendPasswordChangedEmailAsync"] = "Şifre değişikliği bilgilendirme e-postası",
                ["ProcessAsync"] = "Başarısız job arşivleme",
            };

        public static string Resolve(string? recurringJobId, Type? jobType = null, string? methodName = null, string? fallback = null)
        {
            if (!string.IsNullOrWhiteSpace(methodName) &&
                MethodNames.TryGetValue(methodName, out var methodDisplayName))
            {
                return methodDisplayName;
            }

            var typeName = jobType?.Name;
            if (!string.IsNullOrWhiteSpace(typeName) &&
                TypeNames.TryGetValue(typeName, out var typeDisplayName))
            {
                return typeDisplayName;
            }

            return Resolve(recurringJobId, fallback);
        }

        public static string Resolve(string? recurringJobId, string? rawJobName)
        {
            if (string.IsNullOrWhiteSpace(rawJobName))
            {
                return string.IsNullOrWhiteSpace(recurringJobId) ? "Bilinmeyen job" : recurringJobId;
            }

            foreach (var method in MethodNames)
            {
                if (rawJobName.Contains(method.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return method.Value;
                }
            }

            foreach (var type in TypeNames)
            {
                if (rawJobName.Contains(type.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return type.Value;
                }
            }

            return rawJobName;
        }
    }
}
