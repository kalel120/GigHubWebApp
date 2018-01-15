using GigHubWebApp.Core.Models;
using System.Collections.Generic;

namespace GigHubWebApp.Core.ViewModels {
    public class FollowingArtistViewModel {
        public IEnumerable<ApplicationUser> FollowingArtist { get; set; }
    }
}