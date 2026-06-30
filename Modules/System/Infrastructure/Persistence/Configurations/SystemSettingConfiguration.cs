using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salesdesk_api.Modules.System.Domain.Entities;
using salesdesk_api.Shared.Infrastructure.Persistence.Configurations;

namespace salesdesk_api.Modules.System.Infrastructure.Persistence.Configurations
{
    public class SystemSettingConfiguration : BaseEntityConfiguration<SystemSetting>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<SystemSetting> builder)
        {
            builder.ToTable("RII_SD_SYSTEM_SETTING");

            builder.Property(x => x.NumberFormat).HasMaxLength(20).IsRequired();
            builder.Property(x => x.DecimalPlaces).IsRequired();
            builder.Property(x => x.CurrencyCode).HasMaxLength(8).IsRequired();
            builder.Property(x => x.DefaultVatRate).HasPrecision(9, 4).IsRequired();
            builder.Property(x => x.MaxGeneralDiscountRate).HasPrecision(9, 4).IsRequired();
            builder.Property(x => x.EnableGmailInbox).IsRequired();
            builder.Property(x => x.EnableSalesDeskNotifications).IsRequired();
        }
    }
}
