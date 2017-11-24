using System.Collections.Generic;
using GigHubWebApp.Models;

namespace GigHubWebApp.ViewModels {
    public class GigsViewModel {
        public bool IsAuthenticated { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
    }
}