namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1222 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "IsBusy", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "IsBusy", c => c.Boolean(nullable: false));
        }
    }
}
