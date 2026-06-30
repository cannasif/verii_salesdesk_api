using System.ComponentModel.DataAnnotations;
using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Application.Dtos;

public sealed class SalesDeskDashboardDto
{
    public int CustomerCount { get; set; }
    public int PotentialCount { get; set; }
    public int ProductCount { get; set; }
    public int OpenTaskCount { get; set; }
    public int TodayVisitCount { get; set; }
    public int PendingQuoteCount { get; set; }
    public int ToBeIssuedInvoiceCount { get; set; }
    public decimal MonthlySalesTotal { get; set; }
}

public sealed class SalesDeskSearchResultDto
{
    public string Type { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Url { get; set; }
}

public sealed class SalesDeskStatsDto
{
    public Dictionary<string, int> Counts { get; set; } = new();
    public Dictionary<string, decimal> Totals { get; set; } = new();
}

public sealed class SalesDeskCustomerDto
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public SalesDeskCustomerKind Kind { get; set; }
    public decimal Balance { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
}

public sealed class SalesDeskCustomerUpsertDto
{
    [MaxLength(32)]
    public string? Code { get; set; }

    [Required, MaxLength(220)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(160)]
    public string? ContactName { get; set; }

    [MaxLength(40)]
    public string? Phone { get; set; }

    [MaxLength(160), EmailAddress]
    public string? Email { get; set; }

    public SalesDeskCustomerKind Kind { get; set; } = SalesDeskCustomerKind.Customer;
    public decimal Balance { get; set; }

    [MaxLength(80)]
    public string? City { get; set; }

    [MaxLength(80)]
    public string? District { get; set; }
}

public sealed class SalesDeskPotentialCustomerDto
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public SalesDeskPotentialStatus Status { get; set; }
    public int MatchScore { get; set; }
    public DateTime? LastResearchDate { get; set; }
}

public sealed class SalesDeskPotentialCustomerUpsertDto
{
    [MaxLength(32)]
    public string? Code { get; set; }

    [Required, MaxLength(220)]
    public string CompanyName { get; set; } = string.Empty;

    [MaxLength(160)]
    public string? ContactName { get; set; }

    [MaxLength(40)]
    public string? Phone { get; set; }

    [MaxLength(160), EmailAddress]
    public string? Email { get; set; }

    [MaxLength(80)]
    public string? City { get; set; }

    [MaxLength(80)]
    public string? District { get; set; }

    public SalesDeskPotentialStatus Status { get; set; } = SalesDeskPotentialStatus.Waiting;
    public int MatchScore { get; set; }
    public DateTime? LastResearchDate { get; set; }
}

public sealed class SalesDeskProductDto
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal SalesPrice { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal MinimumStockQuantity { get; set; }
    public bool IsLowStock => StockQuantity < MinimumStockQuantity;
}

public sealed class SalesDeskProductUpsertDto
{
    [MaxLength(32)]
    public string? Code { get; set; }

    [Required, MaxLength(220)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Category { get; set; }

    [Required, MaxLength(24)]
    public string Unit { get; set; } = "Adet";

    public decimal SalesPrice { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal MinimumStockQuantity { get; set; }
}

public sealed class SalesDeskProductCustomerDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? PotentialCustomerId { get; set; }
    public string? PotentialCustomerName { get; set; }
}

public sealed class SalesDeskProductCustomerUpsertDto
{
    [Required]
    public long ProductId { get; set; }

    public long? CustomerId { get; set; }
    public long? PotentialCustomerId { get; set; }
}

public sealed class SalesDeskLineDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; }
    public decimal LineTotal { get; set; }
}

public sealed class SalesDeskLineUpsertDto
{
    [Required]
    public long ProductId { get; set; }

    public decimal Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; } = 20;
}

public sealed class SalesDeskQuoteDto
{
    public long Id { get; set; }
    public string QuoteNumber { get; set; } = string.Empty;
    public long CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime QuoteDate { get; set; }
    public SalesDeskDocumentStatus Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal VatTotal { get; set; }
    public decimal GrandTotal { get; set; }
    public string? Notes { get; set; }
    public List<SalesDeskLineDto> Lines { get; set; } = new();
}

