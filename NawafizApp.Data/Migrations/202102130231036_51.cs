namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _51 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FIxOrderEqupments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        fixOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FixOrders", t => t.fixOrder_Id)
                .Index(t => t.fixOrder_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FIxOrderEqupments", "fixOrder_Id", "dbo.FixOrders");
            DropIndex("dbo.FIxOrderEqupments", new[] { "fixOrder_Id" });
            DropTable("dbo.FIxOrderEqupments");
        }
    }
}
