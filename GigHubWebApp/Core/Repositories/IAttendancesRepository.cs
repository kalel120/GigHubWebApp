using GigHubWebApp.Core.Models;
using System.Collections.Generic;

namespace GigHubWebApp.Core.Repositories {
    public interface IAttendancesRepository {
        IEnumerable<Attendence> GetUsersFutureAttendances(string userId);
        Attendence GetAttendanceByGigId(int id, string userId);
        void Add(Attendence attendence);
        void Remove(Attendence attendence);
    }
}