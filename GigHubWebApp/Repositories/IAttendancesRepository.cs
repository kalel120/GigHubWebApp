using GigHubWebApp.Models;
using System.Collections.Generic;

namespace GigHubWebApp.Repositories {
    public interface IAttendancesRepository {
        IEnumerable<Attendence> GetUsersFutureAttendances(string userId);
        bool GetUserAttendance(int id, string userId);
    }
}