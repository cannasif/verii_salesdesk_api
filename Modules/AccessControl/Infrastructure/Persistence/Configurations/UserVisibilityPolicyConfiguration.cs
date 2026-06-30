using salesdesk_api.Modules.AccessControl.Domain.Entities;
using salesdesk_api.Shared.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace salesdesk_api.Modules.AccessControl.Infrastructure.Persistence.Configurations
{
    public class UserVisibilityPolicyConfiguration : BaseEntityConfiguration<UserVisibilityPolicy>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UserVisibilityPolicy> builder)
        {
            builder.ToTable("RII_USER_VISIBILITY_POLICY");

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.VisibilityPolicyId).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.VisibilityPolicy)
                .WithMany(x => x.UserAssignments)
                .HasForeignKey(x => x.VisibilityPolicyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => new { x.UserId, x.VisibilityPolicyId })
                .IsUnique()
                .HasDatabaseName("IX_UserVisibilityPolicy_UserId_VisibilityPolicyId");

            builder.HasIndex(x => x.UserId)
                .HasDatabaseName("IX_UserVisibilityPolicy_UserId");
        }
    }
}
