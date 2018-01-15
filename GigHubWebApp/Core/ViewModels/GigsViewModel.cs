using System.Collections.Generic;
using System.Linq;
using GigHubWebApp.Core.Models;

namespace GigHubWebApp.Core.ViewModels {
    public class GigsViewModel {
        public bool IsAuthenticated { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendence> Attendences { get; set; }
        public ILookup<string, Following> Followees { get; set; }
    }
}