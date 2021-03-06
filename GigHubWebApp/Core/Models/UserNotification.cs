﻿using System;

namespace GigHubWebApp.Core.Models {
    public class UserNotification {
        public string UserId { get; private set; }
        public int NotificationId { get; private set; }
        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }
        public bool IsRead { get; private set; }

        protected UserNotification() { }

        public UserNotification(ApplicationUser user, Notification notification) {
            //User = user ?? throw new ArgumentNullException("user");
            //Notification = notification ?? throw new ArgumentNullException("notification");

            if (user == null)
                throw new ArgumentNullException("user");

            User = user;

            if (notification == null)
                throw new ArgumentNullException("notification");

            Notification = notification;
        }

        public void Read() {
            IsRead = true;
        }
    }
}