public sealed class SalesDeskQuoteUpsertDto
{
    [MaxLength(32)]
    public string? QuoteNumber { get; set; }

    [Required]
    public long CustomerId { get; set; }

    public DateTime QuoteDate { get; set; } = DateTime.UtcNow.Date;
    public SalesDeskDocumentStatus Status { get; set; } = SalesDeskDocumentStatus.Draft;

    [MaxLength(2000)]
    public string? Notes { get; set; }

    public List<SalesDeskLineUpsertDto> Lines { get; set; } = new();
}

public sealed class SalesDeskInvoiceDto
{
    public long Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public long CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public long? QuoteId { get; set; }
    public string? QuoteNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public SalesDeskDocumentStatus Status { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal SubTotal { get; set; }
    public decimal VatTotal { get; set; }
    public decimal GrandTotal { get; set; }
    public string? Notes { get; set; }
    public List<SalesDeskLineDto> Lines { get; set; } = new();
}

public sealed class SalesDeskInvoiceUpsertDto
{
    [MaxLength(32)]
    public string? InvoiceNumber { get; set; }

    [Required]
    public long CustomerId { get; set; }

    public long? QuoteId { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow.Date;
    public DateTime DueDate { get; set; } = DateTime.UtcNow.Date.AddDays(30);
    public SalesDeskDocumentStatus Status { get; set; } = SalesDeskDocumentStatus.ToBeIssued;
    public decimal DiscountRate { get; set; }

    [MaxLength(2000)]
    public string? Notes { get; set; }

    public List<SalesDeskLineUpsertDto> Lines { get; set; } = new();
}

public sealed class SalesDeskTaskDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? GroupName { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? AssignedUserId { get; set; }
    public SalesDeskPriority Priority { get; set; }
    public SalesDeskTaskStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
}

public sealed class SalesDeskTaskUpsertDto
{
    [Required, MaxLength(220)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? GroupName { get; set; }

    public long? CustomerId { get; set; }
    public long? AssignedUserId { get; set; }
    public SalesDeskPriority Priority { get; set; } = SalesDeskPriority.Medium;
    public SalesDeskTaskStatus Status { get; set; } = SalesDeskTaskStatus.Open;
    public DateTime? DueDate { get; set; }
}

public sealed class SalesDeskVisitDto
{
    public long Id { get; set; }
    public DateTime VisitDate { get; set; }
    public TimeSpan? VisitTime { get; set; }
    public string Title { get; set; } = string.Empty;
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string VisitType { get; set; } = string.Empty;
    public SalesDeskVisitStatus Status { get; set; }
    public string? Notes { get; set; }
}

public sealed class SalesDeskVisitUpsertDto
{
    public DateTime VisitDate { get; set; } = DateTime.UtcNow.Date;
    public TimeSpan? VisitTime { get; set; }

    [Required, MaxLength(220)]
    public string Title { get; set; } = string.Empty;

    public long? CustomerId { get; set; }

    [MaxLength(80)]
    public string VisitType { get; set; } = "Yuz Yuze";

    public SalesDeskVisitStatus Status { get; set; } = SalesDeskVisitStatus.Planned;

    [MaxLength(2000)]
    public string? Notes { get; set; }
}

public sealed class SalesDeskVisitFormDto
{
    public long Id { get; set; }
    public long? VisitId { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime FormDate { get; set; }
    public string? Content { get; set; }
    public long? OwnerUserId { get; set; }
}

public sealed class SalesDeskVisitFormUpsertDto
{
    public long? VisitId { get; set; }
    public long? CustomerId { get; set; }

    [Required, MaxLength(220)]
    public string Title { get; set; } = string.Empty;

    public DateTime FormDate { get; set; } = DateTime.UtcNow.Date;

    [MaxLength(4000)]
    public string? Content { get; set; }

