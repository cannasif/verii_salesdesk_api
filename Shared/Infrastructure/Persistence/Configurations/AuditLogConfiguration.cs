using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salesdesk_api.Shared.Domain.Entities;

namespace salesdesk_api.Shared.Infrastructure.Persistence.Configurations;

public sealed class AuditLogConfiguration : BaseEntityConfiguration<AuditLog>
{
    protected override void ConfigureEntity(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("RII_AUDIT_LOG");

        builder.Property(x => x.TraceId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.ActionType)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.EntityType)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.EntityId)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Result)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.Source)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Reason)
            .HasMaxLength(512);

        builder.Property(x => x.FailureReason)
            .HasMaxLength(512);

        builder.Property(x => x.BranchCode)
            .HasMaxLength(32);

        builder.Property(x => x.RequestPath)
            .HasMaxLength(256);

        builder.Property(x => x.RequestMethod)
            .HasMaxLength(16);

        builder.Property(x => x.PerformedByUserEmail)
            .HasMaxLength(256);

        builder.Property(x => x.OldValuesJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.NewValuesJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ChangedFieldsJson)
            .HasColumnType("nvarchar(max)");

        builder.HasIndex(x => x.TraceId)
            .HasDatabaseName("IX_AuditLog_TraceId");

        builder.HasIndex(x => new { x.EntityType, x.EntityId, x.CreatedDate })
            .HasDatabaseName("IX_AuditLog_EntityType_EntityId_CreatedDate");

        builder.HasIndex(x => new { x.PerformedByUserId, x.CreatedDate })
            .HasDatabaseName("IX_AuditLog_PerformedByUserId_CreatedDate");
    }
}
