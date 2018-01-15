using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;
using GigHubWebApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.Repositories {
    public class AttendancesRepository : IAttendancesRepository {
        private readonly ApplicationDbContext _dbContext;

        public AttendancesRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Attendence> GetUsersFutureAttendances(string userId) {
            return _dbContext.Attendences.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList();
        }

        public Attendence GetAttendanceByGigId(int id, string userId) {
            return _dbContext.Attendences.SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);
        }

        public void Add(Attendence attendence) {
            _dbContext.Attendences.Add(attendence);
        }

        public void Remove(Attendence attendence) {
            _dbContext.Attendences.Remove(attendence);
        }
    }
}