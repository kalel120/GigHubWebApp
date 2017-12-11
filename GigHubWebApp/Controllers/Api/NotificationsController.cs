using AutoMapper;
using GigHubWebApp.DTOs;
using GigHubWebApp.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHubWebApp.Controllers.Api {
    [Authorize]
    public class NotificationsController : ApiController {
        private readonly ApplicationDbContext _dbContext;

        public NotificationsController() {
            _dbContext = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications() {
            var userId = User.Identity.GetUserId();
            var notifications = _dbContext.UserNotifications
                                            .Where(un => un.UserId == userId && !un.IsRead)
                                            .Select(un => un.Notification)
                                            .Include(n => n.Gig.Artist)
                                            .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
