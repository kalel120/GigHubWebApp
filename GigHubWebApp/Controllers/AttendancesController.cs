using System.Linq;
using System.Web.Http;
using GigHubWebApp.Models;
using Microsoft.AspNet.Identity;

namespace GigHubWebApp.Controllers {
    [Authorize]
    public class AttendancesController : ApiController {

        private readonly ApplicationDbContext _dbContext;

        public AttendancesController() {
            _dbContext = new ApplicationDbContext();
        }

        public IHttpActionResult Attend([FromBody] int gigId) {
            var userId = User.Identity.GetUserId();

            if (_dbContext.Attendences.Any(a => a.GigId == gigId && a.AttendeeId == userId)) {
                return BadRequest("Duplicate Attendance");
            }

            var attendence = new Attendence {
                GigId = gigId,
                AttendeeId = userId
            };

            _dbContext.Attendences.Add(attendence);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
