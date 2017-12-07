using System;
using System.ComponentModel.DataAnnotations;

namespace GigHubWebApp.Models {
    public class Notification {
        public int Id { get; set; }

        public DateTime DateTime { get; private set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }

        public NotificationType Type { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        public Notification() {
        }

        public Notification(NotificationType type, Gig gig) {
            if (gig == null) {
                throw new ArgumentNullException("gig");
            }

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }
    }
}