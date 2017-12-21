using GigHubWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.ViewModels {
    public class GigsViewModel {
        public bool IsAuthenticated { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendence> Attendences { get; set; }
        public IEnumerable<string> Followings { get; set; }
    }
}