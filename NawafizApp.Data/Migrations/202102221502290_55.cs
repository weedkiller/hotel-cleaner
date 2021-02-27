namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _55 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FIxOrderEqupments", "fixOrder_Id", "dbo.FixOrders");
            AddForeignKey("dbo.FIxOrderEqupments", "fixOrder_Id", "dbo.FixOrders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FIxOrderEqupments", "fixOrder_Id", "dbo.FixOrders");
            AddForeignKey("dbo.FIxOrderEqupments", "fixOrder_Id", "dbo.FixOrders", "Id");
        }
    }
}
