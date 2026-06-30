using salesdesk_api.Shared.Common.Application.Common;

namespace salesdesk_api.Shared.Infrastructure.Services.Localization;

/// <summary>Strings for shared encryption infrastructure (e.g. AES-GCM token helpers).</summary>
public sealed class SecurityEncryptionLocalizationResource : ILocalizationResource
{
    public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> MessagesByCulture { get; } =
        new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["en"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["AesGcmEncryptionService.InvalidEncryptedPayload"] = "Encrypted payload is invalid.",
                ["AesGcmEncryptionService.UnsupportedEncryptedPayloadVersion"] = "Encrypted payload version is not supported.",
            },
            ["tr"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["AesGcmEncryptionService.InvalidEncryptedPayload"] = "Şifreli veri geçersiz.",
                ["AesGcmEncryptionService.UnsupportedEncryptedPayloadVersion"] = "Şifreli veri sürümü desteklenmiyor.",
            },
        };
}
