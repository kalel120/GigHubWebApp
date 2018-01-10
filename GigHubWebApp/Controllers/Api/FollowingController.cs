using GigHubWebApp.DTOs;
using GigHubWebApp.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHubWebApp.Controllers.Api {
    [Authorize]
    public class FollowingController : ApiController {
        private readonly ApplicationDbContext _dbContext;

        public FollowingController() {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult FollowArtist(FollowingDto followingDto) {
            var userId = User.Identity.GetUserId();

            if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.ArtistId)) {
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

        [HttpDelete]
        public IHttpActionResult UnFollowArtist(string id) {
            var userId = User.Identity.GetUserId();

            var following =
                _dbContext.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null) {
                return NotFound();
            }

            _dbContext.Followings.Remove(following);
            _dbContext.SaveChanges();

            return Ok(id);
        }
    }
}
