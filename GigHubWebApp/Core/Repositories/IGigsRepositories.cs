using GigHubWebApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.Core.Repositories {
    public interface IGigsRepositories {
        IEnumerable<Gig> GetUpcomingGigsByArtistId(string userId);
        IQueryable<Gig> GetAllUpcomingGigs();
        IQueryable<Gig> GetUpcomingGigsSearchResult(string query, IQueryable<Gig> upcomingGigs);
        Gig GetGigWithAttendees(int gigId);
        Gig GetGigByGigId(int id);
        void Add(Gig gig);
        void CancelGigWithNotificationToAttendees(Gig gig);
    }
}