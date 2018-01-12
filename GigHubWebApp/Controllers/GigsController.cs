using GigHubWebApp.Models;
using GigHubWebApp.Repositories;
using GigHubWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHubWebApp.Controllers {
    public class GigsController : Controller {
        private readonly ApplicationDbContext _dbContext;
        private readonly GigsRepositories _gigsRepo;
        private readonly AttendancesRepository _attendancesRepo;
        private readonly FollowingRepository _followingRepo;
        private readonly GenreRepository _genreRepo;

        public GigsController() {
            _dbContext = new ApplicationDbContext();
            _gigsRepo = new GigsRepositories(_dbContext);
            _attendancesRepo = new AttendancesRepository(_dbContext);
            _followingRepo = new FollowingRepository(_dbContext);
            _genreRepo = new GenreRepository(_dbContext);
        }

        [Authorize]
        public ActionResult Create() {
            var gigFormViewModel = new GigFormViewModel {
                Genres = _genreRepo.GetGenres(),
                Heading = "Create Gig"
            };
            return View("GigForm", gigFormViewModel);
        }

        [Authorize]
        public ActionResult Edit(int gigId) {
            var gig = _gigsRepo.GetGigByGigId(gigId);

            if (gig == null) {
                return HttpNotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId()) {
                return new HttpUnauthorizedResult();
            }

            var gigFormViewModel = new GigFormViewModel {
                Id = gigId,
                Genres = _genreRepo.GetGenres(),
                Date = gig.DateTime.ToString("dd MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Genre = gig.GenreId,
                Heading = "Update Gig"
            };
            return View("GigForm", gigFormViewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel gigFormViewModel) {
            if (!ModelState.IsValid) {
                gigFormViewModel.Genres = _genreRepo.GetGenres();
                return View("GigForm", gigFormViewModel);
            }
            var objGig = new Gig {
                ArtistId = User.Identity.GetUserId(),
                DateTime = gigFormViewModel.GetDateTime(),
                Venue = gigFormViewModel.Venue,
                GenreId = gigFormViewModel.Genre
            };

            _dbContext.Gigs.Add(objGig);
            _dbContext.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel gigFormViewModel) {
            if (!ModelState.IsValid) {
                gigFormViewModel.Genres = _genreRepo.GetGenres();
                return View("GigForm", gigFormViewModel);
            }

            var gig = _gigsRepo.GetGigWithAttendees(gigFormViewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Update(gigFormViewModel);

            _dbContext.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Attending() {
            var userId = User.Identity.GetUserId();

            var gigsViewModel = new GigsViewModel {
                UpcomingGigs = _gigsRepo.GetGigsUserAttending(userId),
                Attendences = _attendancesRepo.GetUsersFutureAttendances(userId).ToLookup(a => a.GigId),
                Followees = _followingRepo.GetArtistUserFollowing(userId).ToLookup(f => f.FolloweeId),
                IsAuthenticated = User.Identity.IsAuthenticated,
                Heading = "Gig's I'm Going"
            };
            return View("Gigs", gigsViewModel);
        }


        [Authorize]
        public ActionResult Mine() {
            var userId = User.Identity.GetUserId();
            var myUpcomingGigs = _gigsRepo.GetAllUpcomingGigs().Where(g => g.ArtistId == userId);

            return View(myUpcomingGigs);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel gigViewModel) {
            return RedirectToAction("Index", "Home", new { query = gigViewModel.SearchTerm });
        }


        public ActionResult Details(int id) {
            var gig = _gigsRepo.GetGigByGigId(id);

            if (gig == null) {
                return HttpNotFound();
            }

            var gigDetailsViewModel = new GigDetailsViewModel {
                Gig = gig
            };

            if (User.Identity.IsAuthenticated) {
                var userId = User.Identity.GetUserId();
                gigDetailsViewModel.IsAttending = _attendancesRepo.GetUserAttendance(id, userId);
                gigDetailsViewModel.IsFollowing = _followingRepo.GetUserFollowingArtistOrNot(gig.ArtistId, userId);
            }

            return View("Details", gigDetailsViewModel);
        }
    }
}