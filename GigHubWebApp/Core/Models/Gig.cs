using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GigHubWebApp.Core.ViewModels;

namespace GigHubWebApp.Core.Models {

    public class Gig {
        public int Id { get; set; }

        public ApplicationUser Artist { get; set; }
        
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }
        
        public string Venue { get; set; }

        public Genre Genre { get; set; }
        
        public byte GenreId { get; set; }

        public bool IsCanceled { get; private set; }

        public ICollection<Attendence> Attendences { get; private set; }

        public Gig() {
            Attendences = new Collection<Attendence>();
        }

        public void Cancel() {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in Attendences.Select(a => a.Attendee)) {
                attendee.Notify(notification);
            }
        }

        public void Update(GigFormViewModel updatedGig) {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            Venue = updatedGig.Venue;
            DateTime = updatedGig.GetDateTime();
            GenreId = updatedGig.Genre;

            foreach (var attendee in Attendences.Select(a => a.Attendee)) {
                attendee.Notify(notification);
            }
        }
    }
}