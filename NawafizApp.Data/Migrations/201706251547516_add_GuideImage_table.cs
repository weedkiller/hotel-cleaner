namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_GuideImage_table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Image", "GuideClassify_Id", "dbo.GuideClassify");
            DropForeignKey("dbo.Favorite", "ClassifyId", "dbo.GuideClassify");
            DropIndex("dbo.Image", new[] { "GuideClassify_Id" });
            CreateTable(
                "dbo.GuideFavorite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClassifyId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GuideClassify", t => t.ClassifyId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClassifyId);
            
            CreateTable(
                "dbo.GuideImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassifyId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        IsPrimary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GuideClassify", t => t.ClassifyId, cascadeDelete: true)
                .Index(t => t.ClassifyId);
            
            DropColumn("dbo.Image", "GuideClassify_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Image", "GuideClassify_Id", c => c.Int());
            DropForeignKey("dbo.GuideFavorite", "UserId", "dbo.User");
            DropForeignKey("dbo.GuideImage", "ClassifyId", "dbo.GuideClassify");
            DropForeignKey("dbo.GuideFavorite", "ClassifyId", "dbo.GuideClassify");
            DropIndex("dbo.GuideImage", new[] { "ClassifyId" });
            DropIndex("dbo.GuideFavorite", new[] { "ClassifyId" });
            DropIndex("dbo.GuideFavorite", new[] { "UserId" });
            DropTable("dbo.GuideImage");
            DropTable("dbo.GuideFavorite");
            CreateIndex("dbo.Image", "GuideClassify_Id");
            AddForeignKey("dbo.Favorite", "ClassifyId", "dbo.GuideClassify", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Image", "GuideClassify_Id", "dbo.GuideClassify", "Id");
        }
    }
}
