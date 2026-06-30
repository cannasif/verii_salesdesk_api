using salesdesk_api.Modules.System.Domain.Entities;
using salesdesk_api.Shared.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace salesdesk_api.Modules.System.Infrastructure.Persistence.Configurations
{
    public class DocumentFieldLabelConfiguration : BaseEntityConfiguration<DocumentFieldLabel>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<DocumentFieldLabel> builder)
        {
            builder.ToTable("DocumentFieldLabels");

            builder.Property(x => x.DocumentType).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Scope).HasMaxLength(30).IsRequired();
            builder.Property(x => x.FieldKey).HasMaxLength(30).IsRequired();
            builder.Property(x => x.DefaultLabel).HasMaxLength(80).IsRequired();
            builder.Property(x => x.CustomLabel).HasMaxLength(80);
            builder.Property(x => x.HelpText).HasMaxLength(500);
            builder.Property(x => x.Placeholder).HasMaxLength(120);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasIndex(x => new { x.DocumentType, x.Scope, x.FieldKey })
                .IsUnique()
                .HasDatabaseName("UX_DocumentFieldLabels_DocumentType_Scope_FieldKey");

            builder.HasIndex(x => new { x.DocumentType, x.Scope, x.SortOrder })
                .HasDatabaseName("IX_DocumentFieldLabels_DocumentType_Scope_SortOrder");
        }
    }
}
