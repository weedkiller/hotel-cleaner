namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12125555cc55 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "IsBusy", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "IsBusy");
        }
    }
}
