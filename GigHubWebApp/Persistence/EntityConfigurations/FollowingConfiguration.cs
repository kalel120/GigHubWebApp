using GigHubWebApp.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHubWebApp.Persistence.EntityConfigurations {
    public class FollowingConfiguration : EntityTypeConfiguration<Following> {
        public FollowingConfiguration() {
            HasKey(f => new { f.FollowerId, f.FolloweeId });

            Property(f => f.FollowerId).HasColumnOrder(1);
            Property(f => f.FolloweeId).HasColumnOrder(2);
        }
    }
}