using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace salesdesk_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialSalesDeskSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentFieldLabels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FieldKey = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DefaultLabel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CustomLabel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    HelpText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Placeholder = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFieldLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleArgs = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MessageKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MessageArgs = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RelatedEntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RelatedEntityId = table.Column<long>(type: "bigint", nullable: true),
                    NotificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_AUDIT_LOG",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraceId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    FailureReason = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    RequestPath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RequestMethod = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PerformedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    PerformedByUserEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OldValuesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValuesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedFieldsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_AUDIT_LOG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_JOB_EXECUTION_LOG",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecurringJobId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JobName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Queue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMs = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ExceptionType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_JOB_EXECUTION_LOG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_JOB_FAILURE_LOG",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FailedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ExceptionType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Queue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_JOB_FAILURE_LOG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_PASSWORD_RESET_REQUEST",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_PASSWORD_RESET_REQUEST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_PERMISSION_DEFINITIONS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    AvailableOnWeb = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    AvailableOnMobile = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_PERMISSION_DEFINITIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_PERMISSION_GROUP_PERMISSIONS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionGroupId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionDefinitionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_PERMISSION_GROUP_PERMISSIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_PERMISSION_GROUP_PERMISSIONS_RII_PERMISSION_DEFINITIONS_PermissionDefinitionId",
                        column: x => x.PermissionDefinitionId,
                        principalTable: "RII_PERMISSION_DEFINITIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_PERMISSION_GROUPS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsSystemAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_PERMISSION_GROUPS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    City = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    District = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_ErpNewsItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: true),
                    SourceUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_ErpNewsItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_FixedAssets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_FixedAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_GmailMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GmailMessageId = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Preview = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUnread = table.Column<bool>(type: "bit", nullable: false),
                    IsMeeting = table.Column<bool>(type: "bit", nullable: false),
                    ThreadId = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_GmailMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_InvoiceLines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    VatRate = table.Column<decimal>(type: "decimal(18,6)", precision: 9, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_InvoiceLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    QuoteId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,6)", precision: 9, scale: 2, nullable: false),
                    DiscountTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    VatTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_Invoices_RII_SD_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "RII_SD_Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_PotentialCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    City = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    District = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MatchScore = table.Column<int>(type: "int", nullable: false),
                    LastResearchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_PotentialCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_ProductCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    PotentialCustomerId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_ProductCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_ProductCustomers_RII_SD_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "RII_SD_Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_ProductCustomers_RII_SD_PotentialCustomers_PotentialCustomerId",
                        column: x => x.PotentialCustomerId,
                        principalTable: "RII_SD_PotentialCustomers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    SalesPrice = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    StockQuantity = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    MinimumStockQuantity = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    SearchText = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_QuoteLines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    VatRate = table.Column<decimal>(type: "decimal(18,6)", precision: 9, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_QuoteLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_QuoteLines_RII_SD_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "RII_SD_Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_Quotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    QuoteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    VatTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_Quotes_RII_SD_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "RII_SD_Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_RecurringPayments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DayOfMonth = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 2, nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_RecurringPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_RecurringPayments_RII_SD_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "RII_SD_Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_SoftwareResearches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PotentialCustomerId = table.Column<long>(type: "bigint", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Host = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: true),
                    SourceUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ResearchedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_SoftwareResearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_SoftwareResearches_RII_SD_PotentialCustomers_PotentialCustomerId",
                        column: x => x.PotentialCustomerId,
                        principalTable: "RII_SD_PotentialCustomers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_SystemSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DecimalPlaces = table.Column<int>(type: "int", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    DefaultVatRate = table.Column<decimal>(type: "decimal(18,6)", precision: 9, scale: 4, nullable: false),
                    MaxGeneralDiscountRate = table.Column<decimal>(type: "decimal(18,6)", precision: 9, scale: 4, nullable: false),
                    EnableGmailInbox = table.Column<bool>(type: "bit", nullable: false),
                    EnableSalesDeskNotifications = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_SystemSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_Tasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    AssignedUserId = table.Column<long>(type: "bigint", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_Tasks_RII_SD_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "RII_SD_Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_VisitForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    FormDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    OwnerUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_VisitForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_VisitForms_RII_SD_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "RII_SD_Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_Visits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    VisitType = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SD_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_Visits_RII_SD_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "RII_SD_Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SMTP_SETTING",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Host = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordEncrypted = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FromEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FromName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Timeout = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_SMTP_SETTING", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_USER_AUTHORITY",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_USER_AUTHORITY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RII_USERS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ManagerUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_USERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_USERS_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USERS_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USERS_RII_USERS_ManagerUserId",
                        column: x => x.ManagerUserId,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USERS_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USERS_RII_USER_AUTHORITY_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RII_USER_AUTHORITY",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_USER_DETAIL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Gender = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_USER_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_USER_DETAIL_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_DETAIL_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_DETAIL_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_DETAIL_RII_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_USER_PERMISSION_GROUPS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_USER_PERMISSION_GROUPS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_USER_PERMISSION_GROUPS_RII_PERMISSION_GROUPS_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalTable: "RII_PERMISSION_GROUPS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_PERMISSION_GROUPS_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_PERMISSION_GROUPS_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_PERMISSION_GROUPS_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_PERMISSION_GROUPS_RII_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_USER_SESSION",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DeviceInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_USER_SESSION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_USER_SESSION_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_SESSION_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_SESSION_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_SESSION_RII_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "RII_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RII_VISIBILITY_POLICY",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ScopeType = table.Column<int>(type: "int", nullable: false),
                    IncludeSelf = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_VISIBILITY_POLICY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_VISIBILITY_POLICY_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_VISIBILITY_POLICY_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_VISIBILITY_POLICY_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_USER_VISIBILITY_POLICY",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    VisibilityPolicyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestBranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RII_USER_VISIBILITY_POLICY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_USER_VISIBILITY_POLICY_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_VISIBILITY_POLICY_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_VISIBILITY_POLICY_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_VISIBILITY_POLICY_RII_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_USER_VISIBILITY_POLICY_RII_VISIBILITY_POLICY_VisibilityPolicyId",
                        column: x => x.VisibilityPolicyId,
                        principalTable: "RII_VISIBILITY_POLICY",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, true, true, "dashboard.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Dashboard - Goruntule", null, null, null },
                    { 2L, true, true, "search.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Genel Arama - Goruntule", null, null, null },
                    { 3L, true, true, "summary.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Ozet - Goruntule", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 4L, true, "settings.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Sistem Ayarlari - Goruntule", null, null, null },
                    { 5L, true, "settings.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Sistem Ayarlari - Duzenle", null, null, null },
                    { 6L, true, "users.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Kullanicilar - Goruntule", null, null, null },
                    { 7L, true, "users.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Kullanicilar - Olustur", null, null, null },
                    { 8L, true, "users.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Kullanicilar - Duzenle", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 9L, true, true, "salesdesk.customers.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Musteriler - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 10L, true, "salesdesk.customers.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Musteriler - Olustur", null, null, null },
                    { 11L, true, "salesdesk.customers.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Musteriler - Duzenle", null, null, null },
                    { 12L, true, "salesdesk.customers.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Musteriler - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 13L, true, true, "salesdesk.potentials.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Potansiyel Cariler - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 14L, true, "salesdesk.potentials.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Potansiyel Cariler - Olustur", null, null, null },
                    { 15L, true, "salesdesk.potentials.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Potansiyel Cariler - Duzenle", null, null, null },
                    { 16L, true, "salesdesk.potentials.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Potansiyel Cariler - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 17L, true, true, "salesdesk.products.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Stok Urunler - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 18L, true, "salesdesk.products.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Stok Urunler - Olustur", null, null, null },
                    { 19L, true, "salesdesk.products.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Stok Urunler - Duzenle", null, null, null },
                    { 20L, true, "salesdesk.products.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Stok Urunler - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 21L, true, true, "salesdesk.product-customers.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Urun Bazli Musteriler - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 22L, true, "salesdesk.product-customers.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Urun Bazli Musteriler - Olustur", null, null, null },
                    { 23L, true, "salesdesk.product-customers.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Urun Bazli Musteriler - Duzenle", null, null, null },
                    { 24L, true, "salesdesk.product-customers.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Urun Bazli Musteriler - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 25L, true, true, "salesdesk.quotes.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Teklifler - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 26L, true, "salesdesk.quotes.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Teklifler - Olustur", null, null, null },
                    { 27L, true, "salesdesk.quotes.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Teklifler - Duzenle", null, null, null },
                    { 28L, true, "salesdesk.quotes.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Teklifler - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 29L, true, true, "salesdesk.invoices.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Faturalar - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 30L, true, "salesdesk.invoices.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Faturalar - Olustur", null, null, null },
                    { 31L, true, "salesdesk.invoices.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Faturalar - Duzenle", null, null, null },
                    { 32L, true, "salesdesk.invoices.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Faturalar - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 33L, true, true, "salesdesk.tasks.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Acik Maddeler - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 34L, true, "salesdesk.tasks.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Acik Maddeler - Olustur", null, null, null },
                    { 35L, true, "salesdesk.tasks.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Acik Maddeler - Duzenle", null, null, null },
                    { 36L, true, "salesdesk.tasks.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Acik Maddeler - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 37L, true, true, "salesdesk.visits.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Haftalik Ziyaretler - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 38L, true, "salesdesk.visits.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Haftalik Ziyaretler - Olustur", null, null, null },
                    { 39L, true, "salesdesk.visits.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Haftalik Ziyaretler - Duzenle", null, null, null },
                    { 40L, true, "salesdesk.visits.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Haftalik Ziyaretler - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 41L, true, true, "salesdesk.visit-forms.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Ziyaret Formlari - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 42L, true, "salesdesk.visit-forms.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Ziyaret Formlari - Olustur", null, null, null },
                    { 43L, true, "salesdesk.visit-forms.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Ziyaret Formlari - Duzenle", null, null, null },
                    { 44L, true, "salesdesk.visit-forms.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Ziyaret Formlari - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 45L, true, true, "salesdesk.assets.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Demirbaslar - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 46L, true, "salesdesk.assets.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Demirbaslar - Olustur", null, null, null },
                    { 47L, true, "salesdesk.assets.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Demirbaslar - Duzenle", null, null, null },
                    { 48L, true, "salesdesk.assets.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Demirbaslar - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 49L, true, true, "salesdesk.recurring-payments.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Standart Odemeler - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 50L, true, "salesdesk.recurring-payments.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Standart Odemeler - Olustur", null, null, null },
                    { 51L, true, "salesdesk.recurring-payments.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Standart Odemeler - Duzenle", null, null, null },
                    { 52L, true, "salesdesk.recurring-payments.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Standart Odemeler - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 53L, true, true, "salesdesk.software-research.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Yazilim Arastirma - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 54L, true, "salesdesk.software-research.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Yazilim Arastirma - Olustur", null, null, null },
                    { 55L, true, "salesdesk.software-research.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Yazilim Arastirma - Duzenle", null, null, null },
                    { 56L, true, "salesdesk.software-research.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Yazilim Arastirma - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 57L, true, true, "salesdesk.erp-news.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "ERP Haber Takibi - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 58L, true, "salesdesk.erp-news.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "ERP Haber Takibi - Olustur", null, null, null },
                    { 59L, true, "salesdesk.erp-news.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "ERP Haber Takibi - Duzenle", null, null, null },
                    { 60L, true, "salesdesk.erp-news.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "ERP Haber Takibi - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnMobile", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 61L, true, true, "salesdesk.gmail-messages.view", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Gmail Mesajlari - Goruntule", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_DEFINITIONS",
                columns: new[] { "Id", "AvailableOnWeb", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 62L, true, "salesdesk.gmail-messages.create", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Gmail Mesajlari - Olustur", null, null, null },
                    { 63L, true, "salesdesk.gmail-messages.update", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Gmail Mesajlari - Duzenle", null, null, null },
                    { 64L, true, "salesdesk.gmail-messages.delete", null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, true, false, "Gmail Mesajlari - Sil", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RII_PERMISSION_GROUPS",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsActive", "IsDeleted", "IsSystemAdmin", "Name", "RequestBranchCode", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1L, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Full system access", true, false, true, "System Admin", null, null, null });

            migrationBuilder.InsertData(
                table: "RII_USER_AUTHORITY",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "RequestBranchCode", "Title", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, "User", null, null },
                    { 2L, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, "Manager", null, null },
                    { 3L, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, "Admin", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFieldLabels_CreatedBy",
                table: "DocumentFieldLabels",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFieldLabels_DeletedBy",
                table: "DocumentFieldLabels",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFieldLabels_DocumentType_Scope_SortOrder",
                table: "DocumentFieldLabels",
                columns: new[] { "DocumentType", "Scope", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFieldLabels_UpdatedBy",
                table: "DocumentFieldLabels",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UX_DocumentFieldLabels_DocumentType_Scope_FieldKey",
                table: "DocumentFieldLabels",
                columns: new[] { "DocumentType", "Scope", "FieldKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedBy",
                table: "Notifications",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DeletedBy",
                table: "Notifications",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IsRead",
                table: "Notifications",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UpdatedBy",
                table: "Notifications",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_EntityType_EntityId_CreatedDate",
                table: "RII_AUDIT_LOG",
                columns: new[] { "EntityType", "EntityId", "CreatedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_PerformedByUserId_CreatedDate",
                table: "RII_AUDIT_LOG",
                columns: new[] { "PerformedByUserId", "CreatedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_TraceId",
                table: "RII_AUDIT_LOG",
                column: "TraceId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_AUDIT_LOG_CreatedBy",
                table: "RII_AUDIT_LOG",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_AUDIT_LOG_DeletedBy",
                table: "RII_AUDIT_LOG",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_AUDIT_LOG_UpdatedBy",
                table: "RII_AUDIT_LOG",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JobExecutionLog_FinishedAt",
                table: "RII_JOB_EXECUTION_LOG",
                column: "FinishedAt");

            migrationBuilder.CreateIndex(
                name: "IX_JobExecutionLog_JobId",
                table: "RII_JOB_EXECUTION_LOG",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobExecutionLog_JobName",
                table: "RII_JOB_EXECUTION_LOG",
                column: "JobName");

            migrationBuilder.CreateIndex(
                name: "IX_JobExecutionLog_RecurringJobId",
                table: "RII_JOB_EXECUTION_LOG",
                column: "RecurringJobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobExecutionLog_Status",
                table: "RII_JOB_EXECUTION_LOG",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_RII_JOB_EXECUTION_LOG_CreatedBy",
                table: "RII_JOB_EXECUTION_LOG",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_JOB_EXECUTION_LOG_DeletedBy",
                table: "RII_JOB_EXECUTION_LOG",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_JOB_EXECUTION_LOG_UpdatedBy",
                table: "RII_JOB_EXECUTION_LOG",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JobFailureLog_FailedAt",
                table: "RII_JOB_FAILURE_LOG",
                column: "FailedAt");

            migrationBuilder.CreateIndex(
                name: "IX_JobFailureLog_JobId",
                table: "RII_JOB_FAILURE_LOG",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobFailureLog_JobName",
                table: "RII_JOB_FAILURE_LOG",
                column: "JobName");

            migrationBuilder.CreateIndex(
                name: "IX_RII_JOB_FAILURE_LOG_CreatedBy",
                table: "RII_JOB_FAILURE_LOG",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_JOB_FAILURE_LOG_DeletedBy",
                table: "RII_JOB_FAILURE_LOG",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_JOB_FAILURE_LOG_UpdatedBy",
                table: "RII_JOB_FAILURE_LOG",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PASSWORD_RESET_REQUEST_CreatedBy",
                table: "RII_PASSWORD_RESET_REQUEST",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PASSWORD_RESET_REQUEST_DeletedBy",
                table: "RII_PASSWORD_RESET_REQUEST",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PASSWORD_RESET_REQUEST_UpdatedBy",
                table: "RII_PASSWORD_RESET_REQUEST",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PASSWORD_RESET_REQUEST_UserId",
                table: "RII_PASSWORD_RESET_REQUEST",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionDefinitions_Code",
                table: "RII_PERMISSION_DEFINITIONS",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionDefinitions_IsDeleted",
                table: "RII_PERMISSION_DEFINITIONS",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_DEFINITIONS_CreatedBy",
                table: "RII_PERMISSION_DEFINITIONS",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_DEFINITIONS_DeletedBy",
                table: "RII_PERMISSION_DEFINITIONS",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_DEFINITIONS_UpdatedBy",
                table: "RII_PERMISSION_DEFINITIONS",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupPermission_GroupId_DefinitionId",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                columns: new[] { "PermissionGroupId", "PermissionDefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupPermission_IsDeleted",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_GROUP_PERMISSIONS_CreatedBy",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_GROUP_PERMISSIONS_DeletedBy",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_GROUP_PERMISSIONS_PermissionDefinitionId",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "PermissionDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_GROUP_PERMISSIONS_UpdatedBy",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroups_IsDeleted",
                table: "RII_PERMISSION_GROUPS",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroups_Name",
                table: "RII_PERMISSION_GROUPS",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_GROUPS_CreatedBy",
                table: "RII_PERMISSION_GROUPS",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_GROUPS_DeletedBy",
                table: "RII_PERMISSION_GROUPS",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_PERMISSION_GROUPS_UpdatedBy",
                table: "RII_PERMISSION_GROUPS",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Customers_Code",
                table: "RII_SD_Customers",
                column: "Code",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Customers_CreatedBy",
                table: "RII_SD_Customers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Customers_DeletedBy",
                table: "RII_SD_Customers",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Customers_Name",
                table: "RII_SD_Customers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Customers_UpdatedBy",
                table: "RII_SD_Customers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ErpNewsItems_CreatedBy",
                table: "RII_SD_ErpNewsItems",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ErpNewsItems_DeletedBy",
                table: "RII_SD_ErpNewsItems",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ErpNewsItems_Topic_IsCritical_IsRead_PublishedAt",
                table: "RII_SD_ErpNewsItems",
                columns: new[] { "Topic", "IsCritical", "IsRead", "PublishedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ErpNewsItems_UpdatedBy",
                table: "RII_SD_ErpNewsItems",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_FixedAssets_Code",
                table: "RII_SD_FixedAssets",
                column: "Code",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_FixedAssets_CreatedBy",
                table: "RII_SD_FixedAssets",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_FixedAssets_DeletedBy",
                table: "RII_SD_FixedAssets",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_FixedAssets_UpdatedBy",
                table: "RII_SD_FixedAssets",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GmailMessages_CreatedBy",
                table: "RII_SD_GmailMessages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GmailMessages_DeletedBy",
                table: "RII_SD_GmailMessages",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GmailMessages_GmailMessageId",
                table: "RII_SD_GmailMessages",
                column: "GmailMessageId",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GmailMessages_IsUnread_IsMeeting_ReceivedAt",
                table: "RII_SD_GmailMessages",
                columns: new[] { "IsUnread", "IsMeeting", "ReceivedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GmailMessages_UpdatedBy",
                table: "RII_SD_GmailMessages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_InvoiceLines_CreatedBy",
                table: "RII_SD_InvoiceLines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_InvoiceLines_DeletedBy",
                table: "RII_SD_InvoiceLines",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_InvoiceLines_InvoiceId",
                table: "RII_SD_InvoiceLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_InvoiceLines_ProductId",
                table: "RII_SD_InvoiceLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_InvoiceLines_UpdatedBy",
                table: "RII_SD_InvoiceLines",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Invoices_CreatedBy",
                table: "RII_SD_Invoices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Invoices_CustomerId",
                table: "RII_SD_Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Invoices_DeletedBy",
                table: "RII_SD_Invoices",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Invoices_InvoiceNumber",
                table: "RII_SD_Invoices",
                column: "InvoiceNumber",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Invoices_QuoteId",
                table: "RII_SD_Invoices",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Invoices_Status_InvoiceDate",
                table: "RII_SD_Invoices",
                columns: new[] { "Status", "InvoiceDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Invoices_UpdatedBy",
                table: "RII_SD_Invoices",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_PotentialCustomers_Code",
                table: "RII_SD_PotentialCustomers",
                column: "Code",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_PotentialCustomers_CompanyName",
                table: "RII_SD_PotentialCustomers",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_PotentialCustomers_CreatedBy",
                table: "RII_SD_PotentialCustomers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_PotentialCustomers_DeletedBy",
                table: "RII_SD_PotentialCustomers",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_PotentialCustomers_UpdatedBy",
                table: "RII_SD_PotentialCustomers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ProductCustomers_CreatedBy",
                table: "RII_SD_ProductCustomers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ProductCustomers_CustomerId",
                table: "RII_SD_ProductCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ProductCustomers_DeletedBy",
                table: "RII_SD_ProductCustomers",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ProductCustomers_PotentialCustomerId",
                table: "RII_SD_ProductCustomers",
                column: "PotentialCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ProductCustomers_ProductId_CustomerId_PotentialCustomerId",
                table: "RII_SD_ProductCustomers",
                columns: new[] { "ProductId", "CustomerId", "PotentialCustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_ProductCustomers_UpdatedBy",
                table: "RII_SD_ProductCustomers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Products_Code",
                table: "RII_SD_Products",
                column: "Code",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Products_CreatedBy",
                table: "RII_SD_Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Products_DeletedBy",
                table: "RII_SD_Products",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Products_SearchText",
                table: "RII_SD_Products",
                column: "SearchText");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Products_UpdatedBy",
                table: "RII_SD_Products",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_QuoteLines_CreatedBy",
                table: "RII_SD_QuoteLines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_QuoteLines_DeletedBy",
                table: "RII_SD_QuoteLines",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_QuoteLines_ProductId",
                table: "RII_SD_QuoteLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_QuoteLines_QuoteId",
                table: "RII_SD_QuoteLines",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_QuoteLines_UpdatedBy",
                table: "RII_SD_QuoteLines",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Quotes_CreatedBy",
                table: "RII_SD_Quotes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Quotes_CustomerId",
                table: "RII_SD_Quotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Quotes_DeletedBy",
                table: "RII_SD_Quotes",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Quotes_QuoteNumber",
                table: "RII_SD_Quotes",
                column: "QuoteNumber",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Quotes_Status_QuoteDate",
                table: "RII_SD_Quotes",
                columns: new[] { "Status", "QuoteDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Quotes_UpdatedBy",
                table: "RII_SD_Quotes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_RecurringPayments_CreatedBy",
                table: "RII_SD_RecurringPayments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_RecurringPayments_CustomerId",
                table: "RII_SD_RecurringPayments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_RecurringPayments_DeletedBy",
                table: "RII_SD_RecurringPayments",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_RecurringPayments_IsActive_DayOfMonth",
                table: "RII_SD_RecurringPayments",
                columns: new[] { "IsActive", "DayOfMonth" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_RecurringPayments_UpdatedBy",
                table: "RII_SD_RecurringPayments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_SoftwareResearches_CreatedBy",
                table: "RII_SD_SoftwareResearches",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_SoftwareResearches_DeletedBy",
                table: "RII_SD_SoftwareResearches",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_SoftwareResearches_PotentialCustomerId",
                table: "RII_SD_SoftwareResearches",
                column: "PotentialCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_SoftwareResearches_Provider_Status_Score",
                table: "RII_SD_SoftwareResearches",
                columns: new[] { "Provider", "Status", "Score" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_SoftwareResearches_UpdatedBy",
                table: "RII_SD_SoftwareResearches",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_SystemSettings_CreatedBy",
                table: "RII_SD_SystemSettings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_SystemSettings_DeletedBy",
                table: "RII_SD_SystemSettings",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_SystemSettings_UpdatedBy",
                table: "RII_SD_SystemSettings",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Tasks_CreatedBy",
                table: "RII_SD_Tasks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Tasks_CustomerId",
                table: "RII_SD_Tasks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Tasks_DeletedBy",
                table: "RII_SD_Tasks",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Tasks_Status_Priority_DueDate",
                table: "RII_SD_Tasks",
                columns: new[] { "Status", "Priority", "DueDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Tasks_UpdatedBy",
                table: "RII_SD_Tasks",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_VisitForms_CreatedBy",
                table: "RII_SD_VisitForms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_VisitForms_CustomerId",
                table: "RII_SD_VisitForms",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_VisitForms_DeletedBy",
                table: "RII_SD_VisitForms",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_VisitForms_UpdatedBy",
                table: "RII_SD_VisitForms",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_VisitForms_VisitId",
                table: "RII_SD_VisitForms",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Visits_CreatedBy",
                table: "RII_SD_Visits",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Visits_CustomerId",
                table: "RII_SD_Visits",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Visits_DeletedBy",
                table: "RII_SD_Visits",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Visits_UpdatedBy",
                table: "RII_SD_Visits",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_Visits_VisitDate_Status",
                table: "RII_SD_Visits",
                columns: new[] { "VisitDate", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SMTP_SETTING_CreatedByUserId",
                table: "RII_SMTP_SETTING",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SMTP_SETTING_DeletedByUserId",
                table: "RII_SMTP_SETTING",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SMTP_SETTING_UpdatedByUserId",
                table: "RII_SMTP_SETTING",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_AUTHORITY_CreatedBy",
                table: "RII_USER_AUTHORITY",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_AUTHORITY_DeletedBy",
                table: "RII_USER_AUTHORITY",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_AUTHORITY_UpdatedBy",
                table: "RII_USER_AUTHORITY",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthority_IsDeleted",
                table: "RII_USER_AUTHORITY",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthority_Title",
                table: "RII_USER_AUTHORITY",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_DETAIL_CreatedBy",
                table: "RII_USER_DETAIL",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_DETAIL_DeletedBy",
                table: "RII_USER_DETAIL",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_DETAIL_UpdatedBy",
                table: "RII_USER_DETAIL",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_IsDeleted",
                table: "RII_USER_DETAIL",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_UserId",
                table: "RII_USER_DETAIL",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_PERMISSION_GROUPS_CreatedBy",
                table: "RII_USER_PERMISSION_GROUPS",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_PERMISSION_GROUPS_DeletedBy",
                table: "RII_USER_PERMISSION_GROUPS",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_PERMISSION_GROUPS_PermissionGroupId",
                table: "RII_USER_PERMISSION_GROUPS",
                column: "PermissionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_PERMISSION_GROUPS_UpdatedBy",
                table: "RII_USER_PERMISSION_GROUPS",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionGroup_IsDeleted",
                table: "RII_USER_PERMISSION_GROUPS",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionGroup_UserId_GroupId",
                table: "RII_USER_PERMISSION_GROUPS",
                columns: new[] { "UserId", "PermissionGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_SESSION_CreatedBy",
                table: "RII_USER_SESSION",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_SESSION_DeletedBy",
                table: "RII_USER_SESSION",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_SESSION_UpdatedBy",
                table: "RII_USER_SESSION",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_IsDeleted",
                table: "RII_USER_SESSION",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_RevokedAt",
                table: "RII_USER_SESSION",
                column: "RevokedAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_SessionId",
                table: "RII_USER_SESSION",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UserId",
                table: "RII_USER_SESSION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_VISIBILITY_POLICY_CreatedBy",
                table: "RII_USER_VISIBILITY_POLICY",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_VISIBILITY_POLICY_DeletedBy",
                table: "RII_USER_VISIBILITY_POLICY",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_VISIBILITY_POLICY_UpdatedBy",
                table: "RII_USER_VISIBILITY_POLICY",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USER_VISIBILITY_POLICY_VisibilityPolicyId",
                table: "RII_USER_VISIBILITY_POLICY",
                column: "VisibilityPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVisibilityPolicy_UserId",
                table: "RII_USER_VISIBILITY_POLICY",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVisibilityPolicy_UserId_VisibilityPolicyId",
                table: "RII_USER_VISIBILITY_POLICY",
                columns: new[] { "UserId", "VisibilityPolicyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RII_USERS_CreatedBy",
                table: "RII_USERS",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USERS_DeletedBy",
                table: "RII_USERS",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USERS_RoleId",
                table: "RII_USERS",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_USERS_UpdatedBy",
                table: "RII_USERS",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "RII_USERS",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDeleted",
                table: "RII_USERS",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ManagerUserId",
                table: "RII_USERS",
                column: "ManagerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "RII_USERS",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RII_VISIBILITY_POLICY_CreatedBy",
                table: "RII_VISIBILITY_POLICY",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_VISIBILITY_POLICY_DeletedBy",
                table: "RII_VISIBILITY_POLICY",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_VISIBILITY_POLICY_UpdatedBy",
                table: "RII_VISIBILITY_POLICY",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisibilityPolicy_Code",
                table: "RII_VISIBILITY_POLICY",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisibilityPolicy_EntityType_IsActive",
                table: "RII_VISIBILITY_POLICY",
                columns: new[] { "EntityType", "IsActive" });

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFieldLabels_RII_USERS_CreatedBy",
                table: "DocumentFieldLabels",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFieldLabels_RII_USERS_DeletedBy",
                table: "DocumentFieldLabels",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFieldLabels_RII_USERS_UpdatedBy",
                table: "DocumentFieldLabels",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_RII_USERS_CreatedBy",
                table: "Notifications",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_RII_USERS_DeletedBy",
                table: "Notifications",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_RII_USERS_UpdatedBy",
                table: "Notifications",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_RII_USERS_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "RII_USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RII_AUDIT_LOG_RII_USERS_CreatedBy",
                table: "RII_AUDIT_LOG",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_AUDIT_LOG_RII_USERS_DeletedBy",
                table: "RII_AUDIT_LOG",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_AUDIT_LOG_RII_USERS_UpdatedBy",
                table: "RII_AUDIT_LOG",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_JOB_EXECUTION_LOG_RII_USERS_CreatedBy",
                table: "RII_JOB_EXECUTION_LOG",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_JOB_EXECUTION_LOG_RII_USERS_DeletedBy",
                table: "RII_JOB_EXECUTION_LOG",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_JOB_EXECUTION_LOG_RII_USERS_UpdatedBy",
                table: "RII_JOB_EXECUTION_LOG",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_JOB_FAILURE_LOG_RII_USERS_CreatedBy",
                table: "RII_JOB_FAILURE_LOG",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_JOB_FAILURE_LOG_RII_USERS_DeletedBy",
                table: "RII_JOB_FAILURE_LOG",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_JOB_FAILURE_LOG_RII_USERS_UpdatedBy",
                table: "RII_JOB_FAILURE_LOG",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PASSWORD_RESET_REQUEST_RII_USERS_CreatedBy",
                table: "RII_PASSWORD_RESET_REQUEST",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PASSWORD_RESET_REQUEST_RII_USERS_DeletedBy",
                table: "RII_PASSWORD_RESET_REQUEST",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PASSWORD_RESET_REQUEST_RII_USERS_UpdatedBy",
                table: "RII_PASSWORD_RESET_REQUEST",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PASSWORD_RESET_REQUEST_RII_USERS_UserId",
                table: "RII_PASSWORD_RESET_REQUEST",
                column: "UserId",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_DEFINITIONS_RII_USERS_CreatedBy",
                table: "RII_PERMISSION_DEFINITIONS",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_DEFINITIONS_RII_USERS_DeletedBy",
                table: "RII_PERMISSION_DEFINITIONS",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_DEFINITIONS_RII_USERS_UpdatedBy",
                table: "RII_PERMISSION_DEFINITIONS",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_GROUP_PERMISSIONS_RII_PERMISSION_GROUPS_PermissionGroupId",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "PermissionGroupId",
                principalTable: "RII_PERMISSION_GROUPS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_GROUP_PERMISSIONS_RII_USERS_CreatedBy",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_GROUP_PERMISSIONS_RII_USERS_DeletedBy",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_GROUP_PERMISSIONS_RII_USERS_UpdatedBy",
                table: "RII_PERMISSION_GROUP_PERMISSIONS",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_GROUPS_RII_USERS_CreatedBy",
                table: "RII_PERMISSION_GROUPS",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_GROUPS_RII_USERS_DeletedBy",
                table: "RII_PERMISSION_GROUPS",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_PERMISSION_GROUPS_RII_USERS_UpdatedBy",
                table: "RII_PERMISSION_GROUPS",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Customers_RII_USERS_CreatedBy",
                table: "RII_SD_Customers",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Customers_RII_USERS_DeletedBy",
                table: "RII_SD_Customers",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Customers_RII_USERS_UpdatedBy",
                table: "RII_SD_Customers",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ErpNewsItems_RII_USERS_CreatedBy",
                table: "RII_SD_ErpNewsItems",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ErpNewsItems_RII_USERS_DeletedBy",
                table: "RII_SD_ErpNewsItems",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ErpNewsItems_RII_USERS_UpdatedBy",
                table: "RII_SD_ErpNewsItems",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_FixedAssets_RII_USERS_CreatedBy",
                table: "RII_SD_FixedAssets",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_FixedAssets_RII_USERS_DeletedBy",
                table: "RII_SD_FixedAssets",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_FixedAssets_RII_USERS_UpdatedBy",
                table: "RII_SD_FixedAssets",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_GmailMessages_RII_USERS_CreatedBy",
                table: "RII_SD_GmailMessages",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_GmailMessages_RII_USERS_DeletedBy",
                table: "RII_SD_GmailMessages",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_GmailMessages_RII_USERS_UpdatedBy",
                table: "RII_SD_GmailMessages",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_SD_Invoices_InvoiceId",
                table: "RII_SD_InvoiceLines",
                column: "InvoiceId",
                principalTable: "RII_SD_Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_SD_Products_ProductId",
                table: "RII_SD_InvoiceLines",
                column: "ProductId",
                principalTable: "RII_SD_Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_USERS_CreatedBy",
                table: "RII_SD_InvoiceLines",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_USERS_DeletedBy",
                table: "RII_SD_InvoiceLines",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_USERS_UpdatedBy",
                table: "RII_SD_InvoiceLines",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Invoices_RII_SD_Quotes_QuoteId",
                table: "RII_SD_Invoices",
                column: "QuoteId",
                principalTable: "RII_SD_Quotes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Invoices_RII_USERS_CreatedBy",
                table: "RII_SD_Invoices",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Invoices_RII_USERS_DeletedBy",
                table: "RII_SD_Invoices",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Invoices_RII_USERS_UpdatedBy",
                table: "RII_SD_Invoices",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PotentialCustomers_RII_USERS_CreatedBy",
                table: "RII_SD_PotentialCustomers",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PotentialCustomers_RII_USERS_DeletedBy",
                table: "RII_SD_PotentialCustomers",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PotentialCustomers_RII_USERS_UpdatedBy",
                table: "RII_SD_PotentialCustomers",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_SD_Products_ProductId",
                table: "RII_SD_ProductCustomers",
                column: "ProductId",
                principalTable: "RII_SD_Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_USERS_CreatedBy",
                table: "RII_SD_ProductCustomers",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_USERS_DeletedBy",
                table: "RII_SD_ProductCustomers",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_USERS_UpdatedBy",
                table: "RII_SD_ProductCustomers",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Products_RII_USERS_CreatedBy",
                table: "RII_SD_Products",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Products_RII_USERS_DeletedBy",
                table: "RII_SD_Products",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Products_RII_USERS_UpdatedBy",
                table: "RII_SD_Products",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_SD_Quotes_QuoteId",
                table: "RII_SD_QuoteLines",
                column: "QuoteId",
                principalTable: "RII_SD_Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_USERS_CreatedBy",
                table: "RII_SD_QuoteLines",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_USERS_DeletedBy",
                table: "RII_SD_QuoteLines",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_USERS_UpdatedBy",
                table: "RII_SD_QuoteLines",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Quotes_RII_USERS_CreatedBy",
                table: "RII_SD_Quotes",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Quotes_RII_USERS_DeletedBy",
                table: "RII_SD_Quotes",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Quotes_RII_USERS_UpdatedBy",
                table: "RII_SD_Quotes",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_RecurringPayments_RII_USERS_CreatedBy",
                table: "RII_SD_RecurringPayments",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_RecurringPayments_RII_USERS_DeletedBy",
                table: "RII_SD_RecurringPayments",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_RecurringPayments_RII_USERS_UpdatedBy",
                table: "RII_SD_RecurringPayments",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SoftwareResearches_RII_USERS_CreatedBy",
                table: "RII_SD_SoftwareResearches",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SoftwareResearches_RII_USERS_DeletedBy",
                table: "RII_SD_SoftwareResearches",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SoftwareResearches_RII_USERS_UpdatedBy",
                table: "RII_SD_SoftwareResearches",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SystemSettings_RII_USERS_CreatedBy",
                table: "RII_SD_SystemSettings",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SystemSettings_RII_USERS_DeletedBy",
                table: "RII_SD_SystemSettings",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SystemSettings_RII_USERS_UpdatedBy",
                table: "RII_SD_SystemSettings",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Tasks_RII_USERS_CreatedBy",
                table: "RII_SD_Tasks",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Tasks_RII_USERS_DeletedBy",
                table: "RII_SD_Tasks",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Tasks_RII_USERS_UpdatedBy",
                table: "RII_SD_Tasks",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VisitForms_RII_SD_Visits_VisitId",
                table: "RII_SD_VisitForms",
                column: "VisitId",
                principalTable: "RII_SD_Visits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VisitForms_RII_USERS_CreatedBy",
                table: "RII_SD_VisitForms",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VisitForms_RII_USERS_DeletedBy",
                table: "RII_SD_VisitForms",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VisitForms_RII_USERS_UpdatedBy",
                table: "RII_SD_VisitForms",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Visits_RII_USERS_CreatedBy",
                table: "RII_SD_Visits",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Visits_RII_USERS_DeletedBy",
                table: "RII_SD_Visits",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_Visits_RII_USERS_UpdatedBy",
                table: "RII_SD_Visits",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SMTP_SETTING_RII_USERS_CreatedByUserId",
                table: "RII_SMTP_SETTING",
                column: "CreatedByUserId",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SMTP_SETTING_RII_USERS_DeletedByUserId",
                table: "RII_SMTP_SETTING",
                column: "DeletedByUserId",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SMTP_SETTING_RII_USERS_UpdatedByUserId",
                table: "RII_SMTP_SETTING",
                column: "UpdatedByUserId",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_USER_AUTHORITY_RII_USERS_CreatedBy",
                table: "RII_USER_AUTHORITY",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_USER_AUTHORITY_RII_USERS_DeletedBy",
                table: "RII_USER_AUTHORITY",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_USER_AUTHORITY_RII_USERS_UpdatedBy",
                table: "RII_USER_AUTHORITY",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RII_USER_AUTHORITY_RII_USERS_CreatedBy",
                table: "RII_USER_AUTHORITY");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_USER_AUTHORITY_RII_USERS_DeletedBy",
                table: "RII_USER_AUTHORITY");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_USER_AUTHORITY_RII_USERS_UpdatedBy",
                table: "RII_USER_AUTHORITY");

            migrationBuilder.DropTable(
                name: "DocumentFieldLabels");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RII_AUDIT_LOG");

            migrationBuilder.DropTable(
                name: "RII_JOB_EXECUTION_LOG");

            migrationBuilder.DropTable(
                name: "RII_JOB_FAILURE_LOG");

            migrationBuilder.DropTable(
                name: "RII_PASSWORD_RESET_REQUEST");

            migrationBuilder.DropTable(
                name: "RII_PERMISSION_GROUP_PERMISSIONS");

            migrationBuilder.DropTable(
                name: "RII_SD_ErpNewsItems");

            migrationBuilder.DropTable(
                name: "RII_SD_FixedAssets");

            migrationBuilder.DropTable(
                name: "RII_SD_GmailMessages");

            migrationBuilder.DropTable(
                name: "RII_SD_InvoiceLines");

            migrationBuilder.DropTable(
                name: "RII_SD_ProductCustomers");

            migrationBuilder.DropTable(
                name: "RII_SD_QuoteLines");

            migrationBuilder.DropTable(
                name: "RII_SD_RecurringPayments");

            migrationBuilder.DropTable(
                name: "RII_SD_SoftwareResearches");

            migrationBuilder.DropTable(
                name: "RII_SD_SystemSettings");

            migrationBuilder.DropTable(
                name: "RII_SD_Tasks");

            migrationBuilder.DropTable(
                name: "RII_SD_VisitForms");

            migrationBuilder.DropTable(
                name: "RII_SMTP_SETTING");

            migrationBuilder.DropTable(
                name: "RII_USER_DETAIL");

            migrationBuilder.DropTable(
                name: "RII_USER_PERMISSION_GROUPS");

            migrationBuilder.DropTable(
                name: "RII_USER_SESSION");

            migrationBuilder.DropTable(
                name: "RII_USER_VISIBILITY_POLICY");

            migrationBuilder.DropTable(
                name: "RII_PERMISSION_DEFINITIONS");

            migrationBuilder.DropTable(
                name: "RII_SD_Invoices");

            migrationBuilder.DropTable(
                name: "RII_SD_Products");

            migrationBuilder.DropTable(
                name: "RII_SD_PotentialCustomers");

            migrationBuilder.DropTable(
                name: "RII_SD_Visits");

            migrationBuilder.DropTable(
                name: "RII_PERMISSION_GROUPS");

            migrationBuilder.DropTable(
                name: "RII_VISIBILITY_POLICY");

            migrationBuilder.DropTable(
                name: "RII_SD_Quotes");

            migrationBuilder.DropTable(
                name: "RII_SD_Customers");

            migrationBuilder.DropTable(
                name: "RII_USERS");

            migrationBuilder.DropTable(
                name: "RII_USER_AUTHORITY");
        }
    }
}
