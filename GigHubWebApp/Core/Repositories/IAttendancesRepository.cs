using System.Collections.Generic;
using GigHubWebApp.Core.Models;

namespace GigHubWebApp.Core.Repositories {
    public interface IAttendancesRepository {
        IEnumerable<Attendence> GetUsersFutureAttendances(string userId);
        bool GetUserAttendance(int id, string userId);
    }
}