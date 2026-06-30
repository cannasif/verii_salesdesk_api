namespace salesdesk_api.Shared.Common.Application.Common;

public interface ILocalizationResource
{
    IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> MessagesByCulture { get; }
}
