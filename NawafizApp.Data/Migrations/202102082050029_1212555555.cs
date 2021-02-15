namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1212555555 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CleanOrders", "Hoster", c => c.Guid());
            AlterColumn("dbo.CleanOrders", "moshId", c => c.Guid());
            AlterColumn("dbo.CleanOrders", "cleaningEmp", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CleanOrders", "cleaningEmp", c => c.Guid(nullable: false));
            AlterColumn("dbo.CleanOrders", "moshId", c => c.Guid(nullable: false));
            AlterColumn("dbo.CleanOrders", "Hoster", c => c.Guid(nullable: false));
        }
    }
}
