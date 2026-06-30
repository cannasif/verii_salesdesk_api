using System.Text.Json.Serialization;

namespace salesdesk_api.Modules.Notification.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NotificationType
    {
        General,
        Dashboard,
        Customer,
        Potential,
        Product,
        Quote,
        Invoice,
        Task,
        Visit,
        VisitForm,
        Asset,
        RecurringPayment,
        SoftwareResearch,
        ErpNews,
        GmailMessage
    }
}
