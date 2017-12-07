﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GigHubWebApp.Models {
    public class Notification {
        public int Id { get; set; }

        public DateTime DateTime { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        public NotificationType Type { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        public Notification() {
        }

        private Notification(NotificationType type, Gig gig) {
            if (gig == null) {
                throw new ArgumentNullException("gig");
            }

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }

        public Notification GigCreated(Gig gig) {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig gig, DateTime originalDateTime, string originalVenue) {
            var notification = new Notification(NotificationType.GigUpdated, gig) {
                OriginalDateTime = originalDateTime,
                OriginalVenue = originalVenue
            };

            return notification;
        }

        public static Notification GigCanceled(Gig gig) {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}