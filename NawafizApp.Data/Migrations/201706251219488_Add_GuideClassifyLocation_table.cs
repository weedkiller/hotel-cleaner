namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_GuideClassifyLocation_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuideClassifyLocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassifyId = c.Int(nullable: false),
                        Gps_Latitude = c.String(maxLength: 20),
                        Gps_Longitude = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GuideClassify", t => t.ClassifyId, cascadeDelete: true)
                .Index(t => t.ClassifyId);
            
            DropColumn("dbo.GuideClassify", "Gps_Latitude");
            DropColumn("dbo.GuideClassify", "Gps_Longitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GuideClassify", "Gps_Longitude", c => c.String(maxLength: 20));
            AddColumn("dbo.GuideClassify", "Gps_Latitude", c => c.String(maxLength: 20));
            DropForeignKey("dbo.GuideClassifyLocation", "ClassifyId", "dbo.GuideClassify");
            DropIndex("dbo.GuideClassifyLocation", new[] { "ClassifyId" });
            DropTable("dbo.GuideClassifyLocation");
        }
    }
}
