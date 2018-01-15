namespace GigHubWebApp.Migrations {
    using System.Data.Entity.Migrations;

    public partial class AttendancesSpellBugFix : DbMigration {
        public override void Up() {
            RenameTable(name: "dbo.Attendences", newName: "Attendances");
        }

        public override void Down() {
            RenameTable(name: "dbo.Attendances", newName: "Attendences");
        }
    }
}
