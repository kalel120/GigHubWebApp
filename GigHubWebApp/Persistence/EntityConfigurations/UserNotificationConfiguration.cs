using GigHubWebApp.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHubWebApp.Persistence.EntityConfigurations {
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification> {

        public UserNotificationConfiguration() {
            HasRequired(u => u.User)
                .WithMany(n => n.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}