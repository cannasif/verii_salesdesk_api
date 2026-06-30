using Infrastructure.BackgroundJobs.Interfaces;
using salesdesk_api.Helpers;
using salesdesk_api.Modules.AccessControl.Application.Services;
using salesdesk_api.Modules.Audit.Application.Services;
using salesdesk_api.Modules.Identity.Application.Services;
using salesdesk_api.Modules.Notification.Application.Services;
using salesdesk_api.Modules.NetsisIntegrations.Application.Services;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Modules.SmtpIntegration.Application.Services;
using salesdesk_api.Modules.System.Application.Services;
using salesdesk_api.Repositories;
using salesdesk_api.Shared.Common.Application;
using salesdesk_api.Shared.Common.Application.Common;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.Shared.Infrastructure.Security;
using salesdesk_api.Shared.Infrastructure.Services.Auditing;
using salesdesk_api.Shared.Infrastructure.Services.Localization;
using salesdesk_api.UnitOfWork;

namespace salesdesk_api.Shared.Host.WebApi.Extensions;

public static class ModuleServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructureModule(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, salesdesk_api.UnitOfWork.UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IGeocodingService, GeocodingService>();
        services.AddScoped<IFileUploadService, FileUploadService>();
        services.AddScoped<IEncryptionService, AesGcmEncryptionService>();
        services.AddScoped<IAuditLogWriter, AuditLogWriter>();
        services.AddScoped<IAuditLogQueryService, AuditLogQueryService>();

        foreach (var resourceType in typeof(AssemblyMarker).Assembly
                     .GetTypes()
                     .Where(type => typeof(ILocalizationResource).IsAssignableFrom(type) && type is { IsClass: true, IsAbstract: false }))
        {
            services.AddSingleton(typeof(ILocalizationResource), resourceType);
        }

        services.AddSingleton<LocalizationRegistry>();
        services.AddScoped<ILocalizationService, PragmaticLocalizationService>();

        return services;
    }

    public static IServiceCollection AddIdentityModule(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IUserSessionService, UserSessionService>();
        services.AddScoped<IUserSessionCacheService, UserSessionCacheService>();
        services.AddScoped<IUserDetailService, UserDetailService>();

        return services;
    }

    public static IServiceCollection AddAccessControlModule(this IServiceCollection services)
    {
        services.AddScoped<IUserAuthorityService, UserAuthorityService>();
        services.AddScoped<IPermissionAccessService, PermissionAccessService>();
        services.AddScoped<IPermissionDefinitionService, PermissionDefinitionService>();
        services.AddScoped<IPermissionGroupService, PermissionGroupService>();
        services.AddScoped<IUserPermissionGroupService, UserPermissionGroupService>();
        services.AddScoped<IAccessControlRefreshNotifier, AccessControlRefreshNotifier>();
        services.AddScoped<IVisibilityPolicyService, VisibilityPolicyService>();
        services.AddScoped<IUserVisibilityPolicyService, UserVisibilityPolicyService>();
        services.AddScoped<IVisibilityAccessService, VisibilityAccessService>();
        return services;
    }

    public static IServiceCollection AddSalesDeskModule(this IServiceCollection services)
    {
        services.AddScoped<ISalesDeskService, SalesDeskService>();
        services.AddScoped<INetsisReadService, NetsisReadService>();

        return services;
    }

    public static IServiceCollection AddSmtpIntegrationModule(this IServiceCollection services)
    {
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<ISmtpSettingsService, SmtpSettingsService>();
        services.AddScoped<IMailJob, global::Infrastructure.BackgroundJobs.MailJob>();

        return services;
    }

    public static IServiceCollection AddNotificationModule(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }

    public static IServiceCollection AddSystemModule(this IServiceCollection services)
    {
        services.AddScoped<IHangfireDeadLetterJob, global::Infrastructure.BackgroundJobs.HangfireDeadLetterJob>();
        services.AddScoped<IHangfireMonitoringService, HangfireMonitoringService>();
        services.AddScoped<ISystemSettingsService, SystemSettingsService>();
        services.AddScoped<IDocumentFieldLabelService, DocumentFieldLabelService>();

        return services;
    }
}
