using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salesdesk_api.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesDeskGroupsCompaniesNotesInvoiceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RII_SD_INVOICE_InvoiceNumber",
                table: "RII_SD_INVOICE");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceType",
                table: "RII_SD_INVOICE",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "RII_SD_COMPANY",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    IpUsername = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    IpPassword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VpnName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    VpnUsername = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    VpnPassword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VpnIpAddress = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    VpnPort = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DatabaseUsername = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DatabasePassword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LoginUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description1 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
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
                    table.PrimaryKey("PK_RII_SD_COMPANY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_COMPANY_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_COMPANY_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_COMPANY_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_GROUP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_RII_SD_GROUP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_GROUP_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_GROUP_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_GROUP_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_NOTE",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
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
                    table.PrimaryKey("PK_RII_SD_NOTE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_GROUP_MEMBER",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RII_SD_GROUP_MEMBER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_GROUP_MEMBER_RII_SD_GROUP_GroupId",
                        column: x => x.GroupId,
                        principalTable: "RII_SD_GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RII_SD_GROUP_MEMBER_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_GROUP_MEMBER_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_GROUP_MEMBER_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_NOTE_NOTIFICATION",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<long>(type: "bigint", nullable: false),
                    RecipientUserId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_RII_SD_NOTE_NOTIFICATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_NOTIFICATION_RII_SD_NOTE_NoteId",
                        column: x => x.NoteId,
                        principalTable: "RII_SD_NOTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_NOTIFICATION_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_NOTIFICATION_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_NOTIFICATION_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RII_SD_NOTE_RECIPIENT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RII_SD_NOTE_RECIPIENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_RECIPIENT_RII_SD_NOTE_NoteId",
                        column: x => x.NoteId,
                        principalTable: "RII_SD_NOTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_RECIPIENT_RII_USERS_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_RECIPIENT_RII_USERS_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RII_SD_NOTE_RECIPIENT_RII_USERS_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "RII_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_TASK_GroupName",
                table: "RII_SD_TASK",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_QUOTE_QuoteDate",
                table: "RII_SD_QUOTE",
                column: "QuoteDate");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_INVOICE_InvoiceDate",
                table: "RII_SD_INVOICE",
                column: "InvoiceDate");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_INVOICE_InvoiceNumber_InvoiceType",
                table: "RII_SD_INVOICE",
                columns: new[] { "InvoiceNumber", "InvoiceType" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_COMPANY_CreatedBy",
                table: "RII_SD_COMPANY",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_COMPANY_DeletedBy",
                table: "RII_SD_COMPANY",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_COMPANY_Name",
                table: "RII_SD_COMPANY",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_COMPANY_UpdatedBy",
                table: "RII_SD_COMPANY",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_CreatedBy",
                table: "RII_SD_GROUP",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_DeletedBy",
                table: "RII_SD_GROUP",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_Name",
                table: "RII_SD_GROUP",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_UpdatedBy",
                table: "RII_SD_GROUP",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_MEMBER_CreatedBy",
                table: "RII_SD_GROUP_MEMBER",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_MEMBER_DeletedBy",
                table: "RII_SD_GROUP_MEMBER",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_MEMBER_GroupId_UserId",
                table: "RII_SD_GROUP_MEMBER",
                columns: new[] { "GroupId", "UserId" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_MEMBER_UpdatedBy",
                table: "RII_SD_GROUP_MEMBER",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_GROUP_MEMBER_UserId",
                table: "RII_SD_GROUP_MEMBER",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_CreatedBy",
                table: "RII_SD_NOTE",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_CreatedByUserId",
                table: "RII_SD_NOTE",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_DeletedBy",
                table: "RII_SD_NOTE",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_UpdatedBy",
                table: "RII_SD_NOTE",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_NOTIFICATION_CreatedBy",
                table: "RII_SD_NOTE_NOTIFICATION",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_NOTIFICATION_DeletedBy",
                table: "RII_SD_NOTE_NOTIFICATION",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_NOTIFICATION_NoteId",
                table: "RII_SD_NOTE_NOTIFICATION",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_NOTIFICATION_RecipientUserId_DeliveredAt",
                table: "RII_SD_NOTE_NOTIFICATION",
                columns: new[] { "RecipientUserId", "DeliveredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_NOTIFICATION_UpdatedBy",
                table: "RII_SD_NOTE_NOTIFICATION",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_RECIPIENT_CreatedBy",
                table: "RII_SD_NOTE_RECIPIENT",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_RECIPIENT_DeletedBy",
                table: "RII_SD_NOTE_RECIPIENT",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_RECIPIENT_NoteId_UserId",
                table: "RII_SD_NOTE_RECIPIENT",
                columns: new[] { "NoteId", "UserId" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_RECIPIENT_UpdatedBy",
                table: "RII_SD_NOTE_RECIPIENT",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_NOTE_RECIPIENT_UserId",
                table: "RII_SD_NOTE_RECIPIENT",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RII_SD_COMPANY");

            migrationBuilder.DropTable(
                name: "RII_SD_GROUP_MEMBER");

            migrationBuilder.DropTable(
                name: "RII_SD_NOTE_NOTIFICATION");

            migrationBuilder.DropTable(
                name: "RII_SD_NOTE_RECIPIENT");

            migrationBuilder.DropTable(
                name: "RII_SD_GROUP");

            migrationBuilder.DropTable(
                name: "RII_SD_NOTE");

            migrationBuilder.DropIndex(
                name: "IX_RII_SD_TASK_GroupName",
                table: "RII_SD_TASK");

            migrationBuilder.DropIndex(
                name: "IX_RII_SD_QUOTE_QuoteDate",
                table: "RII_SD_QUOTE");

            migrationBuilder.DropIndex(
                name: "IX_RII_SD_INVOICE_InvoiceDate",
                table: "RII_SD_INVOICE");

            migrationBuilder.DropIndex(
                name: "IX_RII_SD_INVOICE_InvoiceNumber_InvoiceType",
                table: "RII_SD_INVOICE");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "RII_SD_INVOICE");

            migrationBuilder.CreateIndex(
                name: "IX_RII_SD_INVOICE_InvoiceNumber",
                table: "RII_SD_INVOICE",
                column: "InvoiceNumber",
                unique: true,
                filter: "[IsDeleted] = 0");
        }
    }
}
