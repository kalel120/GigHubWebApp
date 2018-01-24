using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;

namespace GigHubWebApp.Persistence.Repositories {
    public class NotificationsRepository : INotificationRepository {
        private readonly ApplicationDbContext _dbContext;

        public NotificationsRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Notification> GetNewNotifications(string userId) {
            return _dbContext.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }

        public IEnumerable<UserNotification> GetUsersUnreadNotifications(string userId) {
            return _dbContext.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
        }
    }
}