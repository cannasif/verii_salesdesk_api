using AutoMapper;
using salesdesk_api.Modules.System.Application.Dtos;
using salesdesk_api.Shared.Common.Application;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace salesdesk_api.Modules.System.Application.Services
{
    public class SystemSettingsService : ISystemSettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly IDocumentFieldLabelService _documentFieldLabelService;

        public SystemSettingsService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILocalizationService localizationService,
            IDocumentFieldLabelService documentFieldLabelService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizationService = localizationService;
            _documentFieldLabelService = documentFieldLabelService;
        }

        public async Task<ApiResponse<SystemSettingsDto>> GetAsync()
        {
            try
            {
                var entity = await _unitOfWork.SystemSettings
                    .Query()
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (entity == null)
                {
                    return ApiResponse<SystemSettingsDto>.SuccessResult(
                        new SystemSettingsDto(),
                        _localizationService.GetLocalizedString("SystemSettingsService.DefaultSystemSettingsReturned"));
                }

                return ApiResponse<SystemSettingsDto>.SuccessResult(
                    _mapper.Map<SystemSettingsDto>(entity),
                    _localizationService.GetLocalizedString("SystemSettingsService.SystemSettingsRetrieved"));
            }
            catch (Exception ex)
            {
                return ApiResponse<SystemSettingsDto>.ErrorResult(
                    _localizationService.GetLocalizedString("SystemSettingsService.InternalServerError"),
                    _localizationService.GetLocalizedString("SystemSettingsService.GetExceptionMessage", ex.Message),
                    StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResponse<SystemSettingsDto>> UpdateAsync(UpdateSystemSettingsDto dto, long userId)
        {
            try
            {
                NormalizeDto(dto);
                await _unitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                var entity = await _unitOfWork.SystemSettings
                    .Query()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (entity == null)
                {
                    entity = new Domain.Entities.SystemSetting
                    {
                        IsDeleted = false,
                        CreatedDate = DateTimeProvider.Now,
                        CreatedBy = userId
                    };

                    _mapper.Map(dto, entity);
                    entity.UpdatedDate = DateTimeProvider.Now;
                    entity.UpdatedBy = userId;

                    await _unitOfWork.SystemSettings.AddAsync(entity).ConfigureAwait(false);
                    await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                    var labelUpdateResponse = await UpdateDocumentFieldLabelsIfRequestedAsync(dto, userId).ConfigureAwait(false);
                    if (labelUpdateResponse != null)
                    {
                        await _unitOfWork.RollbackTransactionAsync().ConfigureAwait(false);
                        return labelUpdateResponse;
                    }

                    await _unitOfWork.CommitTransactionAsync().ConfigureAwait(false);

                    return ApiResponse<SystemSettingsDto>.SuccessResult(
                        _mapper.Map<SystemSettingsDto>(entity),
                        _localizationService.GetLocalizedString("SystemSettingsService.SystemSettingsCreated"));
                }

                _mapper.Map(dto, entity);
                entity.UpdatedDate = DateTimeProvider.Now;
                entity.UpdatedBy = userId;

                await _unitOfWork.SystemSettings.UpdateAsync(entity).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                var documentFieldLabelResponse = await UpdateDocumentFieldLabelsIfRequestedAsync(dto, userId).ConfigureAwait(false);
                if (documentFieldLabelResponse != null)
                {
                    await _unitOfWork.RollbackTransactionAsync().ConfigureAwait(false);
                    return documentFieldLabelResponse;
                }

                await _unitOfWork.CommitTransactionAsync().ConfigureAwait(false);

                return ApiResponse<SystemSettingsDto>.SuccessResult(
                    _mapper.Map<SystemSettingsDto>(entity),
                    _localizationService.GetLocalizedString("SystemSettingsService.SystemSettingsUpdated"));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync().ConfigureAwait(false);

                return ApiResponse<SystemSettingsDto>.ErrorResult(
                    _localizationService.GetLocalizedString("SystemSettingsService.InternalServerError"),
                    _localizationService.GetLocalizedString("SystemSettingsService.UpdateExceptionMessage", ex.Message),
                    StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<ApiResponse<SystemSettingsDto>?> UpdateDocumentFieldLabelsIfRequestedAsync(UpdateSystemSettingsDto dto, long userId)
        {
            if (dto.DocumentFieldLabels?.Items == null || dto.DocumentFieldLabels.Items.Count == 0)
            {
                return null;
            }

            var response = await _documentFieldLabelService.UpdateAsync(dto.DocumentFieldLabels, userId).ConfigureAwait(false);
            if (response.Success)
            {
                return null;
            }

            return ApiResponse<SystemSettingsDto>.ErrorResult(
                response.Message,
                response.ExceptionMessage,
                response.StatusCode);
        }

        private static void NormalizeDto(UpdateSystemSettingsDto dto)
        {
            dto.NumberFormat = NormalizeRequiredString(dto.NumberFormat, "tr-TR");
            dto.DecimalPlaces = Math.Clamp(dto.DecimalPlaces, 0, 6);
            dto.CurrencyCode = NormalizeRequiredString(dto.CurrencyCode, "TRY").ToUpperInvariant();
            dto.DefaultVatRate = Math.Clamp(dto.DefaultVatRate, 0m, 100m);
            dto.MaxGeneralDiscountRate = Math.Clamp(dto.MaxGeneralDiscountRate, 0m, 100m);
        }

        private static string NormalizeRequiredString(string? value, string fallback)
        {
            return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
        }

    }
}
