using GigHubWebApp.Core;
using GigHubWebApp.Core.DTOs;
using GigHubWebApp.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHubWebApp.Controllers.Api {
    [Authorize]
    public class FollowingController : ApiController {
        private readonly IUnitOfWork _unitOfWork;
        public FollowingController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult FollowArtist(FollowingDto followingDto) {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.FollowingRepo.GetFolloweeByFollowerId(userId, followingDto.ArtistId) != null) {
                return BadRequest("Duplicate following task");
            }

            var following = new Following {
                FollowerId = userId,
                FolloweeId = followingDto.ArtistId
            };

            _unitOfWork.FollowingRepo.Add(following);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollowArtist(string id) {
            var userId = User.Identity.GetUserId();

            var following = _unitOfWork.FollowingRepo.GetFolloweeByFollowerId(userId, id);
            if (following == null) {
                return NotFound();
            }

            _unitOfWork.FollowingRepo.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
