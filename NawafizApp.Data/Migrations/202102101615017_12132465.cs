namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12132465 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CleanOrders", "isTaked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CleanOrders", "isTaked");
        }
    }
}
