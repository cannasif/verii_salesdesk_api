using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using salesdesk_api.Modules.AccessControl.Domain.Entities;
using salesdesk_api.Modules.Identity.Domain.Entities;
using salesdesk_api.Modules.SmtpIntegration.Domain.Entities;
using salesdesk_api.Modules.System.Domain.Entities;
using salesdesk_api.Repositories;
using salesdesk_api.Shared.Domain.Entities;
using salesdesk_api.Shared.Domain.Entities.Common;
using salesdesk_api.Shared.Infrastructure.Abstractions;
using salesdesk_api.Shared.Infrastructure.Monitoring;
using salesdesk_api.Shared.Infrastructure.Persistence;
using NotificationEntity = salesdesk_api.Modules.Notification.Domain.Entities.Notification;

namespace salesdesk_api.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesDeskDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILocalizationService _localizationService;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        private IGenericRepository<User>? _users;
        private IGenericRepository<UserSession>? _userSessions;
        private IGenericRepository<UserDetail>? _userDetails;
        private IGenericRepository<PasswordResetRequest>? _passwordResetRequests;
        private IGenericRepository<UserAuthority>? _userAuthorities;
        private IGenericRepository<PermissionDefinition>? _permissionDefinitions;
        private IGenericRepository<PermissionGroup>? _permissionGroups;
        private IGenericRepository<PermissionGroupPermission>? _permissionGroupPermissions;
        private IGenericRepository<UserPermissionGroup>? _userPermissionGroups;
        private IGenericRepository<VisibilityPolicy>? _visibilityPolicies;
        private IGenericRepository<UserVisibilityPolicy>? _userVisibilityPolicies;
        private IGenericRepository<NotificationEntity>? _notifications;
        private IGenericRepository<SmtpSetting>? _smtpSettings;
        private IGenericRepository<SystemSetting>? _systemSettings;
        private IGenericRepository<DocumentFieldLabel>? _documentFieldLabels;
        private IGenericRepository<JobFailureLog>? _jobFailureLogs;
        private IGenericRepository<JobExecutionLog>? _jobExecutionLogs;
        private IGenericRepository<AuditLog>? _auditLogs;

        public UnitOfWork(
            SalesDeskDbContext context,
            IHttpContextAccessor httpContextAccessor,
            ILocalizationService localizationService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _localizationService = localizationService;
            _repositories = new ConcurrentDictionary<Type, object>();
        }

        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context, _httpContextAccessor);
        public IGenericRepository<UserSession> UserSessions => _userSessions ??= new GenericRepository<UserSession>(_context, _httpContextAccessor);
        public IGenericRepository<UserDetail> UserDetails => _userDetails ??= new GenericRepository<UserDetail>(_context, _httpContextAccessor);
        public IGenericRepository<PasswordResetRequest> PasswordResetRequests => _passwordResetRequests ??= new GenericRepository<PasswordResetRequest>(_context, _httpContextAccessor);

        public IGenericRepository<UserAuthority> UserAuthorities => _userAuthorities ??= new GenericRepository<UserAuthority>(_context, _httpContextAccessor);
        public IGenericRepository<PermissionDefinition> PermissionDefinitions => _permissionDefinitions ??= new GenericRepository<PermissionDefinition>(_context, _httpContextAccessor);
        public IGenericRepository<PermissionGroup> PermissionGroups => _permissionGroups ??= new GenericRepository<PermissionGroup>(_context, _httpContextAccessor);
        public IGenericRepository<PermissionGroupPermission> PermissionGroupPermissions => _permissionGroupPermissions ??= new GenericRepository<PermissionGroupPermission>(_context, _httpContextAccessor);
        public IGenericRepository<UserPermissionGroup> UserPermissionGroups => _userPermissionGroups ??= new GenericRepository<UserPermissionGroup>(_context, _httpContextAccessor);
        public IGenericRepository<VisibilityPolicy> VisibilityPolicies => _visibilityPolicies ??= new GenericRepository<VisibilityPolicy>(_context, _httpContextAccessor);
        public IGenericRepository<UserVisibilityPolicy> UserVisibilityPolicies => _userVisibilityPolicies ??= new GenericRepository<UserVisibilityPolicy>(_context, _httpContextAccessor);

        public IGenericRepository<NotificationEntity> Notifications => _notifications ??= new GenericRepository<NotificationEntity>(_context, _httpContextAccessor);
        public IGenericRepository<SmtpSetting> SmtpSettings => _smtpSettings ??= new GenericRepository<SmtpSetting>(_context, _httpContextAccessor);
        public IGenericRepository<SystemSetting> SystemSettings => _systemSettings ??= new GenericRepository<SystemSetting>(_context, _httpContextAccessor);
        public IGenericRepository<DocumentFieldLabel> DocumentFieldLabels => _documentFieldLabels ??= new GenericRepository<DocumentFieldLabel>(_context, _httpContextAccessor);
        public IGenericRepository<JobFailureLog> JobFailureLogs => _jobFailureLogs ??= new GenericRepository<JobFailureLog>(_context, _httpContextAccessor);
        public IGenericRepository<JobExecutionLog> JobExecutionLogs => _jobExecutionLogs ??= new GenericRepository<JobExecutionLog>(_context, _httpContextAccessor);
        public IGenericRepository<AuditLog> AuditLogs => _auditLogs ??= new GenericRepository<AuditLog>(_context, _httpContextAccessor);

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            return (IGenericRepository<T>)_repositories.GetOrAdd(
                typeof(T),
                _ => new GenericRepository<T>(_context, _httpContextAccessor));
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch
            {
                if (_transaction != null)
                {
                    await RollbackTransactionAsync();
                }

                throw;
            }
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException(
                    _localizationService.GetLocalizedString("UnitOfWork.TransactionAlreadyInProgress"));
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException(
                    _localizationService.GetLocalizedString("UnitOfWork.NoTransactionInProgress"));
            }

            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                return;
            }

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
                _disposed = true;
            }
        }
    }
}
