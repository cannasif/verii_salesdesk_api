using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using salesdesk_api.Modules.AccessControl.Domain.Entities;
using salesdesk_api.Modules.AccessControl.Infrastructure.Persistence.Configurations;
using salesdesk_api.Modules.Identity.Domain.Entities;
using salesdesk_api.Modules.Identity.Infrastructure.Persistence.Configurations;
using salesdesk_api.Modules.Notification.Infrastructure.Persistence.Configurations;
using salesdesk_api.Modules.SalesDesk.Domain.Entities;
using salesdesk_api.Modules.SalesDesk.Infrastructure.Persistence.Configurations;
using salesdesk_api.Modules.SmtpIntegration.Domain.Entities;
using salesdesk_api.Modules.SmtpIntegration.Infrastructure.Persistence.Configurations;
using salesdesk_api.Modules.System.Domain.Entities;
using salesdesk_api.Modules.System.Infrastructure.Persistence.Configurations;
using salesdesk_api.Shared.Domain.Entities;
using salesdesk_api.Shared.Domain.Entities.Common;
using salesdesk_api.Shared.Infrastructure.Monitoring;
using salesdesk_api.Shared.Infrastructure.Persistence.Configurations;
using NotificationEntity = salesdesk_api.Modules.Notification.Domain.Entities.Notification;

namespace salesdesk_api.Shared.Infrastructure.Persistence
{
    public class SalesDeskDbContext : DbContext
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        private static readonly ValueConverter<DateTime, DateTime> UtcDateTimeConverter = new(
            value => value.Kind == DateTimeKind.Utc ? value : DateTime.SpecifyKind(value, DateTimeKind.Utc),
            value => value.Kind == DateTimeKind.Utc ? value : DateTime.SpecifyKind(value, DateTimeKind.Utc));

        private static readonly ValueConverter<DateTime?, DateTime?> NullableUtcDateTimeConverter = new(
            value => value.HasValue
                ? (value.Value.Kind == DateTimeKind.Utc ? value.Value : DateTime.SpecifyKind(value.Value, DateTimeKind.Utc))
                : value,
            value => value.HasValue
                ? (value.Value.Kind == DateTimeKind.Utc ? value.Value : DateTime.SpecifyKind(value.Value, DateTimeKind.Utc))
                : value);

        public SalesDeskDbContext(DbContextOptions<SalesDeskDbContext> options) : base(options)
        {
        }

        public SalesDeskDbContext(DbContextOptions<SalesDeskDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }

        public DbSet<UserAuthority> UserAuthorities { get; set; }
        public DbSet<PermissionDefinition> PermissionDefinitions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<PermissionGroupPermission> PermissionGroupPermissions { get; set; }
        public DbSet<UserPermissionGroup> UserPermissionGroups { get; set; }
        public DbSet<VisibilityPolicy> VisibilityPolicies { get; set; }
        public DbSet<UserVisibilityPolicy> UserVisibilityPolicies { get; set; }

        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<SmtpSetting> SmtpSettings { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<DocumentFieldLabel> DocumentFieldLabels { get; set; }
        public DbSet<JobFailureLog> JobFailureLogs { get; set; }
        public DbSet<JobExecutionLog> JobExecutionLogs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<SalesDeskCustomer> SalesDeskCustomers { get; set; }
        public DbSet<SalesDeskPotentialCustomer> SalesDeskPotentialCustomers { get; set; }
        public DbSet<SalesDeskProduct> SalesDeskProducts { get; set; }
        public DbSet<SalesDeskProductCustomer> SalesDeskProductCustomers { get; set; }
        public DbSet<SalesDeskQuote> SalesDeskQuotes { get; set; }
        public DbSet<SalesDeskQuoteLine> SalesDeskQuoteLines { get; set; }
        public DbSet<SalesDeskInvoice> SalesDeskInvoices { get; set; }
        public DbSet<SalesDeskInvoiceLine> SalesDeskInvoiceLines { get; set; }
        public DbSet<SalesDeskTask> SalesDeskTasks { get; set; }
        public DbSet<SalesDeskVisit> SalesDeskVisits { get; set; }
        public DbSet<SalesDeskVisitForm> SalesDeskVisitForms { get; set; }
        public DbSet<SalesDeskFixedAsset> SalesDeskFixedAssets { get; set; }
        public DbSet<SalesDeskRecurringPayment> SalesDeskRecurringPayments { get; set; }
        public DbSet<SalesDeskSoftwareResearch> SalesDeskSoftwareResearches { get; set; }
        public DbSet<SalesDeskErpNewsItem> SalesDeskErpNewsItems { get; set; }
        public DbSet<SalesDeskGmailMessage> SalesDeskGmailMessages { get; set; }
        public DbSet<SalesDeskGroup> SalesDeskGroups { get; set; }
        public DbSet<SalesDeskGroupMember> SalesDeskGroupMembers { get; set; }
        public DbSet<SalesDeskCompany> SalesDeskCompanies { get; set; }
        public DbSet<SalesDeskNote> SalesDeskNotes { get; set; }
        public DbSet<SalesDeskNoteRecipient> SalesDeskNoteRecipients { get; set; }
        public DbSet<SalesDeskNoteNotification> SalesDeskNoteNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
            modelBuilder.ApplyConfiguration(new UserDetailConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordResetRequestConfiguration());

            modelBuilder.ApplyConfiguration(new UserAuthorityConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionDefinitionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionGroupConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionGroupPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserPermissionGroupConfiguration());
            modelBuilder.ApplyConfiguration(new VisibilityPolicyConfiguration());
            modelBuilder.ApplyConfiguration(new UserVisibilityPolicyConfiguration());

            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new SmtpSettingConfiguration());
            modelBuilder.ApplyConfiguration(new SystemSettingConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentFieldLabelConfiguration());
            modelBuilder.ApplyConfiguration(new JobFailureLogConfiguration());
            modelBuilder.ApplyConfiguration(new JobExecutionLogConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());

            modelBuilder.ApplyConfiguration(new SalesDeskCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskPotentialCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskProductConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskProductCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskQuoteConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskQuoteLineConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskInvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskInvoiceLineConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskTaskConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskVisitConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskVisitFormConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskFixedAssetConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskRecurringPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskSoftwareResearchConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskErpNewsItemConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskGmailMessageConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskGroupConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskGroupMemberConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskNoteConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskNoteRecipientConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDeskNoteNotificationConfiguration());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        property.SetColumnType("decimal(18,6)");
                    }

                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(UtcDateTimeConverter);
                    }

                    if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(NullableUtcDateTimeConverter);
                    }
                }
            }
        }

        public override int SaveChanges()
        {
            ApplyRequestBranchCode();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyRequestBranchCode();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyRequestBranchCode()
        {
            var branchCode = ResolveRequestBranchCode();
            if (string.IsNullOrWhiteSpace(branchCode))
            {
                return;
            }

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added && string.IsNullOrWhiteSpace(entry.Entity.RequestBranchCode))
                {
                    entry.Entity.RequestBranchCode = branchCode;
                }
            }
        }

        private string? ResolveRequestBranchCode()
        {
            var context = _httpContextAccessor?.HttpContext;
            var branchCode = context?.Items["BranchCode"]?.ToString();

            if (string.IsNullOrWhiteSpace(branchCode))
            {
                branchCode = context?.Request.Headers["X-Branch-Code"].FirstOrDefault()
                    ?? context?.Request.Headers["Branch-Code"].FirstOrDefault();
            }

            return string.IsNullOrWhiteSpace(branchCode)
                ? null
                : branchCode.Trim();
        }
    }
}
