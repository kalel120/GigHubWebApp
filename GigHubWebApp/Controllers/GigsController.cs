using GigHubWebApp.Models;
using GigHubWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHubWebApp.Controllers {
    public class GigsController : Controller {
        private readonly ApplicationDbContext _dbContext;

        public GigsController() {
            _dbContext = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create() {
            var gigFormViewModel = new GigFormViewModel {
                Genres = _dbContext.Genres.ToList(),
                Heading = "Create Gig"
            };
            return View("GigForm", gigFormViewModel);
        }

        [Authorize]
        public ActionResult Edit(int gigId) {
            var userId = User.Identity.GetUserId();
            var gig = _dbContext.Gigs.Single(g => g.Id == gigId && g.ArtistId == userId);

            var gigFormViewModel = new GigFormViewModel {
                Id = gigId,
                Genres = _dbContext.Genres.ToList(),
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
                gigFormViewModel.Genres = _dbContext.Genres.ToList();
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
                gigFormViewModel.Genres = _dbContext.Genres.ToList();
                return View("GigForm", gigFormViewModel);
            }
            var userId = User.Identity.GetUserId();
            var gig = _dbContext.Gigs
                .Include(g => g.Attendences.Select(a => a.Attendee))
                .Single(g => g.Id == gigFormViewModel.Id && g.ArtistId == userId);

            gig.Update(gigFormViewModel);

            _dbContext.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
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
                IsAuthenticated = User.Identity.IsAuthenticated,
                Heading = "Gig's I'm Going"
            };
            return View("Gigs", gigsViewModel);
        }

        [Authorize]
        public ActionResult Mine() {
            var userId = User.Identity.GetUserId();
            var myUpcomingGigs = _dbContext.Gigs
                                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && g.IsCanceled == false)
                                .Include(g => g.Genre)
                                .ToList();
            return View(myUpcomingGigs);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel gigViewModel) {
            return RedirectToAction("Index", "Home", new { query = gigViewModel.SearchTerm });
        }
    }
}