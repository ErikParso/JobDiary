namespace MyJobDiaryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DietCurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietPaymentItems", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DietPaymentItems", "Currency");
        }
    }
}
