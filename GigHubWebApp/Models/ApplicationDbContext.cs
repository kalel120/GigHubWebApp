using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHubWebApp.Models {

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendence> Attendences { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Attendence>()
                .HasRequired(a => a.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}