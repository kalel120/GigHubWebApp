using System;
using System.ComponentModel.DataAnnotations;

namespace GigHubWebApp.Models {
    public class Notification {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public NotificationType Type { get; set; }

        [Required]
        public Gig Gig { get; set; }
    }
}