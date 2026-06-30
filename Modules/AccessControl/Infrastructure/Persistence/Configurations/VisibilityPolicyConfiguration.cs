using salesdesk_api.Modules.AccessControl.Domain.Entities;
using salesdesk_api.Shared.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace salesdesk_api.Modules.AccessControl.Infrastructure.Persistence.Configurations
{
    public class VisibilityPolicyConfiguration : BaseEntityConfiguration<VisibilityPolicy>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<VisibilityPolicy> builder)
        {
            builder.ToTable("RII_VISIBILITY_POLICY");

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.EntityType)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(x => x.ScopeType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.IncludeSelf)
                .HasDefaultValue(true);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(x => x.Code)
                .IsUnique()
                .HasDatabaseName("IX_VisibilityPolicy_Code");

            builder.HasIndex(x => new { x.EntityType, x.IsActive })
                .HasDatabaseName("IX_VisibilityPolicy_EntityType_IsActive");
        }
    }
}
