namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NawafizGuide : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuideClassify",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuideId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ClassifyName = c.String(nullable: false, maxLength: 70),
                        Description = c.String(nullable: false, maxLength: 4000),
                        TownId = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 70),
                        Phone1 = c.String(nullable: false, maxLength: 15),
                        Phone2 = c.String(nullable: false, maxLength: 15),
                        Phone3 = c.String(nullable: false, maxLength: 15),
                        Mobile1 = c.String(nullable: false, maxLength: 15),
                        Mobile2 = c.String(nullable: false, maxLength: 15),
                        Mobile3 = c.String(nullable: false, maxLength: 15),
                        Email1 = c.String(maxLength: 70),
                        Email2 = c.String(maxLength: 70),
                        Email3 = c.String(maxLength: 70),
                        Fax1 = c.String(maxLength: 15),
                        Fax2 = c.String(maxLength: 15),
                        Fax3 = c.String(maxLength: 15),
                        Website = c.String(maxLength: 100),
                        Facebook = c.String(maxLength: 100),
                        Twitter = c.String(maxLength: 100),
                        Instagram = c.String(maxLength: 100),
                        Viewscount = c.Int(),
                        Gps_Latitude = c.String(maxLength: 20),
                        Gps_Longitude = c.String(maxLength: 20),
                        Price = c.Decimal(storeType: "money"),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guide", t => t.GuideId, cascadeDelete: true)
                .ForeignKey("dbo.GuideTown", t => t.TownId, cascadeDelete: true)
                .Index(t => t.GuideId)
                .Index(t => t.TownId);
            
            CreateTable(
                "dbo.Guide",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(),
                        Sort = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ImagePath = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guide", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.GuideDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuideId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guide", t => t.GuideId, cascadeDelete: true)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.GuideId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.GuideTown",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        Gps_Latitude = c.String(maxLength: 20),
                        Gps_Longitude = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GuideCity", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.GuideCity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GuideCityDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.GuideCity", t => t.CityId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.GuideTownDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.Int(nullable: false),
                        TownId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.GuideTown", t => t.TownId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.TownId);
            
            AddColumn("dbo.Image", "GuideClassify_Id", c => c.Int());
            CreateIndex("dbo.Image", "GuideClassify_Id");
            AddForeignKey("dbo.Image", "GuideClassify_Id", "dbo.GuideClassify", "Id");
            AddForeignKey("dbo.Favorite", "ClassifyId", "dbo.GuideClassify", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorite", "ClassifyId", "dbo.GuideClassify");
            DropForeignKey("dbo.Image", "GuideClassify_Id", "dbo.GuideClassify");
            DropForeignKey("dbo.GuideClassify", "TownId", "dbo.GuideTown");
            DropForeignKey("dbo.GuideTownDescription", "TownId", "dbo.GuideTown");
            DropForeignKey("dbo.GuideTownDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.GuideTown", "CityId", "dbo.GuideCity");
            DropForeignKey("dbo.GuideCityDescription", "CityId", "dbo.GuideCity");
            DropForeignKey("dbo.GuideCityDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.GuideClassify", "GuideId", "dbo.Guide");
            DropForeignKey("dbo.Guide", "ParentID", "dbo.Guide");
            DropForeignKey("dbo.GuideDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.GuideDescription", "GuideId", "dbo.Guide");
            DropIndex("dbo.Image", new[] { "GuideClassify_Id" });
            DropIndex("dbo.GuideTownDescription", new[] { "TownId" });
            DropIndex("dbo.GuideTownDescription", new[] { "LanguageId" });
            DropIndex("dbo.GuideCityDescription", new[] { "CityId" });
            DropIndex("dbo.GuideCityDescription", new[] { "LanguageId" });
            DropIndex("dbo.GuideTown", new[] { "CityId" });
            DropIndex("dbo.GuideDescription", new[] { "LanguageId" });
            DropIndex("dbo.GuideDescription", new[] { "GuideId" });
            DropIndex("dbo.Guide", new[] { "ParentID" });
            DropIndex("dbo.GuideClassify", new[] { "TownId" });
            DropIndex("dbo.GuideClassify", new[] { "GuideId" });
            DropColumn("dbo.Image", "GuideClassify_Id");
            DropTable("dbo.GuideTownDescription");
            DropTable("dbo.GuideCityDescription");
            DropTable("dbo.GuideCity");
            DropTable("dbo.GuideTown");
            DropTable("dbo.GuideDescription");
            DropTable("dbo.Guide");
            DropTable("dbo.GuideClassify");
        }
    }
}
