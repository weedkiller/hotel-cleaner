namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _61251 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "IsFixrequisted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "IsFixrequisted");
        }
    }
}
