using AutoMapper;
using GigHubWebApp.Core;
using GigHubWebApp.Core.DTOs;
using GigHubWebApp.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHubWebApp.Controllers.Api {
    [Authorize]
    public class NotificationsController : ApiController {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications() {
            var notifications = _unitOfWork.NotificationRepository.GetNewNotifications(User.Identity.GetUserId());

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead() {
            var notifications = _unitOfWork.NotificationRepository.GetUsersUnreadNotifications(User.Identity.GetUserId());

            foreach (var notification in notifications) {
                notification.Read();    //another way >> notifications.ForEach(n => n.Read());
            }

            _unitOfWork.Complete();

            return Ok();
        }
    }
}