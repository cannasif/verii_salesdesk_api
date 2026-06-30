using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salesdesk_api.Modules.AccessControl.Domain.Entities;

namespace salesdesk_api.Modules.AccessControl.Infrastructure.Persistence.Configurations
{
    public class PermissionDefinitionConfiguration : BaseEntityConfiguration<PermissionDefinition>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<PermissionDefinition> builder)
        {
            builder.ToTable("RII_PERMISSION_DEFINITIONS");

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.AvailableOnWeb)
                .HasDefaultValue(true);

            builder.Property(x => x.AvailableOnMobile)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.Code)
                .IsUnique()
                .HasDatabaseName("IX_PermissionDefinitions_Code");

            builder.HasIndex(x => x.IsDeleted)
                .HasDatabaseName("IX_PermissionDefinitions_IsDeleted");

            builder.HasQueryFilter(x => !x.IsDeleted);

            var seedDate = new DateTime(2026, 06, 30, 0, 0, 0, DateTimeKind.Utc);
            var id = 1;
            var permissions = new List<PermissionDefinition>
            {
                Create(id++, "dashboard.view", "Dashboard - Goruntule", seedDate, mobile: true),
                Create(id++, "search.view", "Genel Arama - Goruntule", seedDate, mobile: true),
                Create(id++, "summary.view", "Ozet - Goruntule", seedDate, mobile: true),
                Create(id++, "settings.view", "Sistem Ayarlari - Goruntule", seedDate),
                Create(id++, "settings.update", "Sistem Ayarlari - Duzenle", seedDate),
                Create(id++, "users.view", "Kullanicilar - Goruntule", seedDate),
                Create(id++, "users.create", "Kullanicilar - Olustur", seedDate),
                Create(id++, "users.update", "Kullanicilar - Duzenle", seedDate)
            };

            foreach (var module in new[]
                     {
                         "customers",
                         "potentials",
                         "products",
                         "product-customers",
                         "quotes",
                         "invoices",
                         "tasks",
                         "visits",
                         "visit-forms",
                         "assets",
                         "recurring-payments",
                         "software-research",
                         "erp-news",
                         "gmail-messages"
                     })
            {
                var name = ToPermissionName(module);
                permissions.Add(Create(id++, $"salesdesk.{module}.view", $"{name} - Goruntule", seedDate, mobile: true));
                permissions.Add(Create(id++, $"salesdesk.{module}.create", $"{name} - Olustur", seedDate));
                permissions.Add(Create(id++, $"salesdesk.{module}.update", $"{name} - Duzenle", seedDate));
                permissions.Add(Create(id++, $"salesdesk.{module}.delete", $"{name} - Sil", seedDate));
            }

            builder.HasData(permissions);
        }

        private static PermissionDefinition Create(
            int id,
            string code,
            string name,
            DateTime seedDate,
            bool mobile = false)
        {
            return new PermissionDefinition
            {
                Id = id,
                Code = code,
                Name = name,
                CreatedDate = seedDate,
                IsDeleted = false,
                IsActive = true,
                AvailableOnWeb = true,
                AvailableOnMobile = mobile
            };
        }

        private static string ToPermissionName(string module)
        {
            return module switch
            {
                "customers" => "Musteriler",
                "potentials" => "Potansiyel Cariler",
                "products" => "Stok Urunler",
                "product-customers" => "Urun Bazli Musteriler",
                "quotes" => "Teklifler",
                "invoices" => "Faturalar",
                "tasks" => "Acik Maddeler",
                "visits" => "Haftalik Ziyaretler",
                "visit-forms" => "Ziyaret Formlari",
                "assets" => "Demirbaslar",
                "recurring-payments" => "Standart Odemeler",
                "software-research" => "Yazilim Arastirma",
                "erp-news" => "ERP Haber Takibi",
                "gmail-messages" => "Gmail Mesajlari",
                _ => module
            };
        }
    }
}
