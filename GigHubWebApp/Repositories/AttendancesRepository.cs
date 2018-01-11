using GigHubWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.Repositories {
    public class AttendancesRepository {
        private readonly ApplicationDbContext _dbContext;

        public AttendancesRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }


        public IEnumerable<Attendence> GetUsersFutureAttendances(string userId) {
            return _dbContext.Attendences.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList();
        }

    }
}