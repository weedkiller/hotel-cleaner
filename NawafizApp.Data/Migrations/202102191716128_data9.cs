namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomRecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Recoed = c.String(),
                        Datetime = c.String(),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomRecs", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.RoomRecs", new[] { "Room_Id" });
            DropTable("dbo.RoomRecs");
        }
    }
}
