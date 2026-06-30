using salesdesk_api.Modules.AccessControl.Domain.Entities;
using salesdesk_api.Modules.Identity.Domain.Entities;
using salesdesk_api.Modules.SmtpIntegration.Domain.Entities;
using salesdesk_api.Modules.System.Domain.Entities;
using salesdesk_api.Repositories;
using salesdesk_api.Shared.Domain.Entities;
using salesdesk_api.Shared.Domain.Entities.Common;
using salesdesk_api.Shared.Infrastructure.Monitoring;
using NotificationEntity = salesdesk_api.Modules.Notification.Domain.Entities.Notification;

namespace salesdesk_api.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<UserSession> UserSessions { get; }
        IGenericRepository<UserDetail> UserDetails { get; }
        IGenericRepository<PasswordResetRequest> PasswordResetRequests { get; }

        IGenericRepository<UserAuthority> UserAuthorities { get; }
        IGenericRepository<PermissionDefinition> PermissionDefinitions { get; }
        IGenericRepository<PermissionGroup> PermissionGroups { get; }
        IGenericRepository<PermissionGroupPermission> PermissionGroupPermissions { get; }
        IGenericRepository<UserPermissionGroup> UserPermissionGroups { get; }
        IGenericRepository<VisibilityPolicy> VisibilityPolicies { get; }
        IGenericRepository<UserVisibilityPolicy> UserVisibilityPolicies { get; }

        IGenericRepository<NotificationEntity> Notifications { get; }
        IGenericRepository<SmtpSetting> SmtpSettings { get; }
        IGenericRepository<SystemSetting> SystemSettings { get; }
        IGenericRepository<DocumentFieldLabel> DocumentFieldLabels { get; }
        IGenericRepository<JobFailureLog> JobFailureLogs { get; }
        IGenericRepository<JobExecutionLog> JobExecutionLogs { get; }
        IGenericRepository<AuditLog> AuditLogs { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        IGenericRepository<T> Repository<T>() where T : BaseEntity;
    }
}
