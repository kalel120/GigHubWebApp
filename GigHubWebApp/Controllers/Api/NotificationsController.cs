using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHubWebApp.Core.DTOs;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Persistence;

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

        [HttpPost]
        public IHttpActionResult MarkAsRead() {
            var userId = User.Identity.GetUserId();
            var notifications = _dbContext.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            foreach (var notification in notifications) {
                notification.Read();
            }

            //another way
            //notifications.ForEach(n => n.Read());

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
