﻿using GigHubWebApp.Core;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHubWebApp.Controllers {
    public class GigsController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Create() {
            var gigFormViewModel = new GigFormViewModel {
                Genres = _unitOfWork.GenreRepo.GetGenres(),
                Heading = "Create Gig"
            };
            return View("GigForm", gigFormViewModel);
        }

        [Authorize]
        public ActionResult Edit(int gigId) {
            var gig = _unitOfWork.GigsRepo.GetGigByGigId(gigId);

            if (gig == null) {
                return HttpNotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId()) {
                return new HttpUnauthorizedResult();
            }

            var gigFormViewModel = new GigFormViewModel {
                Id = gigId,
                Genres = _unitOfWork.GenreRepo.GetGenres(),
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
                gigFormViewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                return View("GigForm", gigFormViewModel);
            }

            var objGig = new Gig {
                ArtistId = User.Identity.GetUserId(),
                DateTime = gigFormViewModel.GetDateTime(),
                Venue = gigFormViewModel.Venue,
                GenreId = gigFormViewModel.Genre
            };

            _unitOfWork.GigsRepo.Add(objGig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel gigFormViewModel) {
            if (!ModelState.IsValid) {
                gigFormViewModel.Genres = _unitOfWork.GenreRepo.GetGenres();
                return View("GigForm", gigFormViewModel);
            }

            var gig = _unitOfWork.GigsRepo.GetGigWithAttendees(gigFormViewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Update(gigFormViewModel);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Attending() {
            var userId = User.Identity.GetUserId();

            var gigsViewModel = new GigsViewModel {
                UpcomingGigs = _unitOfWork.GigsRepo.GetUpcomingGigsByArtistId(userId),
                Attendances = _unitOfWork.AttendancesRepo.GetUsersFutureAttendances(userId).ToLookup(a => a.GigId),
                Followees = _unitOfWork.FollowingRepo.GetFollowingsTableByFollowerId(userId).ToLookup(f => f.FolloweeId),
                IsAuthenticated = User.Identity.IsAuthenticated,
                Heading = "Gig's I'm Going"
            };
            return View("Gigs", gigsViewModel);
        }


        [Authorize]
        public ActionResult Mine() {
            var userId = User.Identity.GetUserId();
            var myUpcomingGigs = _unitOfWork.GigsRepo.GetAllUpcomingGigs().Where(g => g.ArtistId == userId);

            return View(myUpcomingGigs);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel gigViewModel) {
            return RedirectToAction("Index", "Home", new { query = gigViewModel.SearchTerm });
        }


        public ActionResult Details(int id) {
            var gig = _unitOfWork.GigsRepo.GetGigByGigId(id);

            if (gig == null) {
                return HttpNotFound();
            }

            var gigDetailsViewModel = new GigDetailsViewModel {
                Gig = gig
            };

            if (User.Identity.IsAuthenticated) {
                var userId = User.Identity.GetUserId();
                // The inequality operator (!=) returns false if its operands are equal, true otherwise
                gigDetailsViewModel.IsAttending = _unitOfWork.AttendancesRepo.GetAttendanceByGigId(id, userId) != null;
                gigDetailsViewModel.IsFollowing = _unitOfWork.FollowingRepo.IsUserFollowing(gig.ArtistId, userId);
            }

            return View("Details", gigDetailsViewModel);
        }
    }
}