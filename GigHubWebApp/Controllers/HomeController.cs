using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHubWebApp.Models;
using GigHubWebApp.ViewModels;

namespace GigHubWebApp.Controllers {
    public class HomeController : Controller {
        private readonly ApplicationDbContext _dbContext;

        public HomeController() {
            _dbContext = new ApplicationDbContext();
        }


        public ActionResult Index() {
            var upcomingGigs = _dbContext.Gigs
                            .Include(g => g.Artist)
                            .Include(g => g.Genre)
                            .Where(g => g.DateTime > DateTime.Now);

            var upcomingGigsViewModel = new GigsViewModel {
                UpcomingGigs = upcomingGigs,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Heading = "All Upcoming Gigs"
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