namespace salesdesk_api.Modules.System.Domain.Entities
{
    public class SystemSetting : BaseEntity
    {
        public string NumberFormat { get; set; } = "tr-TR";
        public int DecimalPlaces { get; set; } = 2;
        public string CurrencyCode { get; set; } = "TRY";
        public decimal DefaultVatRate { get; set; } = 20m;
        public decimal MaxGeneralDiscountRate { get; set; } = 100m;
        public bool EnableGmailInbox { get; set; } = true;
        public bool EnableSalesDeskNotifications { get; set; } = true;
    }
}
