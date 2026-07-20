using AutoMapper;
using salesdesk_api.Modules.SmtpIntegration.Domain.Entities;
using salesdesk_api.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;

namespace salesdesk_api.Modules.SmtpIntegration.Application.Services;

public class SmtpSettingsService : ISmtpSettingsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILocalizationService _localizationService;
    private readonly IMemoryCache _cache;
    private readonly IDataProtector _protector;

    private const string CacheKey = "smtp_settings_runtime_v1";

    public SmtpSettingsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILocalizationService localizationService,
        IMemoryCache cache,
        IDataProtectionProvider dataProtectionProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localizationService = localizationService;
        _cache = cache;
        _protector = dataProtectionProvider.CreateProtector("smtp-settings-v1");
    }

    public void InvalidateCache()
    {
        _cache.Remove(CacheKey);
    }

    private static IQueryable<SmtpSetting> ActiveSettings(IQueryable<SmtpSetting> query) =>
        query.Where(x => !x.IsDeleted).OrderBy(x => x.Id);

    public async Task<ApiResponse<SmtpSettingsDto>> GetAsync()
    {
        try
        {
            var entity = await ActiveSettings(_unitOfWork.SmtpSettings.Query().AsNoTracking())
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (entity == null)
            {
                var dto = new SmtpSettingsDto
                {
                    Host = "",
                    Port = 587,
                    EnableSsl = true,
                    Username = "",
                    FromEmail = "",
                    FromName = "V3RII SalesDesk System",
                    Timeout = 30,
                    UpdatedAt = DateTime.UtcNow
                };

                return ApiResponse<SmtpSettingsDto>.SuccessResult(
                    dto,
                    _localizationService.GetLocalizedString("SmtpSettingsService.SmtpSettingsRetrievedDefault"));
            }

            var mapped = _mapper.Map<SmtpSettingsDto>(entity);

            return ApiResponse<SmtpSettingsDto>.SuccessResult(
                mapped,
                _localizationService.GetLocalizedString("SmtpSettingsService.SmtpSettingsRetrieved"));
        }
        catch (Exception ex)
        {
            return ApiResponse<SmtpSettingsDto>.ErrorResult(
                _localizationService.GetLocalizedString("SmtpSettingsService.InternalServerError"),
                _localizationService.GetLocalizedString("SmtpSettingsService.GetExceptionMessage", ex.Message, StatusCodes.Status500InternalServerError));
        }
    }

    public async Task<ApiResponse<SmtpSettingsDto>> UpdateAsync(UpdateSmtpSettingsDto dto, long userId)
    {
        try
        {
            // Id=1 ile INSERT SQL identity hatasi verir; mevcut satiri (silinmis dahil) guncelle veya identity ile ekle.
            var entity = await _unitOfWork.SmtpSettings
                .Query(tracking: true)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            var isNew = false;

            if (entity == null)
            {
                isNew = true;
                entity = new SmtpSetting
                {
                    IsDeleted = false,
                    CreatedDate = DateTimeProvider.Now,
                    CreatedBy = userId,
                };

                _mapper.Map(dto, entity);

                if (string.IsNullOrWhiteSpace(dto.Password))
                {
                    return ApiResponse<SmtpSettingsDto>.ErrorResult(
                        _localizationService.GetLocalizedString("MailController.SmtpSettingsIncomplete"),
                        _localizationService.GetLocalizedString("MailController.SmtpSettingsIncomplete"),
                        StatusCodes.Status400BadRequest);
                }

                entity.PasswordEncrypted = _protector.Protect(dto.Password);
                entity.UpdatedDate = DateTimeProvider.Now;
                entity.UpdatedBy = userId;

                await _unitOfWork.SmtpSettings.AddAsync(entity).ConfigureAwait(false);
            }
            else
            {
                entity.IsDeleted = false;
                entity.DeletedDate = null;
                entity.DeletedBy = null;

                _mapper.Map(dto, entity);

                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    entity.PasswordEncrypted = _protector.Protect(dto.Password);
                }
                else if (string.IsNullOrWhiteSpace(entity.PasswordEncrypted))
                {
                    return ApiResponse<SmtpSettingsDto>.ErrorResult(
                        _localizationService.GetLocalizedString("MailController.SmtpSettingsIncomplete"),
                        _localizationService.GetLocalizedString("MailController.SmtpSettingsIncomplete"),
                        StatusCodes.Status400BadRequest);
                }

                entity.UpdatedDate = DateTimeProvider.Now;
                entity.UpdatedBy = userId;

                await _unitOfWork.SmtpSettings.UpdateAsync(entity).ConfigureAwait(false);
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            InvalidateCache();

            var resultDto = _mapper.Map<SmtpSettingsDto>(entity);

            return ApiResponse<SmtpSettingsDto>.SuccessResult(
                resultDto,
                _localizationService.GetLocalizedString(
                    isNew
                        ? "SmtpSettingsService.SmtpSettingsCreated"
                        : "SmtpSettingsService.SmtpSettingsUpdated"));
        }
        catch (Exception ex)
        {
            var details = ex.InnerException?.Message;
            var message = string.IsNullOrWhiteSpace(details) ? ex.Message : $"{ex.Message} | {details}";

            return ApiResponse<SmtpSettingsDto>.ErrorResult(
                _localizationService.GetLocalizedString("SmtpSettingsService.InternalServerError"),
                _localizationService.GetLocalizedString("SmtpSettingsService.UpdateExceptionMessage", message, StatusCodes.Status500InternalServerError));
        }
    }

    public async Task<SmtpSettingsRuntimeDto> GetRuntimeAsync()
    {
        if (_cache.TryGetValue(CacheKey, out SmtpSettingsRuntimeDto? cached) && cached != null)
            return cached;

        var entity = await ActiveSettings(_unitOfWork.SmtpSettings.Query().AsNoTracking())
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (entity == null)
            throw new InvalidOperationException(
                _localizationService.GetLocalizedString("SmtpSettingsService.SmtpSettingsMissingInDatabase"));

        var password = string.IsNullOrWhiteSpace(entity.PasswordEncrypted)
            ? ""
            : _protector.Unprotect(entity.PasswordEncrypted);

        var runtime = new SmtpSettingsRuntimeDto
        {
            Host = entity.Host ?? "",
            Port = entity.Port,
            EnableSsl = entity.EnableSsl,
            Username = entity.Username ?? "",
            Password = password,
            FromEmail = entity.FromEmail ?? "",
            FromName = entity.FromName ?? "",
            Timeout = entity.Timeout
        };

        _cache.Set(CacheKey, runtime);

        return runtime;
    }

    public async Task<bool> IsRuntimeMailConfiguredAsync()
    {
        try
        {
            var smtp = await GetRuntimeAsync().ConfigureAwait(false);
            return !string.IsNullOrWhiteSpace(smtp.Host)
                   && !string.IsNullOrWhiteSpace(smtp.Username)
                   && !string.IsNullOrWhiteSpace(smtp.Password)
                   && !string.IsNullOrWhiteSpace(smtp.FromEmail);
        }
        catch (InvalidOperationException)
        {
            return false;
        }
        catch (CryptographicException)
        {
            return false;
        }
    }
}
