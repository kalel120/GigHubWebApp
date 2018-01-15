using GigHubWebApp.Core.ViewModels;
using GigHubWebApp.Persistence;
using GigHubWebApp.Repositories;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHubWebApp.Controllers {
    public class HomeController : Controller {
        private readonly ApplicationDbContext _dbContext;
        private readonly AttendancesRepository _attendancesRepo;
        private readonly FollowingRepository _followingRepo;
        private readonly GigsRepositories _gigsRepo;

        public HomeController() {
            _dbContext = new ApplicationDbContext();
            _attendancesRepo = new AttendancesRepository(_dbContext);
            _followingRepo = new FollowingRepository(_dbContext);
            _gigsRepo = new GigsRepositories(_dbContext);
        }


        public ActionResult Index(string query = null) {
            var userId = User.Identity.GetUserId();

            var upcomingGigs = _gigsRepo.GetAllUpcomingGigs();

            if (!string.IsNullOrWhiteSpace(query)) {
                upcomingGigs = _gigsRepo.GetUpcomingGigsSearchResult(query, upcomingGigs);
            }

            var upcomingGigsViewModel = new GigsViewModel {
                UpcomingGigs = upcomingGigs,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Followees = _followingRepo.GetFollowingsTableByFollowerId(userId).ToLookup(f => f.FolloweeId),
                Attendences = _attendancesRepo.GetUsersFutureAttendances(userId).ToLookup(a => a.GigId),
                Heading = "All Upcoming Gigs",
                SearchTerm = query
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