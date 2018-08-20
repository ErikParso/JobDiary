namespace MyJobDiaryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class dietsInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "WithDiets", c => c.Boolean(nullable: false));
            AddColumn("dbo.Shifts", "DepartureTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shifts", "ArrivalTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shifts", "ArrivalLocation", c => c.String());
            AddColumn("dbo.Shifts", "DepartureLocation", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Shifts", "DepartureLocation");
            DropColumn("dbo.Shifts", "ArrivalLocation");
            DropColumn("dbo.Shifts", "ArrivalTime");
            DropColumn("dbo.Shifts", "DepartureTime");
            DropColumn("dbo.Shifts", "WithDiets");
        }
    }
}
