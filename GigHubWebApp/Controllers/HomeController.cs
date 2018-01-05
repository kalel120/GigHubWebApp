using GigHubWebApp.Models;
using GigHubWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHubWebApp.Controllers {
    public class HomeController : Controller {
        private readonly ApplicationDbContext _dbContext;

        public HomeController() {
            _dbContext = new ApplicationDbContext();
        }


        public ActionResult Index(string query = null) {
            var upcomingGigs = _dbContext.Gigs
                            .Include(g => g.Artist)
                            .Include(g => g.Genre)
                            .Where(g => g.DateTime > DateTime.Now && g.IsCanceled == false);

            if (!string.IsNullOrWhiteSpace(query)) {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                                                       g.Genre.Name.Contains(query) ||
                                                       g.Venue.Contains(query));
            }

            var userId = User.Identity.GetUserId();
            var attendences = _dbContext.Attendences.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                                      .ToList()
                                      .ToLookup(a => a.GigId);

            var upcomingGigsViewModel = new GigsViewModel {
                UpcomingGigs = upcomingGigs,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Heading = "All Upcoming Gigs",
                SearchTerm = query,
                Attendences = attendences
            };
            return View("Gigs", upcomingGigsViewModel);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}