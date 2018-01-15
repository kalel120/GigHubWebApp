using GigHubWebApp.Core;
using GigHubWebApp.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHubWebApp.Controllers {
    public class FollowingArtistController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingArtistController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public ActionResult GetFollowingArtist() {
            var myFollowingArtists = _unitOfWork.FollowingRepo.GetFolloweesByFollowerId(User.Identity.GetUserId());

            var viewModel = new FollowingArtistViewModel {
                FollowingArtist = myFollowingArtists
            };
            return View("FollowingArtist", viewModel);
        }
    }
}