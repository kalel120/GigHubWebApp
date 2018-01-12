using GigHubWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.Repositories {
    public interface IGigsRepositories {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IQueryable<Gig> GetAllUpcomingGigs();
        IQueryable<Gig> GetUpcomingGigsSearchResult(string query, IQueryable<Gig> upcomingGigs);
        Gig GetGigWithAttendees(int gigId);
        Gig GetGigByGigId(int id);
        void Add(Gig gig);
    }
}