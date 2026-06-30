using salesdesk_api.Modules.System.Application.Dtos;
using salesdesk_api.Modules.System.Domain.Entities;
using salesdesk_api.Shared.Common.Application;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.System.Application.Services
{
    public class DocumentFieldLabelService : IDocumentFieldLabelService
    {
        private const string HeaderNoteScope = "HeaderNote";
        private const string LineDescriptionScope = "LineDescription";

        private static readonly string[] SupportedDocumentTypes = ["SalesDeskQuote", "SalesDeskInvoice"];
        private static readonly string[] SupportedScopes = [HeaderNoteScope, LineDescriptionScope];

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizationService _localizationService;

        public DocumentFieldLabelService(
            IUnitOfWork unitOfWork,
            ILocalizationService localizationService)
        {
            _unitOfWork = unitOfWork;
            _localizationService = localizationService;
        }

        public async Task<ApiResponse<List<DocumentFieldLabelDto>>> GetAsync(string? documentType = null, string? scope = null)
        {
            try
            {
                await EnsureDefaultsAsync().ConfigureAwait(false);

                var normalizedDocumentType = NormalizeDocumentType(documentType, allowEmpty: true);
                var normalizedScope = NormalizeScope(scope, allowEmpty: true);

                if (normalizedDocumentType == null || normalizedScope == null)
                {
                    return ApiResponse<List<DocumentFieldLabelDto>>.ErrorResult(
                        "Geçersiz belge alanı filtresi.",
                        "DocumentType veya scope değeri desteklenmiyor.",
                        StatusCodes.Status400BadRequest);
                }

                var query = _unitOfWork.Repository<DocumentFieldLabel>()
                    .Query()
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted);

                if (!string.IsNullOrWhiteSpace(normalizedDocumentType))
                {
                    query = query.Where(x => x.DocumentType == normalizedDocumentType);
                }

                if (!string.IsNullOrWhiteSpace(normalizedScope))
                {
                    query = query.Where(x => x.Scope == normalizedScope);
                }

                var items = await query
                    .OrderBy(x => x.DocumentType)
                    .ThenBy(x => x.Scope)
                    .ThenBy(x => x.SortOrder)
                    .ThenBy(x => x.Id)
                    .Select(x => ToDto(x))
                    .ToListAsync()
                    .ConfigureAwait(false);

                return ApiResponse<List<DocumentFieldLabelDto>>.SuccessResult(
                    items,
                    "Belge alan başlıkları getirildi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<DocumentFieldLabelDto>>.ErrorResult(
                    _localizationService.GetLocalizedString("SystemSettingsService.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<List<DocumentFieldLabelDto>>> UpdateAsync(UpdateDocumentFieldLabelsRequest request, long userId)
        {
            try
            {
                await EnsureDefaultsAsync().ConfigureAwait(false);

                if (request.Items.Count == 0)
                {
                    return ApiResponse<List<DocumentFieldLabelDto>>.ErrorResult(
                        "Kaydedilecek belge alan başlığı bulunamadı.",
                        "Items listesi boş gönderildi.",
                        StatusCodes.Status400BadRequest);
                }

                var updates = request.Items
                    .Select(NormalizeUpdate)
                    .ToList();

                if (updates.Any(x => x == null))
                {
                    return ApiResponse<List<DocumentFieldLabelDto>>.ErrorResult(
                        "Geçersiz belge alanı bilgisi.",
                        "DocumentType, scope veya fieldKey desteklenmiyor.",
                        StatusCodes.Status400BadRequest);
                }

                var normalizedUpdates = updates.OfType<NormalizedUpdate>().ToList();
                var updateKeys = normalizedUpdates
                    .Select(x => $"{x.DocumentType}|{x.Scope}|{x.FieldKey}")
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                if (updateKeys.Count != normalizedUpdates.Count)
                {
                    return ApiResponse<List<DocumentFieldLabelDto>>.ErrorResult(
                        "Aynı alan birden fazla gönderilemez.",
                        "Tekrarlı belge alanı başlığı bulundu.",
                        StatusCodes.Status400BadRequest);
                }

                var existingItems = await _unitOfWork.Repository<DocumentFieldLabel>()
                    .Query(tracking: true)
                    .Where(x => !x.IsDeleted)
                    .ToListAsync()
                    .ConfigureAwait(false);

                foreach (var update in normalizedUpdates)
                {
                    var entity = existingItems.FirstOrDefault(x =>
                        x.DocumentType == update.DocumentType &&
                        x.Scope == update.Scope &&
                        x.FieldKey == update.FieldKey);

                    if (entity == null)
                    {
                        var definition = GetDefaultDefinitions().First(x =>
                            x.DocumentType == update.DocumentType &&
                            x.Scope == update.Scope &&
                            x.FieldKey == update.FieldKey);

                        entity = new DocumentFieldLabel
                        {
                            DocumentType = definition.DocumentType,
                            Scope = definition.Scope,
                            FieldKey = definition.FieldKey,
                            DefaultLabel = definition.DefaultLabel,
                            HelpText = definition.HelpText,
                            Placeholder = definition.Placeholder,
                            SortOrder = definition.SortOrder,
                            IsActive = true,
                            CreatedBy = userId,
                            CreatedDate = DateTimeProvider.Now,
                        };

                        await _unitOfWork.Repository<DocumentFieldLabel>().AddAsync(entity).ConfigureAwait(false);
                        existingItems.Add(entity);
                    }

                    entity.CustomLabel = NormalizeOptional(update.CustomLabel, 80);
                    entity.HelpText = NormalizeOptional(update.HelpText, 500) ?? entity.HelpText;
                    entity.Placeholder = NormalizeOptional(update.Placeholder, 120) ?? entity.Placeholder;
                    entity.IsActive = update.IsActive;
                    entity.UpdatedBy = userId;
                    entity.UpdatedDate = DateTimeProvider.Now;
                }

                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                var labelsResponse = await GetAsync().ConfigureAwait(false);
                if (!labelsResponse.Success)
                {
                    return labelsResponse;
                }

                return ApiResponse<List<DocumentFieldLabelDto>>.SuccessResult(
                    labelsResponse.Data,
                    "Belge alan başlıkları güncellendi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<DocumentFieldLabelDto>>.ErrorResult(
                    _localizationService.GetLocalizedString("SystemSettingsService.InternalServerError"),
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }

        private async Task EnsureDefaultsAsync()
        {
            var existingKeys = await _unitOfWork.Repository<DocumentFieldLabel>()
                .Query()
                .Where(x => !x.IsDeleted)
                .Select(x => x.DocumentType + "|" + x.Scope + "|" + x.FieldKey)
                .ToListAsync()
                .ConfigureAwait(false);

            var existingKeySet = existingKeys.ToHashSet(StringComparer.OrdinalIgnoreCase);
            var missingDefaults = GetDefaultDefinitions()
                .Where(x => !existingKeySet.Contains(x.DocumentType + "|" + x.Scope + "|" + x.FieldKey))
                .ToList();

            if (missingDefaults.Count == 0)
            {
                return;
            }

            foreach (var definition in missingDefaults)
            {
                await _unitOfWork.Repository<DocumentFieldLabel>().AddAsync(new DocumentFieldLabel
                {
                    DocumentType = definition.DocumentType,
                    Scope = definition.Scope,
                    FieldKey = definition.FieldKey,
                    DefaultLabel = definition.DefaultLabel,
                    HelpText = definition.HelpText,
                    Placeholder = definition.Placeholder,
                    SortOrder = definition.SortOrder,
                    IsActive = true,
                    CreatedDate = DateTimeProvider.Now,
                }).ConfigureAwait(false);
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        private static List<DocumentFieldDefinition> GetDefaultDefinitions()
        {
            var definitions = new List<DocumentFieldDefinition>();

            foreach (var documentType in SupportedDocumentTypes)
            {
                var documentLabel = documentType switch
                {
                    "SalesDeskQuote" => "Teklif Notu",
                    "SalesDeskInvoice" => "Fatura Notu",
                    _ => "Belge Notu"
                };

                for (var i = 1; i <= 15; i++)
                {
                    definitions.Add(new DocumentFieldDefinition(
                        documentType,
                        HeaderNoteScope,
                        $"Note{i}",
                        $"{documentLabel} {i}",
                        "Belge geneli not alanı.",
                        "Not girin...",
                        i));
                }

                definitions.Add(new DocumentFieldDefinition(
                    documentType,
                    LineDescriptionScope,
                    "Description1",
                    "Açıklama 1",
                    "Satır bazlı açıklama alanı.",
                    "Satır açıklaması girin...",
                    1));
                definitions.Add(new DocumentFieldDefinition(
                    documentType,
                    LineDescriptionScope,
                    "Description2",
                    "Açıklama 2",
                    "Satır bazlı ek açıklama alanı.",
                    "Satır açıklaması girin...",
                    2));
                definitions.Add(new DocumentFieldDefinition(
                    documentType,
                    LineDescriptionScope,
                    "Description3",
                    "Açıklama 3",
                    "Satır bazlı iç not alanı.",
                    "Satır açıklaması girin...",
                    3));
            }

            return definitions;
        }

        private static DocumentFieldLabelDto ToDto(DocumentFieldLabel entity)
        {
            var customLabel = NormalizeOptional(entity.CustomLabel, 80);

            return new DocumentFieldLabelDto
            {
                Id = entity.Id,
                DocumentType = entity.DocumentType,
                Scope = entity.Scope,
                FieldKey = entity.FieldKey,
                DefaultLabel = entity.DefaultLabel,
                CustomLabel = customLabel,
                EffectiveLabel = customLabel ?? entity.DefaultLabel,
                HelpText = entity.HelpText,
                Placeholder = entity.Placeholder,
                SortOrder = entity.SortOrder,
                IsActive = entity.IsActive,
            };
        }

        private static NormalizedUpdate? NormalizeUpdate(UpdateDocumentFieldLabelDto dto)
        {
            var documentType = NormalizeDocumentType(dto.DocumentType);
            var scope = NormalizeScope(dto.Scope);
            var fieldKey = NormalizeFieldKey(scope, dto.FieldKey);

            if (documentType == null || scope == null || fieldKey == null)
            {
                return null;
            }

            return new NormalizedUpdate(
                documentType,
                scope,
                fieldKey,
                dto.CustomLabel,
                dto.HelpText,
                dto.Placeholder,
                dto.IsActive);
        }

        private static string? NormalizeDocumentType(string? value, bool allowEmpty = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return allowEmpty ? string.Empty : null;
            }

            return SupportedDocumentTypes.FirstOrDefault(x => x.Equals(value.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private static string? NormalizeScope(string? value, bool allowEmpty = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return allowEmpty ? string.Empty : null;
            }

            return SupportedScopes.FirstOrDefault(x => x.Equals(value.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private static string? NormalizeFieldKey(string? scope, string? value)
        {
            if (scope == null || string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var trimmedValue = value.Trim();

            if (scope == HeaderNoteScope)
            {
                return Enumerable.Range(1, 15)
                    .Select(x => $"Note{x}")
                    .FirstOrDefault(x => x.Equals(trimmedValue, StringComparison.OrdinalIgnoreCase));
            }

            return new[] { "Description1", "Description2", "Description3" }
                .FirstOrDefault(x => x.Equals(trimmedValue, StringComparison.OrdinalIgnoreCase));
        }

        private static string? NormalizeOptional(string? value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var trimmed = value.Trim();
            return trimmed.Length <= maxLength ? trimmed : trimmed[..maxLength];
        }

        private sealed record DocumentFieldDefinition(
            string DocumentType,
            string Scope,
            string FieldKey,
            string DefaultLabel,
            string HelpText,
            string Placeholder,
            int SortOrder);

        private sealed record NormalizedUpdate(
            string DocumentType,
            string Scope,
            string FieldKey,
            string? CustomLabel,
            string? HelpText,
            string? Placeholder,
            bool IsActive);
    }
}
