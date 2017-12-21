using GigHubWebApp.Models;

namespace GigHubWebApp.ViewModels {
    public class GigDetailsViewModel {
        public Gig Gig { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}