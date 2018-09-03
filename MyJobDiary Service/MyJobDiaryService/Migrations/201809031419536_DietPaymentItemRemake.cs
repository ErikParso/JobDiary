namespace MyJobDiaryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DietPaymentItemRemake : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietPaymentItems", "Country", c => c.String());
            AddColumn("dbo.DietPaymentItems", "Hours", c => c.Double(nullable: false));
            AddColumn("dbo.DietPaymentItems", "Reward", c => c.Double(nullable: false));
            DropColumn("dbo.DietPaymentItems", "Location");
            DropColumn("dbo.DietPaymentItems", "Time");
            DropColumn("dbo.DietPaymentItems", "Payment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DietPaymentItems", "Payment", c => c.Double(nullable: false));
            AddColumn("dbo.DietPaymentItems", "Time", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.DietPaymentItems", "Location", c => c.String());
            DropColumn("dbo.DietPaymentItems", "Reward");
            DropColumn("dbo.DietPaymentItems", "Hours");
            DropColumn("dbo.DietPaymentItems", "Country");
        }
    }
}
