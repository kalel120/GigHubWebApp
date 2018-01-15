using GigHubWebApp.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHubWebApp.Persistence.EntityConfigurations {
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification> {

        public UserNotificationConfiguration() {
            HasKey(un => new { un.UserId, un.NotificationId });

            Property(un => un.UserId).HasColumnOrder(1);
            Property(un => un.NotificationId).HasColumnOrder(2);

            HasRequired(u => u.User)
                .WithMany(n => n.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}