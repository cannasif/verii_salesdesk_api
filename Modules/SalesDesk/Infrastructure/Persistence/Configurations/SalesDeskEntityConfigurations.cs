using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salesdesk_api.Modules.SalesDesk.Domain.Entities;
using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Infrastructure.Persistence.Configurations;

namespace salesdesk_api.Modules.SalesDesk.Infrastructure.Persistence.Configurations;

public sealed class SalesDeskCustomerConfiguration : BaseEntityConfiguration<SalesDeskCustomer>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskCustomer> builder)
    {
        builder.ToTable("RII_SD_CUSTOMER");
        builder.Property(x => x.Code).HasMaxLength(32).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(220).IsRequired();
        builder.Property(x => x.ContactName).HasMaxLength(160);
        builder.Property(x => x.Phone).HasMaxLength(40);
        builder.Property(x => x.Email).HasMaxLength(160);
        builder.Property(x => x.City).HasMaxLength(80);
        builder.Property(x => x.District).HasMaxLength(80);
        builder.Property(x => x.Balance).HasPrecision(18, 2);
        builder.HasIndex(x => x.Code).IsUnique().HasFilter("[IsDeleted] = 0");
        builder.HasIndex(x => x.Name);
    }
}

public sealed class SalesDeskPotentialCustomerConfiguration : BaseEntityConfiguration<SalesDeskPotentialCustomer>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskPotentialCustomer> builder)
    {
        builder.ToTable("RII_SD_POTENTIAL_CUSTOMER");
        builder.Property(x => x.Code).HasMaxLength(32).IsRequired();
        builder.Property(x => x.CompanyName).HasMaxLength(220).IsRequired();
        builder.Property(x => x.ContactName).HasMaxLength(160);
        builder.Property(x => x.Phone).HasMaxLength(40);
        builder.Property(x => x.Email).HasMaxLength(160);
        builder.Property(x => x.City).HasMaxLength(80);
        builder.Property(x => x.District).HasMaxLength(80);
        builder.HasIndex(x => x.Code).IsUnique().HasFilter("[IsDeleted] = 0");
        builder.HasIndex(x => x.CompanyName);
    }
}

public sealed class SalesDeskProductConfiguration : BaseEntityConfiguration<SalesDeskProduct>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskProduct> builder)
    {
        builder.ToTable("RII_SD_PRODUCT");
        builder.Property(x => x.Code).HasMaxLength(32).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Category).HasMaxLength(100);
        builder.Property(x => x.Unit).HasMaxLength(24).IsRequired();
        builder.Property(x => x.SalesPrice).HasPrecision(18, 2);
        builder.Property(x => x.StockQuantity).HasPrecision(18, 2);
        builder.Property(x => x.MinimumStockQuantity).HasPrecision(18, 2);
        builder.Property(x => x.SearchText).HasMaxLength(1200);
        builder.HasIndex(x => x.Code).IsUnique().HasFilter("[IsDeleted] = 0");
        builder.HasIndex(x => x.SearchText);
    }
}

public sealed class SalesDeskProductCustomerConfiguration : BaseEntityConfiguration<SalesDeskProductCustomer>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskProductCustomer> builder)
    {
        builder.ToTable("RII_SD_PRODUCT_CUSTOMER");
        builder.HasOne(x => x.Product).WithMany(x => x.ProductCustomers).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.PotentialCustomer).WithMany().HasForeignKey(x => x.PotentialCustomerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(x => new { x.ProductId, x.CustomerId, x.PotentialCustomerId });
    }
}

public sealed class SalesDeskQuoteConfiguration : BaseEntityConfiguration<SalesDeskQuote>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskQuote> builder)
    {
        builder.ToTable("RII_SD_QUOTE");
        builder.Property(x => x.QuoteNumber).HasMaxLength(32).IsRequired();
        builder.Property(x => x.SubTotal).HasPrecision(18, 2);
        builder.Property(x => x.VatTotal).HasPrecision(18, 2);
        builder.Property(x => x.GrandTotal).HasPrecision(18, 2);
        builder.Property(x => x.Notes).HasMaxLength(2000);
        builder.HasOne(x => x.Customer).WithMany(x => x.Quotes).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(x => x.QuoteNumber).IsUnique().HasFilter("[IsDeleted] = 0");
        builder.HasIndex(x => x.QuoteDate);
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => new { x.Status, x.QuoteDate });
    }
}

public sealed class SalesDeskQuoteLineConfiguration : BaseEntityConfiguration<SalesDeskQuoteLine>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskQuoteLine> builder)
    {
        builder.ToTable("RII_SD_QUOTE_LINE");
        builder.Property(x => x.Quantity).HasPrecision(18, 2);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
        builder.Property(x => x.VatRate).HasPrecision(9, 2);
        builder.Property(x => x.LineTotal).HasPrecision(18, 2);
        builder.HasOne(x => x.Quote).WithMany(x => x.Lines).HasForeignKey(x => x.QuoteId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
    }
}

