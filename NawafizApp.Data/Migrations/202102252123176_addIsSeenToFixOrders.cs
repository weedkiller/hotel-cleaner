namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsSeenToFixOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FixOrders", "IsSeenFromFixer", c => c.Boolean(nullable: false));
            AddColumn("dbo.FixOrders", "IsSeenFromManager", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FixOrders", "IsSeenFromManager");
            DropColumn("dbo.FixOrders", "IsSeenFromFixer");
        }
    }
}
