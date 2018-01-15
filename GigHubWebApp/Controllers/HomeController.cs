using GigHubWebApp.Core;
using GigHubWebApp.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHubWebApp.Controllers {
    public class HomeController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null) {
            var upcomingGigs = _unitOfWork.GigsRepo.GetAllUpcomingGigs();

            if (!string.IsNullOrWhiteSpace(query)) {
                upcomingGigs = _unitOfWork.GigsRepo.GetUpcomingGigsSearchResult(query, upcomingGigs);
            }

            var userId = User.Identity.GetUserId();
            var upcomingGigsViewModel = new GigsViewModel {
                UpcomingGigs = upcomingGigs,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Followees = _unitOfWork.FollowingRepo.GetFollowingsTableByFollowerId(userId).ToLookup(f => f.FolloweeId),
                Attendences = _unitOfWork.AttendancesRepo.GetUsersFutureAttendances(userId).ToLookup(a => a.GigId),
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