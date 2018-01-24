using System.Collections.Generic;
using System.Linq;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;

namespace GigHubWebApp.Persistence.Repositories {
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

        public Following GetFolloweeByFollowerId(string userId, string followeeId) {
            return _dbContext.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == followeeId);
        }

        public bool IsUserFollowing(string artistId, string userId) {
            return _dbContext.Followings.Any(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }

        public void Add(Following following) {
            _dbContext.Followings.Add(following);
        }

        public void Remove(Following following) {
            _dbContext.Followings.Remove(following);
        }
    }
}