public sealed class SalesDeskInvoiceConfiguration : BaseEntityConfiguration<SalesDeskInvoice>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskInvoice> builder)
    {
        builder.ToTable("RII_SD_INVOICE");
        builder.Property(x => x.InvoiceNumber).HasMaxLength(32).IsRequired();
        builder.Property(x => x.DiscountRate).HasPrecision(9, 2);
        builder.Property(x => x.DiscountTotal).HasPrecision(18, 2);
        builder.Property(x => x.SubTotal).HasPrecision(18, 2);
        builder.Property(x => x.VatTotal).HasPrecision(18, 2);
        builder.Property(x => x.GrandTotal).HasPrecision(18, 2);
        builder.Property(x => x.Notes).HasMaxLength(2000);
        builder.HasOne(x => x.Customer).WithMany(x => x.Invoices).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Quote).WithMany().HasForeignKey(x => x.QuoteId).OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.InvoiceType)
            .HasDefaultValue(SalesDeskInvoiceType.Sales)
            .HasSentinel(SalesDeskInvoiceType.Sales);
        builder.HasIndex(x => new { x.InvoiceNumber, x.InvoiceType }).IsUnique().HasFilter("[IsDeleted] = 0");
        builder.HasIndex(x => x.InvoiceDate);
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => new { x.Status, x.InvoiceDate });
    }
}

public sealed class SalesDeskInvoiceLineConfiguration : BaseEntityConfiguration<SalesDeskInvoiceLine>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskInvoiceLine> builder)
    {
        builder.ToTable("RII_SD_INVOICE_LINE");
        builder.Property(x => x.Quantity).HasPrecision(18, 2);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
        builder.Property(x => x.VatRate).HasPrecision(9, 2);
        builder.Property(x => x.LineTotal).HasPrecision(18, 2);
        builder.HasOne(x => x.Invoice).WithMany(x => x.Lines).HasForeignKey(x => x.InvoiceId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
    }
}

public sealed class SalesDeskTaskConfiguration : BaseEntityConfiguration<SalesDeskTask>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskTask> builder)
    {
        builder.ToTable("RII_SD_TASK");
        builder.Property(x => x.Title).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(2000);
        builder.Property(x => x.GroupName).HasMaxLength(100);
        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(x => new { x.Status, x.Priority, x.DueDate });
        builder.HasIndex(x => x.GroupName);
    }
}

public sealed class SalesDeskVisitConfiguration : BaseEntityConfiguration<SalesDeskVisit>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskVisit> builder)
    {
        builder.ToTable("RII_SD_VISIT");
        builder.Property(x => x.Title).HasMaxLength(220).IsRequired();
        builder.Property(x => x.VisitType).HasMaxLength(80);
        builder.Property(x => x.Notes).HasMaxLength(2000);
        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(x => new { x.VisitDate, x.Status });
    }
}

public sealed class SalesDeskVisitFormConfiguration : BaseEntityConfiguration<SalesDeskVisitForm>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskVisitForm> builder)
    {
        builder.ToTable("RII_SD_VISIT_FORM");
        builder.Property(x => x.Title).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Content).HasMaxLength(4000);
        builder.HasOne(x => x.Visit).WithMany().HasForeignKey(x => x.VisitId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
    }
}

public sealed class SalesDeskFixedAssetConfiguration : BaseEntityConfiguration<SalesDeskFixedAsset>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskFixedAsset> builder)
    {
        builder.ToTable("RII_SD_FIXED_ASSET");
        builder.Property(x => x.Code).HasMaxLength(32).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Category).HasMaxLength(100);
        builder.Property(x => x.Value).HasPrecision(18, 2);
        builder.HasIndex(x => x.Code).IsUnique().HasFilter("[IsDeleted] = 0");
    }
}

public sealed class SalesDeskRecurringPaymentConfiguration : BaseEntityConfiguration<SalesDeskRecurringPayment>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskRecurringPayment> builder)
    {
        builder.ToTable("RII_SD_RECURRING_PAYMENT");
        builder.Property(x => x.Name).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Category).HasMaxLength(100);
        builder.Property(x => x.Amount).HasPrecision(18, 2);
        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(x => new { x.IsActive, x.DayOfMonth });
    }
}

public sealed class SalesDeskSoftwareResearchConfiguration : BaseEntityConfiguration<SalesDeskSoftwareResearch>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskSoftwareResearch> builder)
    {
        builder.ToTable("RII_SD_SOFTWARE_RESEARCH");
        builder.Property(x => x.Provider).HasMaxLength(80).IsRequired();
        builder.Property(x => x.Keywords).HasMaxLength(800);
        builder.Property(x => x.Host).HasMaxLength(180);
        builder.Property(x => x.SourceUrl).HasMaxLength(1000);
        builder.HasOne(x => x.PotentialCustomer).WithMany().HasForeignKey(x => x.PotentialCustomerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(x => new { x.Provider, x.Status, x.Score });
    }
}

