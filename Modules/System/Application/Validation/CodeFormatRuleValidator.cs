using System.Text;
using System.Text.RegularExpressions;

namespace salesdesk_api.Modules.System.Application.Validation
{
    public static class CodeFormatRuleValidator
    {
        public const string MaskLegend = "9=rakam, A=harf, X=harf/rakam.";

        public static CodeFormatRuleValidationResult Validate(
            string? code,
            bool isEnabled,
            string? mask,
            string? example,
            string? errorMessage,
            string fieldDisplayName)
        {
            if (!isEnabled || string.IsNullOrWhiteSpace(mask) || string.IsNullOrWhiteSpace(code))
            {
                return CodeFormatRuleValidationResult.Valid();
            }

            var regexPattern = BuildRegexPattern(mask);
            var normalizedCode = code.Trim();

            if (Regex.IsMatch(normalizedCode, regexPattern, RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100)))
            {
                return CodeFormatRuleValidationResult.Valid();
            }

            var message = string.IsNullOrWhiteSpace(errorMessage)
                ? BuildDefaultErrorMessage(fieldDisplayName, mask, example)
                : errorMessage.Trim();

            return CodeFormatRuleValidationResult.Invalid(message);
        }

        private static string BuildRegexPattern(string mask)
        {
            var builder = new StringBuilder("^");

            foreach (var character in mask.Trim())
            {
                builder.Append(character switch
                {
                    '9' => "[0-9]",
                    'A' => "\\p{L}",
                    'X' => "(?:[0-9]|\\p{L})",
                    _ => Regex.Escape(character.ToString())
                });
            }

            builder.Append('$');
            return builder.ToString();
        }

        private static string BuildDefaultErrorMessage(string fieldDisplayName, string mask, string? example)
        {
            var formatInfo = string.IsNullOrWhiteSpace(example)
                ? mask.Trim()
                : example.Trim();

            return $"{fieldDisplayName} {formatInfo} formatında olmalıdır. Kural: {MaskLegend}";
        }
    }

    public sealed record CodeFormatRuleValidationResult(bool IsValid, string? ErrorMessage)
    {
        public static CodeFormatRuleValidationResult Valid() => new(true, null);
        public static CodeFormatRuleValidationResult Invalid(string errorMessage) => new(false, errorMessage);
    }
}
