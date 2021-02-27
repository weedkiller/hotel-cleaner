namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomRecs", "Room_Id", "dbo.Rooms");
            AddForeignKey("dbo.RoomRecs", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomRecs", "Room_Id", "dbo.Rooms");
            AddForeignKey("dbo.RoomRecs", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
