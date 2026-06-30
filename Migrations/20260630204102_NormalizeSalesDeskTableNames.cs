using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salesdesk_api.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeSalesDeskTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFieldLabels_RII_USERS_CreatedBy",
                table: "DocumentFieldLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFieldLabels_RII_USERS_DeletedBy",
                table: "DocumentFieldLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFieldLabels_RII_USERS_UpdatedBy",
                table: "DocumentFieldLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_RII_USERS_CreatedBy",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_RII_USERS_DeletedBy",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_RII_USERS_UpdatedBy",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_RII_USERS_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Customers_RII_USERS_CreatedBy",
                table: "RII_SD_Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Customers_RII_USERS_DeletedBy",
                table: "RII_SD_Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Customers_RII_USERS_UpdatedBy",
                table: "RII_SD_Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ErpNewsItems_RII_USERS_CreatedBy",
                table: "RII_SD_ErpNewsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ErpNewsItems_RII_USERS_DeletedBy",
                table: "RII_SD_ErpNewsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ErpNewsItems_RII_USERS_UpdatedBy",
                table: "RII_SD_ErpNewsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_FixedAssets_RII_USERS_CreatedBy",
                table: "RII_SD_FixedAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_FixedAssets_RII_USERS_DeletedBy",
                table: "RII_SD_FixedAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_FixedAssets_RII_USERS_UpdatedBy",
                table: "RII_SD_FixedAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_GmailMessages_RII_USERS_CreatedBy",
                table: "RII_SD_GmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_GmailMessages_RII_USERS_DeletedBy",
                table: "RII_SD_GmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_GmailMessages_RII_USERS_UpdatedBy",
                table: "RII_SD_GmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_SD_Invoices_InvoiceId",
                table: "RII_SD_InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_SD_Products_ProductId",
                table: "RII_SD_InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_USERS_CreatedBy",
                table: "RII_SD_InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_USERS_DeletedBy",
                table: "RII_SD_InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_InvoiceLines_RII_USERS_UpdatedBy",
                table: "RII_SD_InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Invoices_RII_SD_Customers_CustomerId",
                table: "RII_SD_Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Invoices_RII_SD_Quotes_QuoteId",
                table: "RII_SD_Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Invoices_RII_USERS_CreatedBy",
                table: "RII_SD_Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Invoices_RII_USERS_DeletedBy",
                table: "RII_SD_Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Invoices_RII_USERS_UpdatedBy",
                table: "RII_SD_Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PotentialCustomers_RII_USERS_CreatedBy",
                table: "RII_SD_PotentialCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PotentialCustomers_RII_USERS_DeletedBy",
                table: "RII_SD_PotentialCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PotentialCustomers_RII_USERS_UpdatedBy",
                table: "RII_SD_PotentialCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_SD_Customers_CustomerId",
                table: "RII_SD_ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_SD_PotentialCustomers_PotentialCustomerId",
                table: "RII_SD_ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_SD_Products_ProductId",
                table: "RII_SD_ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_USERS_CreatedBy",
                table: "RII_SD_ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_USERS_DeletedBy",
                table: "RII_SD_ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_USERS_UpdatedBy",
                table: "RII_SD_ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Products_RII_USERS_CreatedBy",
                table: "RII_SD_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Products_RII_USERS_DeletedBy",
                table: "RII_SD_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Products_RII_USERS_UpdatedBy",
                table: "RII_SD_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_SD_Products_ProductId",
                table: "RII_SD_QuoteLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_SD_Quotes_QuoteId",
                table: "RII_SD_QuoteLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_USERS_CreatedBy",
                table: "RII_SD_QuoteLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_USERS_DeletedBy",
                table: "RII_SD_QuoteLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QuoteLines_RII_USERS_UpdatedBy",
                table: "RII_SD_QuoteLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Quotes_RII_SD_Customers_CustomerId",
                table: "RII_SD_Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Quotes_RII_USERS_CreatedBy",
                table: "RII_SD_Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Quotes_RII_USERS_DeletedBy",
                table: "RII_SD_Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Quotes_RII_USERS_UpdatedBy",
                table: "RII_SD_Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_RecurringPayments_RII_SD_Customers_CustomerId",
                table: "RII_SD_RecurringPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_RecurringPayments_RII_USERS_CreatedBy",
                table: "RII_SD_RecurringPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_RecurringPayments_RII_USERS_DeletedBy",
                table: "RII_SD_RecurringPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_RecurringPayments_RII_USERS_UpdatedBy",
                table: "RII_SD_RecurringPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SoftwareResearches_RII_SD_PotentialCustomers_PotentialCustomerId",
                table: "RII_SD_SoftwareResearches");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SoftwareResearches_RII_USERS_CreatedBy",
                table: "RII_SD_SoftwareResearches");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SoftwareResearches_RII_USERS_DeletedBy",
                table: "RII_SD_SoftwareResearches");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SoftwareResearches_RII_USERS_UpdatedBy",
                table: "RII_SD_SoftwareResearches");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SystemSettings_RII_USERS_CreatedBy",
                table: "RII_SD_SystemSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SystemSettings_RII_USERS_DeletedBy",
                table: "RII_SD_SystemSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SystemSettings_RII_USERS_UpdatedBy",
                table: "RII_SD_SystemSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Tasks_RII_SD_Customers_CustomerId",
                table: "RII_SD_Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Tasks_RII_USERS_CreatedBy",
                table: "RII_SD_Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Tasks_RII_USERS_DeletedBy",
                table: "RII_SD_Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Tasks_RII_USERS_UpdatedBy",
                table: "RII_SD_Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VisitForms_RII_SD_Customers_CustomerId",
                table: "RII_SD_VisitForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VisitForms_RII_SD_Visits_VisitId",
                table: "RII_SD_VisitForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VisitForms_RII_USERS_CreatedBy",
                table: "RII_SD_VisitForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VisitForms_RII_USERS_DeletedBy",
                table: "RII_SD_VisitForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VisitForms_RII_USERS_UpdatedBy",
                table: "RII_SD_VisitForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Visits_RII_SD_Customers_CustomerId",
                table: "RII_SD_Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Visits_RII_USERS_CreatedBy",
                table: "RII_SD_Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Visits_RII_USERS_DeletedBy",
                table: "RII_SD_Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_Visits_RII_USERS_UpdatedBy",
                table: "RII_SD_Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_Visits",
                table: "RII_SD_Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_VisitForms",
                table: "RII_SD_VisitForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_Tasks",
                table: "RII_SD_Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_SystemSettings",
                table: "RII_SD_SystemSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_SoftwareResearches",
                table: "RII_SD_SoftwareResearches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_RecurringPayments",
                table: "RII_SD_RecurringPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_Quotes",
                table: "RII_SD_Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_QuoteLines",
                table: "RII_SD_QuoteLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_Products",
                table: "RII_SD_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_ProductCustomers",
                table: "RII_SD_ProductCustomers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_PotentialCustomers",
                table: "RII_SD_PotentialCustomers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_Invoices",
                table: "RII_SD_Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_InvoiceLines",
                table: "RII_SD_InvoiceLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_GmailMessages",
                table: "RII_SD_GmailMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_FixedAssets",
                table: "RII_SD_FixedAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_ErpNewsItems",
                table: "RII_SD_ErpNewsItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_Customers",
                table: "RII_SD_Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentFieldLabels",
                table: "DocumentFieldLabels");

            migrationBuilder.RenameTable(
                name: "RII_SD_Visits",
                newName: "RII_SD_VISIT");

            migrationBuilder.RenameTable(
                name: "RII_SD_VisitForms",
                newName: "RII_SD_VISIT_FORM");

            migrationBuilder.RenameTable(
                name: "RII_SD_Tasks",
                newName: "RII_SD_TASK");

            migrationBuilder.RenameTable(
                name: "RII_SD_SystemSettings",
                newName: "RII_SD_SYSTEM_SETTING");

            migrationBuilder.RenameTable(
                name: "RII_SD_SoftwareResearches",
                newName: "RII_SD_SOFTWARE_RESEARCH");

            migrationBuilder.RenameTable(
                name: "RII_SD_RecurringPayments",
                newName: "RII_SD_RECURRING_PAYMENT");

            migrationBuilder.RenameTable(
                name: "RII_SD_Quotes",
                newName: "RII_SD_QUOTE");

            migrationBuilder.RenameTable(
                name: "RII_SD_QuoteLines",
                newName: "RII_SD_QUOTE_LINE");

            migrationBuilder.RenameTable(
                name: "RII_SD_Products",
                newName: "RII_SD_PRODUCT");

            migrationBuilder.RenameTable(
                name: "RII_SD_ProductCustomers",
                newName: "RII_SD_PRODUCT_CUSTOMER");

            migrationBuilder.RenameTable(
                name: "RII_SD_PotentialCustomers",
                newName: "RII_SD_POTENTIAL_CUSTOMER");

            migrationBuilder.RenameTable(
                name: "RII_SD_Invoices",
                newName: "RII_SD_INVOICE");

            migrationBuilder.RenameTable(
                name: "RII_SD_InvoiceLines",
                newName: "RII_SD_INVOICE_LINE");

            migrationBuilder.RenameTable(
                name: "RII_SD_GmailMessages",
                newName: "RII_SD_GMAIL_MESSAGE");

            migrationBuilder.RenameTable(
                name: "RII_SD_FixedAssets",
                newName: "RII_SD_FIXED_ASSET");

            migrationBuilder.RenameTable(
                name: "RII_SD_ErpNewsItems",
                newName: "RII_SD_ERP_NEWS_ITEM");

            migrationBuilder.RenameTable(
                name: "RII_SD_Customers",
                newName: "RII_SD_CUSTOMER");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "RII_NOTIFICATION");

            migrationBuilder.RenameTable(
                name: "DocumentFieldLabels",
                newName: "RII_DOCUMENT_FIELD_LABEL");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Visits_VisitDate_Status",
                table: "RII_SD_VISIT",
                newName: "IX_RII_SD_VISIT_VisitDate_Status");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Visits_UpdatedBy",
                table: "RII_SD_VISIT",
                newName: "IX_RII_SD_VISIT_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Visits_DeletedBy",
                table: "RII_SD_VISIT",
                newName: "IX_RII_SD_VISIT_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Visits_CustomerId",
                table: "RII_SD_VISIT",
                newName: "IX_RII_SD_VISIT_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Visits_CreatedBy",
                table: "RII_SD_VISIT",
                newName: "IX_RII_SD_VISIT_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VisitForms_VisitId",
                table: "RII_SD_VISIT_FORM",
                newName: "IX_RII_SD_VISIT_FORM_VisitId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VisitForms_UpdatedBy",
                table: "RII_SD_VISIT_FORM",
                newName: "IX_RII_SD_VISIT_FORM_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VisitForms_DeletedBy",
                table: "RII_SD_VISIT_FORM",
                newName: "IX_RII_SD_VISIT_FORM_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VisitForms_CustomerId",
                table: "RII_SD_VISIT_FORM",
                newName: "IX_RII_SD_VISIT_FORM_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VisitForms_CreatedBy",
                table: "RII_SD_VISIT_FORM",
                newName: "IX_RII_SD_VISIT_FORM_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Tasks_UpdatedBy",
                table: "RII_SD_TASK",
                newName: "IX_RII_SD_TASK_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Tasks_Status_Priority_DueDate",
                table: "RII_SD_TASK",
                newName: "IX_RII_SD_TASK_Status_Priority_DueDate");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Tasks_DeletedBy",
                table: "RII_SD_TASK",
                newName: "IX_RII_SD_TASK_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Tasks_CustomerId",
                table: "RII_SD_TASK",
                newName: "IX_RII_SD_TASK_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Tasks_CreatedBy",
                table: "RII_SD_TASK",
                newName: "IX_RII_SD_TASK_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SystemSettings_UpdatedBy",
                table: "RII_SD_SYSTEM_SETTING",
                newName: "IX_RII_SD_SYSTEM_SETTING_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SystemSettings_DeletedBy",
                table: "RII_SD_SYSTEM_SETTING",
                newName: "IX_RII_SD_SYSTEM_SETTING_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SystemSettings_CreatedBy",
                table: "RII_SD_SYSTEM_SETTING",
                newName: "IX_RII_SD_SYSTEM_SETTING_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SoftwareResearches_UpdatedBy",
                table: "RII_SD_SOFTWARE_RESEARCH",
                newName: "IX_RII_SD_SOFTWARE_RESEARCH_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SoftwareResearches_Provider_Status_Score",
                table: "RII_SD_SOFTWARE_RESEARCH",
                newName: "IX_RII_SD_SOFTWARE_RESEARCH_Provider_Status_Score");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SoftwareResearches_PotentialCustomerId",
                table: "RII_SD_SOFTWARE_RESEARCH",
                newName: "IX_RII_SD_SOFTWARE_RESEARCH_PotentialCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SoftwareResearches_DeletedBy",
                table: "RII_SD_SOFTWARE_RESEARCH",
                newName: "IX_RII_SD_SOFTWARE_RESEARCH_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SoftwareResearches_CreatedBy",
                table: "RII_SD_SOFTWARE_RESEARCH",
                newName: "IX_RII_SD_SOFTWARE_RESEARCH_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RecurringPayments_UpdatedBy",
                table: "RII_SD_RECURRING_PAYMENT",
                newName: "IX_RII_SD_RECURRING_PAYMENT_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RecurringPayments_IsActive_DayOfMonth",
                table: "RII_SD_RECURRING_PAYMENT",
                newName: "IX_RII_SD_RECURRING_PAYMENT_IsActive_DayOfMonth");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RecurringPayments_DeletedBy",
                table: "RII_SD_RECURRING_PAYMENT",
                newName: "IX_RII_SD_RECURRING_PAYMENT_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RecurringPayments_CustomerId",
                table: "RII_SD_RECURRING_PAYMENT",
                newName: "IX_RII_SD_RECURRING_PAYMENT_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RecurringPayments_CreatedBy",
                table: "RII_SD_RECURRING_PAYMENT",
                newName: "IX_RII_SD_RECURRING_PAYMENT_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Quotes_UpdatedBy",
                table: "RII_SD_QUOTE",
                newName: "IX_RII_SD_QUOTE_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Quotes_Status_QuoteDate",
                table: "RII_SD_QUOTE",
                newName: "IX_RII_SD_QUOTE_Status_QuoteDate");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Quotes_QuoteNumber",
                table: "RII_SD_QUOTE",
                newName: "IX_RII_SD_QUOTE_QuoteNumber");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Quotes_DeletedBy",
                table: "RII_SD_QUOTE",
                newName: "IX_RII_SD_QUOTE_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Quotes_CustomerId",
                table: "RII_SD_QUOTE",
                newName: "IX_RII_SD_QUOTE_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Quotes_CreatedBy",
                table: "RII_SD_QUOTE",
                newName: "IX_RII_SD_QUOTE_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QuoteLines_UpdatedBy",
                table: "RII_SD_QUOTE_LINE",
                newName: "IX_RII_SD_QUOTE_LINE_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QuoteLines_QuoteId",
                table: "RII_SD_QUOTE_LINE",
                newName: "IX_RII_SD_QUOTE_LINE_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QuoteLines_ProductId",
                table: "RII_SD_QUOTE_LINE",
                newName: "IX_RII_SD_QUOTE_LINE_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QuoteLines_DeletedBy",
                table: "RII_SD_QUOTE_LINE",
                newName: "IX_RII_SD_QUOTE_LINE_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QuoteLines_CreatedBy",
                table: "RII_SD_QUOTE_LINE",
                newName: "IX_RII_SD_QUOTE_LINE_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Products_UpdatedBy",
                table: "RII_SD_PRODUCT",
                newName: "IX_RII_SD_PRODUCT_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Products_SearchText",
                table: "RII_SD_PRODUCT",
                newName: "IX_RII_SD_PRODUCT_SearchText");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Products_DeletedBy",
                table: "RII_SD_PRODUCT",
                newName: "IX_RII_SD_PRODUCT_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Products_CreatedBy",
                table: "RII_SD_PRODUCT",
                newName: "IX_RII_SD_PRODUCT_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Products_Code",
                table: "RII_SD_PRODUCT",
                newName: "IX_RII_SD_PRODUCT_Code");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ProductCustomers_UpdatedBy",
                table: "RII_SD_PRODUCT_CUSTOMER",
                newName: "IX_RII_SD_PRODUCT_CUSTOMER_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ProductCustomers_ProductId_CustomerId_PotentialCustomerId",
                table: "RII_SD_PRODUCT_CUSTOMER",
                newName: "IX_RII_SD_PRODUCT_CUSTOMER_ProductId_CustomerId_PotentialCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ProductCustomers_PotentialCustomerId",
                table: "RII_SD_PRODUCT_CUSTOMER",
                newName: "IX_RII_SD_PRODUCT_CUSTOMER_PotentialCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ProductCustomers_DeletedBy",
                table: "RII_SD_PRODUCT_CUSTOMER",
                newName: "IX_RII_SD_PRODUCT_CUSTOMER_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ProductCustomers_CustomerId",
                table: "RII_SD_PRODUCT_CUSTOMER",
                newName: "IX_RII_SD_PRODUCT_CUSTOMER_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ProductCustomers_CreatedBy",
                table: "RII_SD_PRODUCT_CUSTOMER",
                newName: "IX_RII_SD_PRODUCT_CUSTOMER_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PotentialCustomers_UpdatedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                newName: "IX_RII_SD_POTENTIAL_CUSTOMER_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PotentialCustomers_DeletedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                newName: "IX_RII_SD_POTENTIAL_CUSTOMER_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PotentialCustomers_CreatedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                newName: "IX_RII_SD_POTENTIAL_CUSTOMER_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PotentialCustomers_CompanyName",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                newName: "IX_RII_SD_POTENTIAL_CUSTOMER_CompanyName");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PotentialCustomers_Code",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                newName: "IX_RII_SD_POTENTIAL_CUSTOMER_Code");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Invoices_UpdatedBy",
                table: "RII_SD_INVOICE",
                newName: "IX_RII_SD_INVOICE_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Invoices_Status_InvoiceDate",
                table: "RII_SD_INVOICE",
                newName: "IX_RII_SD_INVOICE_Status_InvoiceDate");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Invoices_QuoteId",
                table: "RII_SD_INVOICE",
                newName: "IX_RII_SD_INVOICE_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Invoices_InvoiceNumber",
                table: "RII_SD_INVOICE",
                newName: "IX_RII_SD_INVOICE_InvoiceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Invoices_DeletedBy",
                table: "RII_SD_INVOICE",
                newName: "IX_RII_SD_INVOICE_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Invoices_CustomerId",
                table: "RII_SD_INVOICE",
                newName: "IX_RII_SD_INVOICE_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Invoices_CreatedBy",
                table: "RII_SD_INVOICE",
                newName: "IX_RII_SD_INVOICE_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_InvoiceLines_UpdatedBy",
                table: "RII_SD_INVOICE_LINE",
                newName: "IX_RII_SD_INVOICE_LINE_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_InvoiceLines_ProductId",
                table: "RII_SD_INVOICE_LINE",
                newName: "IX_RII_SD_INVOICE_LINE_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_InvoiceLines_InvoiceId",
                table: "RII_SD_INVOICE_LINE",
                newName: "IX_RII_SD_INVOICE_LINE_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_InvoiceLines_DeletedBy",
                table: "RII_SD_INVOICE_LINE",
                newName: "IX_RII_SD_INVOICE_LINE_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_InvoiceLines_CreatedBy",
                table: "RII_SD_INVOICE_LINE",
                newName: "IX_RII_SD_INVOICE_LINE_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GmailMessages_UpdatedBy",
                table: "RII_SD_GMAIL_MESSAGE",
                newName: "IX_RII_SD_GMAIL_MESSAGE_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GmailMessages_IsUnread_IsMeeting_ReceivedAt",
                table: "RII_SD_GMAIL_MESSAGE",
                newName: "IX_RII_SD_GMAIL_MESSAGE_IsUnread_IsMeeting_ReceivedAt");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GmailMessages_GmailMessageId",
                table: "RII_SD_GMAIL_MESSAGE",
                newName: "IX_RII_SD_GMAIL_MESSAGE_GmailMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GmailMessages_DeletedBy",
                table: "RII_SD_GMAIL_MESSAGE",
                newName: "IX_RII_SD_GMAIL_MESSAGE_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GmailMessages_CreatedBy",
                table: "RII_SD_GMAIL_MESSAGE",
                newName: "IX_RII_SD_GMAIL_MESSAGE_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_FixedAssets_UpdatedBy",
                table: "RII_SD_FIXED_ASSET",
                newName: "IX_RII_SD_FIXED_ASSET_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_FixedAssets_DeletedBy",
                table: "RII_SD_FIXED_ASSET",
                newName: "IX_RII_SD_FIXED_ASSET_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_FixedAssets_CreatedBy",
                table: "RII_SD_FIXED_ASSET",
                newName: "IX_RII_SD_FIXED_ASSET_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_FixedAssets_Code",
                table: "RII_SD_FIXED_ASSET",
                newName: "IX_RII_SD_FIXED_ASSET_Code");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ErpNewsItems_UpdatedBy",
                table: "RII_SD_ERP_NEWS_ITEM",
                newName: "IX_RII_SD_ERP_NEWS_ITEM_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ErpNewsItems_Topic_IsCritical_IsRead_PublishedAt",
                table: "RII_SD_ERP_NEWS_ITEM",
                newName: "IX_RII_SD_ERP_NEWS_ITEM_Topic_IsCritical_IsRead_PublishedAt");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ErpNewsItems_DeletedBy",
                table: "RII_SD_ERP_NEWS_ITEM",
                newName: "IX_RII_SD_ERP_NEWS_ITEM_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ErpNewsItems_CreatedBy",
                table: "RII_SD_ERP_NEWS_ITEM",
                newName: "IX_RII_SD_ERP_NEWS_ITEM_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Customers_UpdatedBy",
                table: "RII_SD_CUSTOMER",
                newName: "IX_RII_SD_CUSTOMER_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Customers_Name",
                table: "RII_SD_CUSTOMER",
                newName: "IX_RII_SD_CUSTOMER_Name");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Customers_DeletedBy",
                table: "RII_SD_CUSTOMER",
                newName: "IX_RII_SD_CUSTOMER_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Customers_CreatedBy",
                table: "RII_SD_CUSTOMER",
                newName: "IX_RII_SD_CUSTOMER_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_Customers_Code",
                table: "RII_SD_CUSTOMER",
                newName: "IX_RII_SD_CUSTOMER_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "RII_NOTIFICATION",
                newName: "IX_RII_NOTIFICATION_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UpdatedBy",
                table: "RII_NOTIFICATION",
                newName: "IX_RII_NOTIFICATION_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_IsRead",
                table: "RII_NOTIFICATION",
                newName: "IX_RII_NOTIFICATION_IsRead");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_DeletedBy",
                table: "RII_NOTIFICATION",
                newName: "IX_RII_NOTIFICATION_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_CreatedBy",
                table: "RII_NOTIFICATION",
                newName: "IX_RII_NOTIFICATION_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "UX_DocumentFieldLabels_DocumentType_Scope_FieldKey",
                table: "RII_DOCUMENT_FIELD_LABEL",
                newName: "UX_RII_DOCUMENT_FIELD_LABEL_DOCUMENT_TYPE_SCOPE_FIELD_KEY");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFieldLabels_UpdatedBy",
                table: "RII_DOCUMENT_FIELD_LABEL",
                newName: "IX_RII_DOCUMENT_FIELD_LABEL_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFieldLabels_DocumentType_Scope_SortOrder",
                table: "RII_DOCUMENT_FIELD_LABEL",
                newName: "IX_RII_DOCUMENT_FIELD_LABEL_DOCUMENT_TYPE_SCOPE_SORT_ORDER");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFieldLabels_DeletedBy",
                table: "RII_DOCUMENT_FIELD_LABEL",
                newName: "IX_RII_DOCUMENT_FIELD_LABEL_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFieldLabels_CreatedBy",
                table: "RII_DOCUMENT_FIELD_LABEL",
                newName: "IX_RII_DOCUMENT_FIELD_LABEL_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_VISIT",
                table: "RII_SD_VISIT",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_VISIT_FORM",
                table: "RII_SD_VISIT_FORM",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_TASK",
                table: "RII_SD_TASK",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_SYSTEM_SETTING",
                table: "RII_SD_SYSTEM_SETTING",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_SOFTWARE_RESEARCH",
                table: "RII_SD_SOFTWARE_RESEARCH",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_RECURRING_PAYMENT",
                table: "RII_SD_RECURRING_PAYMENT",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_QUOTE",
                table: "RII_SD_QUOTE",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_QUOTE_LINE",
                table: "RII_SD_QUOTE_LINE",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_PRODUCT",
                table: "RII_SD_PRODUCT",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_PRODUCT_CUSTOMER",
                table: "RII_SD_PRODUCT_CUSTOMER",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_POTENTIAL_CUSTOMER",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_INVOICE",
                table: "RII_SD_INVOICE",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_INVOICE_LINE",
                table: "RII_SD_INVOICE_LINE",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_GMAIL_MESSAGE",
                table: "RII_SD_GMAIL_MESSAGE",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_FIXED_ASSET",
                table: "RII_SD_FIXED_ASSET",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_ERP_NEWS_ITEM",
                table: "RII_SD_ERP_NEWS_ITEM",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_CUSTOMER",
                table: "RII_SD_CUSTOMER",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_NOTIFICATION",
                table: "RII_NOTIFICATION",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_DOCUMENT_FIELD_LABEL",
                table: "RII_DOCUMENT_FIELD_LABEL",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_DOCUMENT_FIELD_LABEL_RII_USERS_CreatedBy",
                table: "RII_DOCUMENT_FIELD_LABEL",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_DOCUMENT_FIELD_LABEL_RII_USERS_DeletedBy",
                table: "RII_DOCUMENT_FIELD_LABEL",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_DOCUMENT_FIELD_LABEL_RII_USERS_UpdatedBy",
                table: "RII_DOCUMENT_FIELD_LABEL",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_NOTIFICATION_RII_USERS_CreatedBy",
                table: "RII_NOTIFICATION",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_NOTIFICATION_RII_USERS_DeletedBy",
                table: "RII_NOTIFICATION",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_NOTIFICATION_RII_USERS_UpdatedBy",
                table: "RII_NOTIFICATION",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_NOTIFICATION_RII_USERS_UserId",
                table: "RII_NOTIFICATION",
                column: "UserId",
                principalTable: "RII_USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_CUSTOMER_RII_USERS_CreatedBy",
                table: "RII_SD_CUSTOMER",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_CUSTOMER_RII_USERS_DeletedBy",
                table: "RII_SD_CUSTOMER",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_CUSTOMER_RII_USERS_UpdatedBy",
                table: "RII_SD_CUSTOMER",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ERP_NEWS_ITEM_RII_USERS_CreatedBy",
                table: "RII_SD_ERP_NEWS_ITEM",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ERP_NEWS_ITEM_RII_USERS_DeletedBy",
                table: "RII_SD_ERP_NEWS_ITEM",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ERP_NEWS_ITEM_RII_USERS_UpdatedBy",
                table: "RII_SD_ERP_NEWS_ITEM",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_FIXED_ASSET_RII_USERS_CreatedBy",
                table: "RII_SD_FIXED_ASSET",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_FIXED_ASSET_RII_USERS_DeletedBy",
                table: "RII_SD_FIXED_ASSET",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_FIXED_ASSET_RII_USERS_UpdatedBy",
                table: "RII_SD_FIXED_ASSET",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_GMAIL_MESSAGE_RII_USERS_CreatedBy",
                table: "RII_SD_GMAIL_MESSAGE",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_GMAIL_MESSAGE_RII_USERS_DeletedBy",
                table: "RII_SD_GMAIL_MESSAGE",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_GMAIL_MESSAGE_RII_USERS_UpdatedBy",
                table: "RII_SD_GMAIL_MESSAGE",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_INVOICE",
                column: "CustomerId",
                principalTable: "RII_SD_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_RII_SD_QUOTE_QuoteId",
                table: "RII_SD_INVOICE",
                column: "QuoteId",
                principalTable: "RII_SD_QUOTE",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_RII_USERS_CreatedBy",
                table: "RII_SD_INVOICE",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_RII_USERS_DeletedBy",
                table: "RII_SD_INVOICE",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_RII_USERS_UpdatedBy",
                table: "RII_SD_INVOICE",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_SD_INVOICE_InvoiceId",
                table: "RII_SD_INVOICE_LINE",
                column: "InvoiceId",
                principalTable: "RII_SD_INVOICE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_SD_PRODUCT_ProductId",
                table: "RII_SD_INVOICE_LINE",
                column: "ProductId",
                principalTable: "RII_SD_PRODUCT",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_USERS_CreatedBy",
                table: "RII_SD_INVOICE_LINE",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_USERS_DeletedBy",
                table: "RII_SD_INVOICE_LINE",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_USERS_UpdatedBy",
                table: "RII_SD_INVOICE_LINE",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_POTENTIAL_CUSTOMER_RII_USERS_CreatedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_POTENTIAL_CUSTOMER_RII_USERS_DeletedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_POTENTIAL_CUSTOMER_RII_USERS_UpdatedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_RII_USERS_CreatedBy",
                table: "RII_SD_PRODUCT",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_RII_USERS_DeletedBy",
                table: "RII_SD_PRODUCT",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_RII_USERS_UpdatedBy",
                table: "RII_SD_PRODUCT",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_PRODUCT_CUSTOMER",
                column: "CustomerId",
                principalTable: "RII_SD_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_SD_POTENTIAL_CUSTOMER_PotentialCustomerId",
                table: "RII_SD_PRODUCT_CUSTOMER",
                column: "PotentialCustomerId",
                principalTable: "RII_SD_POTENTIAL_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_SD_PRODUCT_ProductId",
                table: "RII_SD_PRODUCT_CUSTOMER",
                column: "ProductId",
                principalTable: "RII_SD_PRODUCT",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_USERS_CreatedBy",
                table: "RII_SD_PRODUCT_CUSTOMER",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_USERS_DeletedBy",
                table: "RII_SD_PRODUCT_CUSTOMER",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_USERS_UpdatedBy",
                table: "RII_SD_PRODUCT_CUSTOMER",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_QUOTE",
                column: "CustomerId",
                principalTable: "RII_SD_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_RII_USERS_CreatedBy",
                table: "RII_SD_QUOTE",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_RII_USERS_DeletedBy",
                table: "RII_SD_QUOTE",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_RII_USERS_UpdatedBy",
                table: "RII_SD_QUOTE",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_SD_PRODUCT_ProductId",
                table: "RII_SD_QUOTE_LINE",
                column: "ProductId",
                principalTable: "RII_SD_PRODUCT",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_SD_QUOTE_QuoteId",
                table: "RII_SD_QUOTE_LINE",
                column: "QuoteId",
                principalTable: "RII_SD_QUOTE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_USERS_CreatedBy",
                table: "RII_SD_QUOTE_LINE",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_USERS_DeletedBy",
                table: "RII_SD_QUOTE_LINE",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_USERS_UpdatedBy",
                table: "RII_SD_QUOTE_LINE",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_RECURRING_PAYMENT_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_RECURRING_PAYMENT",
                column: "CustomerId",
                principalTable: "RII_SD_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_RECURRING_PAYMENT_RII_USERS_CreatedBy",
                table: "RII_SD_RECURRING_PAYMENT",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_RECURRING_PAYMENT_RII_USERS_DeletedBy",
                table: "RII_SD_RECURRING_PAYMENT",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_RECURRING_PAYMENT_RII_USERS_UpdatedBy",
                table: "RII_SD_RECURRING_PAYMENT",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SOFTWARE_RESEARCH_RII_SD_POTENTIAL_CUSTOMER_PotentialCustomerId",
                table: "RII_SD_SOFTWARE_RESEARCH",
                column: "PotentialCustomerId",
                principalTable: "RII_SD_POTENTIAL_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SOFTWARE_RESEARCH_RII_USERS_CreatedBy",
                table: "RII_SD_SOFTWARE_RESEARCH",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SOFTWARE_RESEARCH_RII_USERS_DeletedBy",
                table: "RII_SD_SOFTWARE_RESEARCH",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SOFTWARE_RESEARCH_RII_USERS_UpdatedBy",
                table: "RII_SD_SOFTWARE_RESEARCH",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SYSTEM_SETTING_RII_USERS_CreatedBy",
                table: "RII_SD_SYSTEM_SETTING",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SYSTEM_SETTING_RII_USERS_DeletedBy",
                table: "RII_SD_SYSTEM_SETTING",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_SYSTEM_SETTING_RII_USERS_UpdatedBy",
                table: "RII_SD_SYSTEM_SETTING",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_TASK_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_TASK",
                column: "CustomerId",
                principalTable: "RII_SD_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_TASK_RII_USERS_CreatedBy",
                table: "RII_SD_TASK",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_TASK_RII_USERS_DeletedBy",
                table: "RII_SD_TASK",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_TASK_RII_USERS_UpdatedBy",
                table: "RII_SD_TASK",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_VISIT",
                column: "CustomerId",
                principalTable: "RII_SD_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_RII_USERS_CreatedBy",
                table: "RII_SD_VISIT",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_RII_USERS_DeletedBy",
                table: "RII_SD_VISIT",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_RII_USERS_UpdatedBy",
                table: "RII_SD_VISIT",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_VISIT_FORM",
                column: "CustomerId",
                principalTable: "RII_SD_CUSTOMER",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_SD_VISIT_VisitId",
                table: "RII_SD_VISIT_FORM",
                column: "VisitId",
                principalTable: "RII_SD_VISIT",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_USERS_CreatedBy",
                table: "RII_SD_VISIT_FORM",
                column: "CreatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_USERS_DeletedBy",
                table: "RII_SD_VISIT_FORM",
                column: "DeletedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_USERS_UpdatedBy",
                table: "RII_SD_VISIT_FORM",
                column: "UpdatedBy",
                principalTable: "RII_USERS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RII_DOCUMENT_FIELD_LABEL_RII_USERS_CreatedBy",
                table: "RII_DOCUMENT_FIELD_LABEL");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_DOCUMENT_FIELD_LABEL_RII_USERS_DeletedBy",
                table: "RII_DOCUMENT_FIELD_LABEL");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_DOCUMENT_FIELD_LABEL_RII_USERS_UpdatedBy",
                table: "RII_DOCUMENT_FIELD_LABEL");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_NOTIFICATION_RII_USERS_CreatedBy",
                table: "RII_NOTIFICATION");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_NOTIFICATION_RII_USERS_DeletedBy",
                table: "RII_NOTIFICATION");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_NOTIFICATION_RII_USERS_UpdatedBy",
                table: "RII_NOTIFICATION");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_NOTIFICATION_RII_USERS_UserId",
                table: "RII_NOTIFICATION");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_CUSTOMER_RII_USERS_CreatedBy",
                table: "RII_SD_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_CUSTOMER_RII_USERS_DeletedBy",
                table: "RII_SD_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_CUSTOMER_RII_USERS_UpdatedBy",
                table: "RII_SD_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ERP_NEWS_ITEM_RII_USERS_CreatedBy",
                table: "RII_SD_ERP_NEWS_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ERP_NEWS_ITEM_RII_USERS_DeletedBy",
                table: "RII_SD_ERP_NEWS_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_ERP_NEWS_ITEM_RII_USERS_UpdatedBy",
                table: "RII_SD_ERP_NEWS_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_FIXED_ASSET_RII_USERS_CreatedBy",
                table: "RII_SD_FIXED_ASSET");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_FIXED_ASSET_RII_USERS_DeletedBy",
                table: "RII_SD_FIXED_ASSET");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_FIXED_ASSET_RII_USERS_UpdatedBy",
                table: "RII_SD_FIXED_ASSET");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_GMAIL_MESSAGE_RII_USERS_CreatedBy",
                table: "RII_SD_GMAIL_MESSAGE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_GMAIL_MESSAGE_RII_USERS_DeletedBy",
                table: "RII_SD_GMAIL_MESSAGE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_GMAIL_MESSAGE_RII_USERS_UpdatedBy",
                table: "RII_SD_GMAIL_MESSAGE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_INVOICE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_RII_SD_QUOTE_QuoteId",
                table: "RII_SD_INVOICE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_RII_USERS_CreatedBy",
                table: "RII_SD_INVOICE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_RII_USERS_DeletedBy",
                table: "RII_SD_INVOICE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_RII_USERS_UpdatedBy",
                table: "RII_SD_INVOICE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_SD_INVOICE_InvoiceId",
                table: "RII_SD_INVOICE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_SD_PRODUCT_ProductId",
                table: "RII_SD_INVOICE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_USERS_CreatedBy",
                table: "RII_SD_INVOICE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_USERS_DeletedBy",
                table: "RII_SD_INVOICE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_INVOICE_LINE_RII_USERS_UpdatedBy",
                table: "RII_SD_INVOICE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_POTENTIAL_CUSTOMER_RII_USERS_CreatedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_POTENTIAL_CUSTOMER_RII_USERS_DeletedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_POTENTIAL_CUSTOMER_RII_USERS_UpdatedBy",
                table: "RII_SD_POTENTIAL_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_RII_USERS_CreatedBy",
                table: "RII_SD_PRODUCT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_RII_USERS_DeletedBy",
                table: "RII_SD_PRODUCT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_RII_USERS_UpdatedBy",
                table: "RII_SD_PRODUCT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_PRODUCT_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_SD_POTENTIAL_CUSTOMER_PotentialCustomerId",
                table: "RII_SD_PRODUCT_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_SD_PRODUCT_ProductId",
                table: "RII_SD_PRODUCT_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_USERS_CreatedBy",
                table: "RII_SD_PRODUCT_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_USERS_DeletedBy",
                table: "RII_SD_PRODUCT_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_PRODUCT_CUSTOMER_RII_USERS_UpdatedBy",
                table: "RII_SD_PRODUCT_CUSTOMER");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_QUOTE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_RII_USERS_CreatedBy",
                table: "RII_SD_QUOTE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_RII_USERS_DeletedBy",
                table: "RII_SD_QUOTE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_RII_USERS_UpdatedBy",
                table: "RII_SD_QUOTE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_SD_PRODUCT_ProductId",
                table: "RII_SD_QUOTE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_SD_QUOTE_QuoteId",
                table: "RII_SD_QUOTE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_USERS_CreatedBy",
                table: "RII_SD_QUOTE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_USERS_DeletedBy",
                table: "RII_SD_QUOTE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_QUOTE_LINE_RII_USERS_UpdatedBy",
                table: "RII_SD_QUOTE_LINE");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_RECURRING_PAYMENT_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_RECURRING_PAYMENT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_RECURRING_PAYMENT_RII_USERS_CreatedBy",
                table: "RII_SD_RECURRING_PAYMENT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_RECURRING_PAYMENT_RII_USERS_DeletedBy",
                table: "RII_SD_RECURRING_PAYMENT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_RECURRING_PAYMENT_RII_USERS_UpdatedBy",
                table: "RII_SD_RECURRING_PAYMENT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SOFTWARE_RESEARCH_RII_SD_POTENTIAL_CUSTOMER_PotentialCustomerId",
                table: "RII_SD_SOFTWARE_RESEARCH");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SOFTWARE_RESEARCH_RII_USERS_CreatedBy",
                table: "RII_SD_SOFTWARE_RESEARCH");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SOFTWARE_RESEARCH_RII_USERS_DeletedBy",
                table: "RII_SD_SOFTWARE_RESEARCH");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SOFTWARE_RESEARCH_RII_USERS_UpdatedBy",
                table: "RII_SD_SOFTWARE_RESEARCH");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SYSTEM_SETTING_RII_USERS_CreatedBy",
                table: "RII_SD_SYSTEM_SETTING");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SYSTEM_SETTING_RII_USERS_DeletedBy",
                table: "RII_SD_SYSTEM_SETTING");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_SYSTEM_SETTING_RII_USERS_UpdatedBy",
                table: "RII_SD_SYSTEM_SETTING");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_TASK_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_TASK");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_TASK_RII_USERS_CreatedBy",
                table: "RII_SD_TASK");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_TASK_RII_USERS_DeletedBy",
                table: "RII_SD_TASK");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_TASK_RII_USERS_UpdatedBy",
                table: "RII_SD_TASK");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_VISIT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_RII_USERS_CreatedBy",
                table: "RII_SD_VISIT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_RII_USERS_DeletedBy",
                table: "RII_SD_VISIT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_RII_USERS_UpdatedBy",
                table: "RII_SD_VISIT");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_SD_CUSTOMER_CustomerId",
                table: "RII_SD_VISIT_FORM");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_SD_VISIT_VisitId",
                table: "RII_SD_VISIT_FORM");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_USERS_CreatedBy",
                table: "RII_SD_VISIT_FORM");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_USERS_DeletedBy",
                table: "RII_SD_VISIT_FORM");

            migrationBuilder.DropForeignKey(
                name: "FK_RII_SD_VISIT_FORM_RII_USERS_UpdatedBy",
                table: "RII_SD_VISIT_FORM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_VISIT_FORM",
                table: "RII_SD_VISIT_FORM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_VISIT",
                table: "RII_SD_VISIT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_TASK",
                table: "RII_SD_TASK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_SYSTEM_SETTING",
                table: "RII_SD_SYSTEM_SETTING");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_SOFTWARE_RESEARCH",
                table: "RII_SD_SOFTWARE_RESEARCH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_RECURRING_PAYMENT",
                table: "RII_SD_RECURRING_PAYMENT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_QUOTE_LINE",
                table: "RII_SD_QUOTE_LINE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_QUOTE",
                table: "RII_SD_QUOTE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_PRODUCT_CUSTOMER",
                table: "RII_SD_PRODUCT_CUSTOMER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_PRODUCT",
                table: "RII_SD_PRODUCT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_POTENTIAL_CUSTOMER",
                table: "RII_SD_POTENTIAL_CUSTOMER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_INVOICE_LINE",
                table: "RII_SD_INVOICE_LINE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_INVOICE",
                table: "RII_SD_INVOICE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_GMAIL_MESSAGE",
                table: "RII_SD_GMAIL_MESSAGE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_FIXED_ASSET",
                table: "RII_SD_FIXED_ASSET");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_ERP_NEWS_ITEM",
                table: "RII_SD_ERP_NEWS_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_SD_CUSTOMER",
                table: "RII_SD_CUSTOMER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_NOTIFICATION",
                table: "RII_NOTIFICATION");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RII_DOCUMENT_FIELD_LABEL",
                table: "RII_DOCUMENT_FIELD_LABEL");

            migrationBuilder.RenameTable(
                name: "RII_SD_VISIT_FORM",
                newName: "RII_SD_VisitForms");

            migrationBuilder.RenameTable(
                name: "RII_SD_VISIT",
                newName: "RII_SD_Visits");

            migrationBuilder.RenameTable(
                name: "RII_SD_TASK",
                newName: "RII_SD_Tasks");

            migrationBuilder.RenameTable(
                name: "RII_SD_SYSTEM_SETTING",
                newName: "RII_SD_SystemSettings");

            migrationBuilder.RenameTable(
                name: "RII_SD_SOFTWARE_RESEARCH",
                newName: "RII_SD_SoftwareResearches");

            migrationBuilder.RenameTable(
                name: "RII_SD_RECURRING_PAYMENT",
                newName: "RII_SD_RecurringPayments");

            migrationBuilder.RenameTable(
                name: "RII_SD_QUOTE_LINE",
                newName: "RII_SD_QuoteLines");

            migrationBuilder.RenameTable(
                name: "RII_SD_QUOTE",
                newName: "RII_SD_Quotes");

            migrationBuilder.RenameTable(
                name: "RII_SD_PRODUCT_CUSTOMER",
                newName: "RII_SD_ProductCustomers");

            migrationBuilder.RenameTable(
                name: "RII_SD_PRODUCT",
                newName: "RII_SD_Products");

            migrationBuilder.RenameTable(
                name: "RII_SD_POTENTIAL_CUSTOMER",
                newName: "RII_SD_PotentialCustomers");

            migrationBuilder.RenameTable(
                name: "RII_SD_INVOICE_LINE",
                newName: "RII_SD_InvoiceLines");

            migrationBuilder.RenameTable(
                name: "RII_SD_INVOICE",
                newName: "RII_SD_Invoices");

            migrationBuilder.RenameTable(
                name: "RII_SD_GMAIL_MESSAGE",
                newName: "RII_SD_GmailMessages");

            migrationBuilder.RenameTable(
                name: "RII_SD_FIXED_ASSET",
                newName: "RII_SD_FixedAssets");

            migrationBuilder.RenameTable(
                name: "RII_SD_ERP_NEWS_ITEM",
                newName: "RII_SD_ErpNewsItems");

            migrationBuilder.RenameTable(
                name: "RII_SD_CUSTOMER",
                newName: "RII_SD_Customers");

            migrationBuilder.RenameTable(
                name: "RII_NOTIFICATION",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "RII_DOCUMENT_FIELD_LABEL",
                newName: "DocumentFieldLabels");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_FORM_VisitId",
                table: "RII_SD_VisitForms",
                newName: "IX_RII_SD_VisitForms_VisitId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_FORM_UpdatedBy",
                table: "RII_SD_VisitForms",
                newName: "IX_RII_SD_VisitForms_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_FORM_DeletedBy",
                table: "RII_SD_VisitForms",
                newName: "IX_RII_SD_VisitForms_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_FORM_CustomerId",
                table: "RII_SD_VisitForms",
                newName: "IX_RII_SD_VisitForms_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_FORM_CreatedBy",
                table: "RII_SD_VisitForms",
                newName: "IX_RII_SD_VisitForms_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_VisitDate_Status",
                table: "RII_SD_Visits",
                newName: "IX_RII_SD_Visits_VisitDate_Status");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_UpdatedBy",
                table: "RII_SD_Visits",
                newName: "IX_RII_SD_Visits_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_DeletedBy",
                table: "RII_SD_Visits",
                newName: "IX_RII_SD_Visits_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_CustomerId",
                table: "RII_SD_Visits",
                newName: "IX_RII_SD_Visits_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_VISIT_CreatedBy",
                table: "RII_SD_Visits",
                newName: "IX_RII_SD_Visits_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_TASK_UpdatedBy",
                table: "RII_SD_Tasks",
                newName: "IX_RII_SD_Tasks_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_TASK_Status_Priority_DueDate",
                table: "RII_SD_Tasks",
                newName: "IX_RII_SD_Tasks_Status_Priority_DueDate");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_TASK_DeletedBy",
                table: "RII_SD_Tasks",
                newName: "IX_RII_SD_Tasks_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_TASK_CustomerId",
                table: "RII_SD_Tasks",
                newName: "IX_RII_SD_Tasks_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_TASK_CreatedBy",
                table: "RII_SD_Tasks",
                newName: "IX_RII_SD_Tasks_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SYSTEM_SETTING_UpdatedBy",
                table: "RII_SD_SystemSettings",
                newName: "IX_RII_SD_SystemSettings_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SYSTEM_SETTING_DeletedBy",
                table: "RII_SD_SystemSettings",
                newName: "IX_RII_SD_SystemSettings_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SYSTEM_SETTING_CreatedBy",
                table: "RII_SD_SystemSettings",
                newName: "IX_RII_SD_SystemSettings_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SOFTWARE_RESEARCH_UpdatedBy",
                table: "RII_SD_SoftwareResearches",
                newName: "IX_RII_SD_SoftwareResearches_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SOFTWARE_RESEARCH_Provider_Status_Score",
                table: "RII_SD_SoftwareResearches",
                newName: "IX_RII_SD_SoftwareResearches_Provider_Status_Score");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SOFTWARE_RESEARCH_PotentialCustomerId",
                table: "RII_SD_SoftwareResearches",
                newName: "IX_RII_SD_SoftwareResearches_PotentialCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SOFTWARE_RESEARCH_DeletedBy",
                table: "RII_SD_SoftwareResearches",
                newName: "IX_RII_SD_SoftwareResearches_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_SOFTWARE_RESEARCH_CreatedBy",
                table: "RII_SD_SoftwareResearches",
                newName: "IX_RII_SD_SoftwareResearches_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RECURRING_PAYMENT_UpdatedBy",
                table: "RII_SD_RecurringPayments",
                newName: "IX_RII_SD_RecurringPayments_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RECURRING_PAYMENT_IsActive_DayOfMonth",
                table: "RII_SD_RecurringPayments",
                newName: "IX_RII_SD_RecurringPayments_IsActive_DayOfMonth");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RECURRING_PAYMENT_DeletedBy",
                table: "RII_SD_RecurringPayments",
                newName: "IX_RII_SD_RecurringPayments_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RECURRING_PAYMENT_CustomerId",
                table: "RII_SD_RecurringPayments",
                newName: "IX_RII_SD_RecurringPayments_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_RECURRING_PAYMENT_CreatedBy",
                table: "RII_SD_RecurringPayments",
                newName: "IX_RII_SD_RecurringPayments_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_LINE_UpdatedBy",
                table: "RII_SD_QuoteLines",
                newName: "IX_RII_SD_QuoteLines_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_LINE_QuoteId",
                table: "RII_SD_QuoteLines",
                newName: "IX_RII_SD_QuoteLines_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_LINE_ProductId",
                table: "RII_SD_QuoteLines",
                newName: "IX_RII_SD_QuoteLines_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_LINE_DeletedBy",
                table: "RII_SD_QuoteLines",
                newName: "IX_RII_SD_QuoteLines_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_LINE_CreatedBy",
                table: "RII_SD_QuoteLines",
                newName: "IX_RII_SD_QuoteLines_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_UpdatedBy",
                table: "RII_SD_Quotes",
                newName: "IX_RII_SD_Quotes_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_Status_QuoteDate",
                table: "RII_SD_Quotes",
                newName: "IX_RII_SD_Quotes_Status_QuoteDate");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_QuoteNumber",
                table: "RII_SD_Quotes",
                newName: "IX_RII_SD_Quotes_QuoteNumber");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_DeletedBy",
                table: "RII_SD_Quotes",
                newName: "IX_RII_SD_Quotes_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_CustomerId",
                table: "RII_SD_Quotes",
                newName: "IX_RII_SD_Quotes_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_QUOTE_CreatedBy",
                table: "RII_SD_Quotes",
                newName: "IX_RII_SD_Quotes_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_CUSTOMER_UpdatedBy",
                table: "RII_SD_ProductCustomers",
                newName: "IX_RII_SD_ProductCustomers_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_CUSTOMER_ProductId_CustomerId_PotentialCustomerId",
                table: "RII_SD_ProductCustomers",
                newName: "IX_RII_SD_ProductCustomers_ProductId_CustomerId_PotentialCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_CUSTOMER_PotentialCustomerId",
                table: "RII_SD_ProductCustomers",
                newName: "IX_RII_SD_ProductCustomers_PotentialCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_CUSTOMER_DeletedBy",
                table: "RII_SD_ProductCustomers",
                newName: "IX_RII_SD_ProductCustomers_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_CUSTOMER_CustomerId",
                table: "RII_SD_ProductCustomers",
                newName: "IX_RII_SD_ProductCustomers_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_CUSTOMER_CreatedBy",
                table: "RII_SD_ProductCustomers",
                newName: "IX_RII_SD_ProductCustomers_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_UpdatedBy",
                table: "RII_SD_Products",
                newName: "IX_RII_SD_Products_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_SearchText",
                table: "RII_SD_Products",
                newName: "IX_RII_SD_Products_SearchText");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_DeletedBy",
                table: "RII_SD_Products",
                newName: "IX_RII_SD_Products_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_CreatedBy",
                table: "RII_SD_Products",
                newName: "IX_RII_SD_Products_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_PRODUCT_Code",
                table: "RII_SD_Products",
                newName: "IX_RII_SD_Products_Code");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_POTENTIAL_CUSTOMER_UpdatedBy",
                table: "RII_SD_PotentialCustomers",
                newName: "IX_RII_SD_PotentialCustomers_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_POTENTIAL_CUSTOMER_DeletedBy",
                table: "RII_SD_PotentialCustomers",
                newName: "IX_RII_SD_PotentialCustomers_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_POTENTIAL_CUSTOMER_CreatedBy",
                table: "RII_SD_PotentialCustomers",
                newName: "IX_RII_SD_PotentialCustomers_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_POTENTIAL_CUSTOMER_CompanyName",
                table: "RII_SD_PotentialCustomers",
                newName: "IX_RII_SD_PotentialCustomers_CompanyName");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_POTENTIAL_CUSTOMER_Code",
                table: "RII_SD_PotentialCustomers",
                newName: "IX_RII_SD_PotentialCustomers_Code");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_LINE_UpdatedBy",
                table: "RII_SD_InvoiceLines",
                newName: "IX_RII_SD_InvoiceLines_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_LINE_ProductId",
                table: "RII_SD_InvoiceLines",
                newName: "IX_RII_SD_InvoiceLines_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_LINE_InvoiceId",
                table: "RII_SD_InvoiceLines",
                newName: "IX_RII_SD_InvoiceLines_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_LINE_DeletedBy",
                table: "RII_SD_InvoiceLines",
                newName: "IX_RII_SD_InvoiceLines_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_LINE_CreatedBy",
                table: "RII_SD_InvoiceLines",
                newName: "IX_RII_SD_InvoiceLines_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_UpdatedBy",
                table: "RII_SD_Invoices",
                newName: "IX_RII_SD_Invoices_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_Status_InvoiceDate",
                table: "RII_SD_Invoices",
                newName: "IX_RII_SD_Invoices_Status_InvoiceDate");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_QuoteId",
                table: "RII_SD_Invoices",
                newName: "IX_RII_SD_Invoices_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_InvoiceNumber",
                table: "RII_SD_Invoices",
                newName: "IX_RII_SD_Invoices_InvoiceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_DeletedBy",
                table: "RII_SD_Invoices",
                newName: "IX_RII_SD_Invoices_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_CustomerId",
                table: "RII_SD_Invoices",
                newName: "IX_RII_SD_Invoices_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_INVOICE_CreatedBy",
                table: "RII_SD_Invoices",
                newName: "IX_RII_SD_Invoices_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GMAIL_MESSAGE_UpdatedBy",
                table: "RII_SD_GmailMessages",
                newName: "IX_RII_SD_GmailMessages_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GMAIL_MESSAGE_IsUnread_IsMeeting_ReceivedAt",
                table: "RII_SD_GmailMessages",
                newName: "IX_RII_SD_GmailMessages_IsUnread_IsMeeting_ReceivedAt");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GMAIL_MESSAGE_GmailMessageId",
                table: "RII_SD_GmailMessages",
                newName: "IX_RII_SD_GmailMessages_GmailMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GMAIL_MESSAGE_DeletedBy",
                table: "RII_SD_GmailMessages",
                newName: "IX_RII_SD_GmailMessages_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_GMAIL_MESSAGE_CreatedBy",
                table: "RII_SD_GmailMessages",
                newName: "IX_RII_SD_GmailMessages_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_FIXED_ASSET_UpdatedBy",
                table: "RII_SD_FixedAssets",
                newName: "IX_RII_SD_FixedAssets_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_FIXED_ASSET_DeletedBy",
                table: "RII_SD_FixedAssets",
                newName: "IX_RII_SD_FixedAssets_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_FIXED_ASSET_CreatedBy",
                table: "RII_SD_FixedAssets",
                newName: "IX_RII_SD_FixedAssets_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_FIXED_ASSET_Code",
                table: "RII_SD_FixedAssets",
                newName: "IX_RII_SD_FixedAssets_Code");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ERP_NEWS_ITEM_UpdatedBy",
                table: "RII_SD_ErpNewsItems",
                newName: "IX_RII_SD_ErpNewsItems_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ERP_NEWS_ITEM_Topic_IsCritical_IsRead_PublishedAt",
                table: "RII_SD_ErpNewsItems",
                newName: "IX_RII_SD_ErpNewsItems_Topic_IsCritical_IsRead_PublishedAt");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ERP_NEWS_ITEM_DeletedBy",
                table: "RII_SD_ErpNewsItems",
                newName: "IX_RII_SD_ErpNewsItems_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_ERP_NEWS_ITEM_CreatedBy",
                table: "RII_SD_ErpNewsItems",
                newName: "IX_RII_SD_ErpNewsItems_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_CUSTOMER_UpdatedBy",
                table: "RII_SD_Customers",
                newName: "IX_RII_SD_Customers_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_CUSTOMER_Name",
                table: "RII_SD_Customers",
                newName: "IX_RII_SD_Customers_Name");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_CUSTOMER_DeletedBy",
                table: "RII_SD_Customers",
                newName: "IX_RII_SD_Customers_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_CUSTOMER_CreatedBy",
                table: "RII_SD_Customers",
                newName: "IX_RII_SD_Customers_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_SD_CUSTOMER_Code",
                table: "RII_SD_Customers",
                newName: "IX_RII_SD_Customers_Code");

            migrationBuilder.RenameIndex(
                name: "IX_RII_NOTIFICATION_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RII_NOTIFICATION_UpdatedBy",
                table: "Notifications",
                newName: "IX_Notifications_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_NOTIFICATION_IsRead",
                table: "Notifications",
                newName: "IX_Notifications_IsRead");

            migrationBuilder.RenameIndex(
                name: "IX_RII_NOTIFICATION_DeletedBy",
                table: "Notifications",
                newName: "IX_Notifications_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_NOTIFICATION_CreatedBy",
                table: "Notifications",
                newName: "IX_Notifications_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "UX_RII_DOCUMENT_FIELD_LABEL_DOCUMENT_TYPE_SCOPE_FIELD_KEY",
                table: "DocumentFieldLabels",
                newName: "UX_DocumentFieldLabels_DocumentType_Scope_FieldKey");

            migrationBuilder.RenameIndex(
                name: "IX_RII_DOCUMENT_FIELD_LABEL_UpdatedBy",
                table: "DocumentFieldLabels",
                newName: "IX_DocumentFieldLabels_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_DOCUMENT_FIELD_LABEL_DOCUMENT_TYPE_SCOPE_SORT_ORDER",
                table: "DocumentFieldLabels",
                newName: "IX_DocumentFieldLabels_DocumentType_Scope_SortOrder");

            migrationBuilder.RenameIndex(
                name: "IX_RII_DOCUMENT_FIELD_LABEL_DeletedBy",
                table: "DocumentFieldLabels",
                newName: "IX_DocumentFieldLabels_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_RII_DOCUMENT_FIELD_LABEL_CreatedBy",
                table: "DocumentFieldLabels",
                newName: "IX_DocumentFieldLabels_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_VisitForms",
                table: "RII_SD_VisitForms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_Visits",
                table: "RII_SD_Visits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_Tasks",
                table: "RII_SD_Tasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_SystemSettings",
                table: "RII_SD_SystemSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_SoftwareResearches",
                table: "RII_SD_SoftwareResearches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_RecurringPayments",
                table: "RII_SD_RecurringPayments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_QuoteLines",
                table: "RII_SD_QuoteLines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_Quotes",
                table: "RII_SD_Quotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_ProductCustomers",
                table: "RII_SD_ProductCustomers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_Products",
                table: "RII_SD_Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_PotentialCustomers",
                table: "RII_SD_PotentialCustomers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_InvoiceLines",
                table: "RII_SD_InvoiceLines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_Invoices",
                table: "RII_SD_Invoices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_GmailMessages",
                table: "RII_SD_GmailMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_FixedAssets",
                table: "RII_SD_FixedAssets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_ErpNewsItems",
                table: "RII_SD_ErpNewsItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RII_SD_Customers",
                table: "RII_SD_Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentFieldLabels",
                table: "DocumentFieldLabels",
                column: "Id");

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
                name: "FK_RII_SD_Invoices_RII_SD_Customers_CustomerId",
                table: "RII_SD_Invoices",
                column: "CustomerId",
                principalTable: "RII_SD_Customers",
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
                name: "FK_RII_SD_ProductCustomers_RII_SD_Customers_CustomerId",
                table: "RII_SD_ProductCustomers",
                column: "CustomerId",
                principalTable: "RII_SD_Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RII_SD_ProductCustomers_RII_SD_PotentialCustomers_PotentialCustomerId",
                table: "RII_SD_ProductCustomers",
                column: "PotentialCustomerId",
                principalTable: "RII_SD_PotentialCustomers",
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
                name: "FK_RII_SD_QuoteLines_RII_SD_Products_ProductId",
                table: "RII_SD_QuoteLines",
                column: "ProductId",
                principalTable: "RII_SD_Products",
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
                name: "FK_RII_SD_Quotes_RII_SD_Customers_CustomerId",
                table: "RII_SD_Quotes",
                column: "CustomerId",
                principalTable: "RII_SD_Customers",
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
                name: "FK_RII_SD_RecurringPayments_RII_SD_Customers_CustomerId",
                table: "RII_SD_RecurringPayments",
                column: "CustomerId",
                principalTable: "RII_SD_Customers",
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
                name: "FK_RII_SD_SoftwareResearches_RII_SD_PotentialCustomers_PotentialCustomerId",
                table: "RII_SD_SoftwareResearches",
                column: "PotentialCustomerId",
                principalTable: "RII_SD_PotentialCustomers",
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
                name: "FK_RII_SD_Tasks_RII_SD_Customers_CustomerId",
                table: "RII_SD_Tasks",
                column: "CustomerId",
                principalTable: "RII_SD_Customers",
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
                name: "FK_RII_SD_VisitForms_RII_SD_Customers_CustomerId",
                table: "RII_SD_VisitForms",
                column: "CustomerId",
                principalTable: "RII_SD_Customers",
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
                name: "FK_RII_SD_Visits_RII_SD_Customers_CustomerId",
                table: "RII_SD_Visits",
                column: "CustomerId",
                principalTable: "RII_SD_Customers",
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
        }
    }
}
