namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addshiftstousers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "FromTime", c => c.DateTime());
            AddColumn("dbo.User", "ToTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ToTime");
            DropColumn("dbo.User", "FromTime");
        }
    }
}
