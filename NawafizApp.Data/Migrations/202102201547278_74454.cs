namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _74454 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "HotelBlock_Id", "dbo.HotelBlocks");
            DropIndex("dbo.Rooms", new[] { "HotelBlock_Id" });
            AlterColumn("dbo.Rooms", "HotelBlock_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Rooms", "HotelBlock_Id");
            AddForeignKey("dbo.Rooms", "HotelBlock_Id", "dbo.HotelBlocks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "HotelBlock_Id", "dbo.HotelBlocks");
            DropIndex("dbo.Rooms", new[] { "HotelBlock_Id" });
            AlterColumn("dbo.Rooms", "HotelBlock_Id", c => c.Int());
            CreateIndex("dbo.Rooms", "HotelBlock_Id");
            AddForeignKey("dbo.Rooms", "HotelBlock_Id", "dbo.HotelBlocks", "Id");
        }
    }
}
