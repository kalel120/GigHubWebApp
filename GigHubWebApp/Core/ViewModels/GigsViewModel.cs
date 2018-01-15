using GigHubWebApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.Core.ViewModels {
    public class GigsViewModel {
        public bool IsAuthenticated { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; set; }
        public ILookup<string, Following> Followees { get; set; }
    }
}