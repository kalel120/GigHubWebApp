using GigHubWebApp.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHubWebApp.Persistence.EntityConfigurations {
    public class AttendancesConfiguration : EntityTypeConfiguration<Attendance> {
        public AttendancesConfiguration() {
            HasKey(a => new { a.GigId, a.AttendeeId });

            Property(a => a.GigId).HasColumnOrder(1);
            Property(a => a.AttendeeId).HasColumnOrder(2);
        }
    }
}