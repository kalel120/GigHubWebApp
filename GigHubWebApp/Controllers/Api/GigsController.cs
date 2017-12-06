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
            gig.IsCanceled = true;

            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
