using GigHubWebApp.Core.Models;
using System.Collections.Generic;

namespace GigHubWebApp.Core.Repositories {
    public interface IAttendancesRepository {
        IEnumerable<Attendance> GetUsersFutureAttendances(string userId);
        Attendance GetAttendanceByGigId(int id, string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}