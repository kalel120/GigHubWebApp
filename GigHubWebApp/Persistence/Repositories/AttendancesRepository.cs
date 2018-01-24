using System;
using System.Collections.Generic;
using System.Linq;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;

namespace GigHubWebApp.Persistence.Repositories {
    public class AttendancesRepository : IAttendancesRepository {
        private readonly ApplicationDbContext _dbContext;

        public AttendancesRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Attendance> GetUsersFutureAttendances(string userId) {
            return _dbContext.Attendances.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList();
        }

        public Attendance GetAttendanceByGigId(int id, string userId) {
            return _dbContext.Attendances.SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);
        }

        public void Add(Attendance attendance) {
            _dbContext.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance) {
            _dbContext.Attendances.Remove(attendance);
        }
    }
}