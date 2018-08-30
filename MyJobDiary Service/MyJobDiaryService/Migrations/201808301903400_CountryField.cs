namespace MyJobDiaryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shifts", "Country");
        }
    }
}
