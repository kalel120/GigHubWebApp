using GigHubWebApp.Core;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Http;

namespace GigHubWebApp.Controllers.Api {
    [Authorize]
    public class GigsController : ApiController {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id) {
            var gig = _unitOfWork.GigsRepo.GetGigWithAttendees(id);

            if (gig.ArtistId != User.Identity.GetUserId()) {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (gig.IsCanceled) {
                return NotFound();
            }

            _unitOfWork.GigsRepo.CancelGigWithNotificationToAttendees(gig);

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
