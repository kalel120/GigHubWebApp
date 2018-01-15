using GigHubWebApp.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHubWebApp.Persistence.EntityConfigurations {
    public class AppUserConfiguration : EntityTypeConfiguration<ApplicationUser> {
        public AppUserConfiguration() {
            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            HasMany(u => u.Followers)
                .WithRequired(u => u.Followee)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Followees)
                .WithRequired(u => u.Follower)
                .WillCascadeOnDelete(false);
        }
    }
}