using System;
using System.Linq;
using System.Web.Http;
using GigHubWebApp.Models;
using Microsoft.AspNet.Identity;

namespace GigHubWebApp.Controllers.Api {
    [Authorize]
    public class GigsController : ApiController {
        private readonly ApplicationDbContext _dbContext;

        public GigsController() {
            _dbContext = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id) {
            var userId = User.Identity.GetUserId();
            var gig = _dbContext.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled) {
                return NotFound();
            }

            gig.IsCanceled = true;

            var notification = new Notification(NotificationType.GigCanceled, gig);

            var attendees = _dbContext.Attendences.Where(g => g.GigId == id).Select(u => u.Attendee).ToList();

            foreach (var attendee in attendees) {
                attendee.Notify(notification);
            }

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