    public long? OwnerUserId { get; set; }
}

public sealed class SalesDeskFixedAssetDto
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Value { get; set; }
    public SalesDeskFixedAssetStatus Status { get; set; }
}

public sealed class SalesDeskFixedAssetUpsertDto
{
    [MaxLength(32)]
    public string? Code { get; set; }

    [Required, MaxLength(220)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Category { get; set; }

    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow.Date;
    public decimal Value { get; set; }
    public SalesDeskFixedAssetStatus Status { get; set; } = SalesDeskFixedAssetStatus.Active;
}

public sealed class SalesDeskRecurringPaymentDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SalesDeskRecurringPaymentType Type { get; set; }
    public string? Category { get; set; }
    public int DayOfMonth { get; set; }
    public decimal Amount { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public bool IsActive { get; set; }
}

public sealed class SalesDeskRecurringPaymentUpsertDto
{
    [Required, MaxLength(220)]
    public string Name { get; set; } = string.Empty;

    public SalesDeskRecurringPaymentType Type { get; set; } = SalesDeskRecurringPaymentType.Expense;

    [MaxLength(100)]
    public string? Category { get; set; }

    [Range(1, 31)]
    public int DayOfMonth { get; set; } = 1;

    public decimal Amount { get; set; }
    public long? CustomerId { get; set; }
    public bool IsActive { get; set; } = true;
}

public sealed class SalesDeskSoftwareResearchDto
{
    public long Id { get; set; }
    public long? PotentialCustomerId { get; set; }
    public string? PotentialCustomerName { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string? Keywords { get; set; }
    public string? Host { get; set; }
    public string? SourceUrl { get; set; }
    public int Score { get; set; }
    public SalesDeskPotentialStatus Status { get; set; }
    public DateTime? ResearchedAt { get; set; }
}

public sealed class SalesDeskSoftwareResearchUpsertDto
{
    public long? PotentialCustomerId { get; set; }

    [Required, MaxLength(80)]
    public string Provider { get; set; } = "Netsis";

    [MaxLength(800)]
    public string? Keywords { get; set; }

    [MaxLength(180)]
    public string? Host { get; set; }

    [MaxLength(1000)]
    public string? SourceUrl { get; set; }

    public int Score { get; set; }
    public SalesDeskPotentialStatus Status { get; set; } = SalesDeskPotentialStatus.Waiting;
    public DateTime? ResearchedAt { get; set; }
}

public sealed class SalesDeskErpNewsItemDto
{
    public long Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Source { get; set; }
    public string? SourceUrl { get; set; }
    public int Score { get; set; }
    public bool IsCritical { get; set; }
    public bool IsRead { get; set; }
    public DateTime PublishedAt { get; set; }
}

public sealed class SalesDeskErpNewsItemUpsertDto
{
    [Required, MaxLength(80)]
    public string Topic { get; set; } = "Netsis";

    [Required, MaxLength(320)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(180)]
    public string? Source { get; set; }

    [MaxLength(1000)]
    public string? SourceUrl { get; set; }

    public int Score { get; set; }
    public bool IsCritical { get; set; }
    public bool IsRead { get; set; }
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
}

public sealed class SalesDeskGmailMessageDto
{
    public long Id { get; set; }
    public string GmailMessageId { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string? Preview { get; set; }
    public DateTime ReceivedAt { get; set; }
    public bool IsUnread { get; set; }
    public bool IsMeeting { get; set; }
    public string? ThreadId { get; set; }
}

public sealed class SalesDeskGmailMessageUpsertDto
{
    [Required, MaxLength(180)]
    public string GmailMessageId { get; set; } = string.Empty;

    [Required, MaxLength(240)]
    public string Sender { get; set; } = string.Empty;

    [Required, MaxLength(320)]
    public string Subject { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Preview { get; set; }

    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    public bool IsUnread { get; set; }
    public bool IsMeeting { get; set; }

    [MaxLength(180)]
    public string? ThreadId { get; set; }
}
