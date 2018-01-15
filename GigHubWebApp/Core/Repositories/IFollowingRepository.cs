using GigHubWebApp.Core.Models;
using System.Collections.Generic;

namespace GigHubWebApp.Core.Repositories {
    public interface IFollowingRepository {
        IEnumerable<Following> GetFollowingsTableByFollowerId(string userId);
        IEnumerable<ApplicationUser> GetFolloweesByFollowerId(string userId);
        Following GetFolloweeByFollowerId(string userId, string followeeId);
        bool IsUserFollowing(string artistId, string userId);
        void Add(Following following);
        void Remove(Following following);
    }
}