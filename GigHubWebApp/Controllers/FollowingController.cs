using System.Linq;
using System.Web.Http;
using GigHubWebApp.DTOs;
using GigHubWebApp.Models;
using Microsoft.AspNet.Identity;

namespace GigHubWebApp.Controllers {
    [Authorize]
    public class FollowingController : ApiController {
        private readonly ApplicationDbContext _dbContext;

        public FollowingController() {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult FollowArtist(FollowingDto followingDto) {
            var userId = User.Identity.GetUserId();

            if (_dbContext.Followings.Any(f => f.FolloweeId == userId && f.FollowerId == followingDto.ArtistId)) {
                return BadRequest("Duplicate following task");
            }

            var objFollowing = new Following {
                FollowerId = userId,
                FolloweeId = followingDto.ArtistId
            };

            _dbContext.Followings.Add(objFollowing);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
