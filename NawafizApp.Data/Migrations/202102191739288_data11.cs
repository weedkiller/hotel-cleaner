namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms");
            AddForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms");
            AddForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
