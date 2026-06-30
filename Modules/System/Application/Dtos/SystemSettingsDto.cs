using System;
using System.ComponentModel.DataAnnotations;

namespace salesdesk_api.Modules.System.Application.Dtos
{
    public class SystemSettingsDto
    {
        public string NumberFormat { get; set; } = "tr-TR";
        public int DecimalPlaces { get; set; } = 2;
        public string CurrencyCode { get; set; } = "TRY";
        public decimal DefaultVatRate { get; set; } = 20m;
        public decimal MaxGeneralDiscountRate { get; set; } = 100m;
        public bool EnableGmailInbox { get; set; } = true;
        public bool EnableSalesDeskNotifications { get; set; } = true;
        public DateTime? UpdatedAt { get; set; }
    }

    public class UpdateSystemSettingsDto
    {
        [Required]
        [MaxLength(20)]
        public string NumberFormat { get; set; } = "tr-TR";

        [Range(0, 6)]
        public int DecimalPlaces { get; set; } = 2;

        [Required]
        [MaxLength(8)]
        public string CurrencyCode { get; set; } = "TRY";

        [Range(0, 100)]
        public decimal DefaultVatRate { get; set; } = 20m;

        [Range(0, 100)]
        public decimal MaxGeneralDiscountRate { get; set; } = 100m;

        public bool EnableGmailInbox { get; set; } = true;
        public bool EnableSalesDeskNotifications { get; set; } = true;
        public UpdateDocumentFieldLabelsRequest? DocumentFieldLabels { get; set; }
    }
}
