using GigHubWebApp.Models;
using System.Collections.Generic;

namespace GigHubWebApp.Repositories {
    public interface IFollowingRepository {
        IEnumerable<Following> GetArtistUserFollowing(string userId);
        bool GetUserFollowingArtistOrNot(string artistId, string userId);
    }
}