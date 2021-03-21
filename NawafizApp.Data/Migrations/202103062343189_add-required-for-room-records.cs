namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredforroomrecords : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RoomRecs", new[] { "Room_Id" });
            AlterColumn("dbo.RoomRecs", "Room_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.RoomRecs", "Room_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RoomRecs", new[] { "Room_Id" });
            AlterColumn("dbo.RoomRecs", "Room_Id", c => c.Int());
            CreateIndex("dbo.RoomRecs", "Room_Id");
        }
    }
}
