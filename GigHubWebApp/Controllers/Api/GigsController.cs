using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Persistence;

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

            var gig = _dbContext.Gigs
                .Include(g => g.Attendences.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled) {
                return NotFound();
            }

            gig.Cancel();

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
