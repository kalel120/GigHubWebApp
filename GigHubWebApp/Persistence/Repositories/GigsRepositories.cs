using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHubWebApp.Persistence.Repositories {
    public class GigsRepositories : IGigsRepositories {
        private readonly IApplicationDbContext _dbContext;

        public GigsRepositories(IApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtistId(string userId) {
            return _dbContext.Gigs
                .Where(g => g.ArtistId == userId
                            && g.DateTime > DateTime.Now
                            && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public IQueryable<Gig> GetAllUpcomingGigs() {
            return _dbContext.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && g.IsCanceled == false);
        }

        public IQueryable<Gig> GetUpcomingGigsSearchResult(string query, IQueryable<Gig> upcomingGigs) {
            upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                                                   g.Genre.Name.Contains(query) ||
                                                   g.Venue.Contains(query));
            return upcomingGigs;
        }

        public Gig GetGigWithAttendees(int gigId) {
            return _dbContext.Gigs.Include(g => g.Attendences.Select(a => a.Attendee))
                                  .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigByGigId(int id) {
            return _dbContext.Gigs.Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }

        public void Add(Gig gig) {
            _dbContext.Gigs.Add(gig);
        }

        public void CancelGigWithNotificationToAttendees(Gig gig) {
            gig.Cancel();
        }
    }
}