public sealed class SalesDeskErpNewsItemConfiguration : BaseEntityConfiguration<SalesDeskErpNewsItem>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskErpNewsItem> builder)
    {
        builder.ToTable("RII_SD_ERP_NEWS_ITEM");
        builder.Property(x => x.Topic).HasMaxLength(80).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(320).IsRequired();
        builder.Property(x => x.Source).HasMaxLength(180);
        builder.Property(x => x.SourceUrl).HasMaxLength(1000);
        builder.HasIndex(x => new { x.Topic, x.IsCritical, x.IsRead, x.PublishedAt });
    }
}

public sealed class SalesDeskGmailMessageConfiguration : BaseEntityConfiguration<SalesDeskGmailMessage>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskGmailMessage> builder)
    {
        builder.ToTable("RII_SD_GMAIL_MESSAGE");
        builder.Property(x => x.GmailMessageId).HasMaxLength(180).IsRequired();
        builder.Property(x => x.Sender).HasMaxLength(240).IsRequired();
        builder.Property(x => x.Subject).HasMaxLength(320).IsRequired();
        builder.Property(x => x.Preview).HasMaxLength(2000);
        builder.Property(x => x.ThreadId).HasMaxLength(180);
        builder.HasIndex(x => x.GmailMessageId).IsUnique().HasFilter("[IsDeleted] = 0");
        builder.HasIndex(x => new { x.IsUnread, x.IsMeeting, x.ReceivedAt });
    }
}

public sealed class SalesDeskGroupConfiguration : BaseEntityConfiguration<SalesDeskGroup>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskGroup> builder)
    {
        builder.ToTable("RII_SD_GROUP");
        builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.HasIndex(x => x.Name);
    }
}

public sealed class SalesDeskGroupMemberConfiguration : BaseEntityConfiguration<SalesDeskGroupMember>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskGroupMember> builder)
    {
        builder.ToTable("RII_SD_GROUP_MEMBER");
        builder.HasOne(x => x.Group).WithMany(x => x.Members).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.GroupId, x.UserId }).IsUnique().HasFilter("[IsDeleted] = 0");
        builder.HasIndex(x => x.UserId);
    }
}

public sealed class SalesDeskCompanyConfiguration : BaseEntityConfiguration<SalesDeskCompany>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskCompany> builder)
    {
        builder.ToTable("RII_SD_COMPANY");
        builder.Property(x => x.Name).HasMaxLength(220).IsRequired();
        builder.Property(x => x.IpAddress).HasMaxLength(120);
        builder.Property(x => x.IpUsername).HasMaxLength(120);
        builder.Property(x => x.IpPassword).HasMaxLength(200);
        builder.Property(x => x.VpnName).HasMaxLength(120);
        builder.Property(x => x.VpnUsername).HasMaxLength(120);
        builder.Property(x => x.VpnPassword).HasMaxLength(200);
        builder.Property(x => x.VpnIpAddress).HasMaxLength(120);
        builder.Property(x => x.VpnPort).HasMaxLength(20);
        builder.Property(x => x.DatabaseUsername).HasMaxLength(120);
        builder.Property(x => x.DatabasePassword).HasMaxLength(200);
        builder.Property(x => x.LoginUrl).HasMaxLength(500);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.Description1).HasMaxLength(1000);
        builder.HasIndex(x => x.Name);
    }
}

public sealed class SalesDeskNoteConfiguration : BaseEntityConfiguration<SalesDeskNote>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskNote> builder)
    {
        builder.ToTable("RII_SD_NOTE");
        builder.Property(x => x.Title).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Content).HasMaxLength(4000);
        builder.Property(x => x.CreatedByName).HasMaxLength(160).IsRequired();
        builder.HasIndex(x => x.CreatedByUserId);
    }
}

public sealed class SalesDeskNoteRecipientConfiguration : BaseEntityConfiguration<SalesDeskNoteRecipient>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskNoteRecipient> builder)
    {
        builder.ToTable("RII_SD_NOTE_RECIPIENT");
        builder.HasOne(x => x.Note).WithMany(x => x.Recipients).HasForeignKey(x => x.NoteId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.NoteId, x.UserId }).IsUnique().HasFilter("[IsDeleted] = 0");
        builder.HasIndex(x => x.UserId);
    }
}

public sealed class SalesDeskNoteNotificationConfiguration : BaseEntityConfiguration<SalesDeskNoteNotification>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SalesDeskNoteNotification> builder)
    {
        builder.ToTable("RII_SD_NOTE_NOTIFICATION");
        builder.Property(x => x.Title).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Message).HasMaxLength(500).IsRequired();
        builder.Property(x => x.CreatedByName).HasMaxLength(160).IsRequired();
        builder.HasOne(x => x.Note).WithMany().HasForeignKey(x => x.NoteId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.RecipientUserId, x.DeliveredAt });
    }
}
