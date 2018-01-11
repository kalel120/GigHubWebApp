using GigHubWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.Repositories {
    public class FollowingRepository {
        private readonly ApplicationDbContext _dbContext;

        public FollowingRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Following> GetArtistUserFollowing(string userId) {
            return _dbContext.Followings.Where(f => f.FollowerId == userId).ToList();
        }

    }
}