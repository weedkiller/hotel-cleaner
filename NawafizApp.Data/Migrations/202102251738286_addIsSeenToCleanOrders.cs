namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsSeenToCleanOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CleanOrders", "IsSeenFromCleaner", c => c.Boolean(nullable: false));
            AddColumn("dbo.CleanOrders", "IsSeenFromManager", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CleanOrders", "IsSeenFromManager");
            DropColumn("dbo.CleanOrders", "IsSeenFromCleaner");
        }
    }
}
