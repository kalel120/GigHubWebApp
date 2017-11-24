using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHubWebApp.Models;
using GigHubWebApp.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHubWebApp.Controllers {
    public class GigsController : Controller {
        private readonly ApplicationDbContext _dbContext;

        public GigsController() {
            _dbContext = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create() {
            var gigFormViewModel = new GigFormViewModel {
                Genres = _dbContext.Genres.ToList()
            };
            return View(gigFormViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel gigFormViewModel) {
            if (!ModelState.IsValid) {
                gigFormViewModel.Genres = _dbContext.Genres.ToList();
                return View("Create", gigFormViewModel);
            }
            var objGig = new Gig {
                ArtistId = User.Identity.GetUserId(),
                DateTime = gigFormViewModel.GetDateTime(),
                Venue = gigFormViewModel.Venue,
                GenreId = gigFormViewModel.Genre
            };

            _dbContext.Gigs.Add(objGig);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Attending() {
            var userId = User.Identity.GetUserId();

            var attendingGigs = _dbContext.Attendences
                                .Where(a => a.AttendeeId == userId)
                                .Select(a => a.Gig)
                                .Include(ar => ar.Artist)
                                .Include(g => g.Genre)
                                .ToList();

            var gigsViewModel = new GigsViewModel {
                UpcomingGigs = attendingGigs,
                IsAuthenticated = User.Identity.IsAuthenticated
            };
            return View(gigsViewModel);
        }
    }
}