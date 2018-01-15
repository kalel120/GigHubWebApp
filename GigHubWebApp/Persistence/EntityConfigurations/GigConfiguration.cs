using System.Data.Entity.ModelConfiguration;
using GigHubWebApp.Core.Models;

namespace GigHubWebApp.Persistence.EntityConfigurations {
    public class GigConfiguration : EntityTypeConfiguration<Gig> {
        public GigConfiguration() {
            Property(g => g.ArtistId).IsRequired();

            Property(g => g.GenreId).IsRequired();

            Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(g=>g.Attendences)
                .WithRequired(g=>g.Gig)
                .WillCascadeOnDelete(false);
        }
    }
}