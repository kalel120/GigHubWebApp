using GigHubWebApp.Core.Models;
using System.Collections.Generic;

namespace GigHubWebApp.Core.Repositories {
    public interface INotificationRepository {
        IEnumerable<Notification> GetNewNotifications(string userId);
        IEnumerable<UserNotification> GetUsersUnreadNotifications(string userId);
    }
}