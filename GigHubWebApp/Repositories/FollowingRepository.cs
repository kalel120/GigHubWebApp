using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;
using GigHubWebApp.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.Repositories {
    public class FollowingRepository : IFollowingRepository {
        private readonly ApplicationDbContext _dbContext;

        public FollowingRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Following> GetFollowingsTableByFollowerId(string userId) {
            return _dbContext.Followings.Where(f => f.FollowerId == userId).ToList();
        }

        public IEnumerable<ApplicationUser> GetFolloweesByFollowerId(string userId) {
            return _dbContext.Followings.Where(u => u.FollowerId == userId)
                .Select(u => u.Followee)
                .ToList();
        }

        public bool GetUserFollowingArtistOrNot(string artistId, string userId) {
            return _dbContext.Followings.Any(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }

    }
}