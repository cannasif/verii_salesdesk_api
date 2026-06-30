using AutoMapper;
using salesdesk_api.Modules.SmtpIntegration.Application.Dtos;
using salesdesk_api.Modules.SmtpIntegration.Domain.Entities;

namespace salesdesk_api.Modules.SmtpIntegration.Application.Mappings;

public class SmtpSettingsMappingProfile : Profile
{
    public SmtpSettingsMappingProfile()
    {
        CreateMap<SmtpSetting, SmtpSettingsDto>();

        CreateMap<UpdateSmtpSettingsDto, SmtpSetting>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordEncrypted, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
    }
}
