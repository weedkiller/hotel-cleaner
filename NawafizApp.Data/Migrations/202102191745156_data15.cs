namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FixOrders", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.CleanOrders", "Room_Id", "dbo.Rooms");
            AddForeignKey("dbo.FixOrders", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CleanOrders", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CleanOrders", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.FixOrders", "Room_Id", "dbo.Rooms");
            AddForeignKey("dbo.CleanOrders", "Room_Id", "dbo.Rooms", "Id");
            AddForeignKey("dbo.FixOrders", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
