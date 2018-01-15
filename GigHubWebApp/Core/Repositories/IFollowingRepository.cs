using System.Collections.Generic;
using GigHubWebApp.Core.Models;

namespace GigHubWebApp.Core.Repositories {
    public interface IFollowingRepository {
        IEnumerable<Following> GetArtistUserFollowing(string userId);
        bool GetUserFollowingArtistOrNot(string artistId, string userId);
    }
}