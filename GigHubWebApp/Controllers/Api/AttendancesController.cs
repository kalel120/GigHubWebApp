using GigHubWebApp.DTOs;
using GigHubWebApp.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHubWebApp.Controllers.Api {
    [Authorize]
    public class AttendancesController : ApiController {

        private readonly ApplicationDbContext _dbContext;

        public AttendancesController() {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto gigDto) {
            var userId = User.Identity.GetUserId();

            if (_dbContext.Attendences.Any(a => a.GigId == gigDto.GigId && a.AttendeeId == userId)) {
                return BadRequest("Duplicate Attendance");
            }

            var attendence = new Attendence {
                GigId = gigDto.GigId,
                AttendeeId = userId
            };

            _dbContext.Attendences.Add(attendence);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Absent(int id) {
            var userId = User.Identity.GetUserId();

            var attendence = _dbContext.Attendences.SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);
            if (attendence == null) {
                return NotFound();
            }

            _dbContext.Attendences.Remove(attendence);
            _dbContext.SaveChanges();

            return Ok(id);
        }
    }
}
