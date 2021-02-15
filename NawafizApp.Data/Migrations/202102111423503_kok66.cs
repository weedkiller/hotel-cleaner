namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kok66 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Isrequisted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Isrequisted");
        }
    }
}
