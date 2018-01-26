using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHubWebApp.Persistence.Repositories {
    public class NotificationsRepository : INotificationRepository {
        private readonly IApplicationDbContext _dbContext;

        public NotificationsRepository(IApplicationDbContext dbContext) {
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