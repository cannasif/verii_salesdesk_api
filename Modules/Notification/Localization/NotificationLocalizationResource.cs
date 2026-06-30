using salesdesk_api.Shared.Common.Application.Common;

namespace salesdesk_api.Modules.Notification.Localization;

public sealed class NotificationLocalizationResource : ILocalizationResource
{
    public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> MessagesByCulture { get; } =
        new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["en"] = Build("All notifications marked as read.", "Internal server error.", "Notification created.", "Notification deleted.", "Notification marked as read.", "Notification not found.", "Notifications retrieved successfully.", "Unread count retrieved."),
            ["tr"] = Build("Tum bildirimler okundu olarak isaretlendi.", "Dahili sunucu hatasi olustu.", "Bildirim olusturuldu.", "Bildirim silindi.", "Bildirim okundu olarak isaretlendi.", "Bildirim bulunamadi.", "Bildirimler basariyla getirildi.", "Okunmamis bildirim sayisi getirildi."),
            ["de"] = Build("Tum bildirimler okundu olarak isaretlendi.", "Dahili sunucu hatasi olustu.", "Bildirim olusturuldu.", "Bildirim silindi.", "Bildirim okundu olarak isaretlendi.", "Bildirim bulunamadi.", "Bildirimler basariyla getirildi.", "Okunmamis bildirim sayisi getirildi."),
            ["fr"] = Build("Tum bildirimler okundu olarak isaretlendi.", "Dahili sunucu hatasi olustu.", "Bildirim olusturuldu.", "Bildirim silindi.", "Bildirim okundu olarak isaretlendi.", "Bildirim bulunamadi.", "Bildirimler basariyla getirildi.", "Okunmamis bildirim sayisi getirildi."),
            ["es"] = Build("Tum bildirimler okundu olarak isaretlendi.", "Dahili sunucu hatasi olustu.", "Bildirim olusturuldu.", "Bildirim silindi.", "Bildirim okundu olarak isaretlendi.", "Bildirim bulunamadi.", "Bildirimler basariyla getirildi.", "Okunmamis bildirim sayisi getirildi."),
            ["it"] = Build("Tum bildirimler okundu olarak isaretlendi.", "Dahili sunucu hatasi olustu.", "Bildirim olusturuldu.", "Bildirim silindi.", "Bildirim okundu olarak isaretlendi.", "Bildirim bulunamadi.", "Bildirimler basariyla getirildi.", "Okunmamis bildirim sayisi getirildi."),
            ["ar"] = Build("Tum bildirimler okundu olarak isaretlendi.", "Dahili sunucu hatasi olustu.", "Bildirim olusturuldu.", "Bildirim silindi.", "Bildirim okundu olarak isaretlendi.", "Bildirim bulunamadi.", "Bildirimler basariyla getirildi.", "Okunmamis bildirim sayisi getirildi.")
        };

    private static IReadOnlyDictionary<string, string> Build(
        string allRead,
        string serverError,
        string created,
        string deleted,
        string markedAsRead,
        string notFound,
        string retrieved,
        string unreadCount)
    {
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Notification.SalesDesk.Message"] = "{0}",
            ["Notification.SalesDesk.Title"] = "SalesDesk",
            ["NotificationService.AllNotificationsMarkedAsRead"] = allRead,
            ["NotificationService.InternalServerError"] = serverError,
            ["NotificationService.NotificationCreated"] = created,
            ["NotificationService.NotificationDeleted"] = deleted,
            ["NotificationService.NotificationMarkedAsRead"] = markedAsRead,
            ["NotificationService.NotificationNotFound"] = notFound,
            ["NotificationService.NotificationsRetrieved"] = retrieved,
            ["NotificationService.UnreadCountRetrieved"] = unreadCount
        };
    }
}
