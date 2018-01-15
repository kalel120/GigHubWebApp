using GigHubWebApp.Core.Models;
using GigHubWebApp.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHubWebApp.Persistence {

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new GigConfiguration());

            modelBuilder.Configurations.Add(new AppUserConfiguration());

            modelBuilder.Configurations.Add(new UserNotificationConfiguration());

            modelBuilder.Configurations.Add(new AttendancesConfiguration());

            modelBuilder.Configurations.Add(new FollowingConfiguration());

            modelBuilder.Configurations.Add(new GenreConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}