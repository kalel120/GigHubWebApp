using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHubWebApp.Models {

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
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
            modelBuilder.Entity<Attendence>()
                .HasRequired(g => g.Gig)
                .WithMany(a => a.Attendences)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(u => u.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(u => u.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>()
                .HasRequired(u => u.User)
                .WithMany(n => n.UserNotifications)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}