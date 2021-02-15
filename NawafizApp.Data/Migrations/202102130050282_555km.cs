namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _555km : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderEqps", "FixOrder_Id", "dbo.FixOrders");
            DropForeignKey("dbo.FixOrders", "User_UserId", "dbo.User");
            DropIndex("dbo.FixOrders", new[] { "User_UserId" });
            DropIndex("dbo.OrderEqps", new[] { "FixOrder_Id" });
            AddColumn("dbo.FixOrders", "Hoster", c => c.Guid());
            AddColumn("dbo.FixOrders", "moshId", c => c.Guid());
            AddColumn("dbo.FixOrders", "maitremp", c => c.Guid());
            AddColumn("dbo.FixOrders", "Creation_At", c => c.String());
            AddColumn("dbo.FixOrders", "isTaked", c => c.Boolean(nullable: false));
            DropColumn("dbo.FixOrders", "OrderNum");
            DropColumn("dbo.FixOrders", "OrderPriority");
            DropColumn("dbo.FixOrders", "User_ID");
            DropColumn("dbo.FixOrders", "User_UserId");
            DropTable("dbo.OrderEqps");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderEqps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        needfix = c.Boolean(nullable: false),
                        ishere = c.Boolean(nullable: false),
                        FixOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FixOrders", "User_UserId", c => c.Guid());
            AddColumn("dbo.FixOrders", "User_ID", c => c.Guid());
            AddColumn("dbo.FixOrders", "OrderPriority", c => c.String());
            AddColumn("dbo.FixOrders", "OrderNum", c => c.String());
            DropColumn("dbo.FixOrders", "isTaked");
            DropColumn("dbo.FixOrders", "Creation_At");
            DropColumn("dbo.FixOrders", "maitremp");
            DropColumn("dbo.FixOrders", "moshId");
            DropColumn("dbo.FixOrders", "Hoster");
            CreateIndex("dbo.OrderEqps", "FixOrder_Id");
            CreateIndex("dbo.FixOrders", "User_UserId");
            AddForeignKey("dbo.FixOrders", "User_UserId", "dbo.User", "UserId");
            AddForeignKey("dbo.OrderEqps", "FixOrder_Id", "dbo.FixOrders", "Id");
        }
    }
}
