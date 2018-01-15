using GigHubWebApp.Core;
using GigHubWebApp.Core.DTOs;
using GigHubWebApp.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHubWebApp.Controllers.Api {
    [Authorize]
    public class AttendancesController : ApiController {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto gigDto) {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.AttendancesRepo.GetAttendanceByGigId(gigDto.GigId, userId) != null) {
                return BadRequest("Duplicate Attendance");
            }

            var attendence = new Attendence {
                GigId = gigDto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.AttendancesRepo.Add(attendence);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Absent(int id) {
            var userId = User.Identity.GetUserId();

            var attendence = _unitOfWork.AttendancesRepo.GetAttendanceByGigId(id, userId);
            if (attendence == null) {
                return NotFound();
            }

            _unitOfWork.AttendancesRepo.Remove(attendence);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
