using System.Linq;
using System.Web.Mvc;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.ViewModels;
using GigHubWebApp.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHubWebApp.Controllers {
    public class FollowingArtistController : Controller {
        private readonly ApplicationDbContext _dbContext;

        public FollowingArtistController() {
            _dbContext = new ApplicationDbContext();
        }

        // GET: FollowingArtist
        public ActionResult GetFollowingArtist() {
            var userId = User.Identity.GetUserId();

            var myFollowingArtists = _dbContext.Followings
                .Where(u => u.FollowerId == userId)
                .Select(u => u.Followee)
                .ToList();

            var viewModel = new FollowingArtistViewModel {
                FollowingArtist = myFollowingArtists
            };
            return View("FollowingArtist", viewModel);
        }
    }
}