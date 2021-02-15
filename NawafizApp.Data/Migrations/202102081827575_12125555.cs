namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12125555 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Stauts = c.String(),
                        needfix = c.Boolean(nullable: false),
                        ishere = c.Boolean(nullable: false),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNum = c.String(),
                        RoomDirection = c.String(),
                        Isbusy = c.Boolean(nullable: false),
                        IsNeedfix = c.Boolean(nullable: false),
                        isneedclean = c.Boolean(nullable: false),
                        IsInService = c.Boolean(nullable: false),
                        HotelBlock_Id = c.Int(),
                        RoomType_Id = c.Int(),
                        RoomStatus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HotelBlocks", t => t.HotelBlock_Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id)
                .ForeignKey("dbo.RoomStatus", t => t.RoomStatus_Id)
                .Index(t => t.HotelBlock_Id)
                .Index(t => t.RoomType_Id)
                .Index(t => t.RoomStatus_Id);
            
            CreateTable(
                "dbo.FixOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNum = c.String(),
                        OrderPriority = c.String(),
                        Creation_Date = c.String(),
                        Creation_Time = c.String(),
                        Description = c.String(),
                        User_ID = c.Guid(),
                        isFinished = c.Boolean(nullable: false),
                        startdate = c.String(),
                        enddate = c.String(),
                        Room_Id = c.Int(),
                        User_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.Room_Id)
                .Index(t => t.User_UserId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FixOrders", t => t.FixOrder_Id)
                .Index(t => t.FixOrder_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        NationalNum = c.String(maxLength: 4000),
                        Image = c.String(maxLength: 4000),
                        Contract = c.String(maxLength: 4000),
                        Mobile = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 4000),
                        SecurityStamp = c.String(maxLength: 4000),
                        PhoneNumber = c.String(maxLength: 4000),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 256),
                        CreationDate = c.DateTime(nullable: false),
                        PassWordExpired = c.Boolean(),
                        HotelBlock_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.HotelBlocks", t => t.HotelBlock_Id)
                .Index(t => t.HotelBlock_Id);
            
            CreateTable(
                "dbo.Claim",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(maxLength: 4000),
                        ClaimValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.HotelBlocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlockNum = c.String(),
                        BlockName = c.String(),
                        BlockDescription = c.String(),
                        NmberOfTheFloorsIntheBlock = c.String(),
                        NmberOfTheRoomsIntheBlock = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExternalLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifictations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        senderId = c.Guid(nullable: false),
                        RevieverId = c.Guid(nullable: false),
                        NotText = c.String(),
                        NotDateTime = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        Room_ID = c.Int(),
                        Equipment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.RevieverId, cascadeDelete: true)
                .Index(t => t.RevieverId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.CleanOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hoster = c.Guid(nullable: false),
                        moshId = c.Guid(nullable: false),
                        cleaningEmp = c.Guid(nullable: false),
                        Creation_Date = c.String(),
                        Creation_Time = c.String(),
                        Creation_At = c.String(),
                        Description = c.String(),
                        isFinished = c.Boolean(nullable: false),
                        startdate = c.String(),
                        enddate = c.String(),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 2),
                        ArabicName = c.String(nullable: false, maxLength: 100),
                        EnglishName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusNum = c.String(),
                        busy = c.Int(nullable: false),
                        NeedClean = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "RoomStatus_Id", "dbo.RoomStatus");
            DropForeignKey("dbo.Rooms", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.CleanOrders", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.FixOrders", "User_UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Notifictations", "RevieverId", "dbo.User");
            DropForeignKey("dbo.ExternalLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "HotelBlock_Id", "dbo.HotelBlocks");
            DropForeignKey("dbo.Rooms", "HotelBlock_Id", "dbo.HotelBlocks");
            DropForeignKey("dbo.Claim", "UserId", "dbo.User");
            DropForeignKey("dbo.FixOrders", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.OrderEqps", "FixOrder_Id", "dbo.FixOrders");
            DropForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.CleanOrders", new[] { "Room_Id" });
            DropIndex("dbo.Notifictations", new[] { "RevieverId" });
            DropIndex("dbo.ExternalLogin", new[] { "UserId" });
            DropIndex("dbo.Claim", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "HotelBlock_Id" });
            DropIndex("dbo.OrderEqps", new[] { "FixOrder_Id" });
            DropIndex("dbo.FixOrders", new[] { "User_UserId" });
            DropIndex("dbo.FixOrders", new[] { "Room_Id" });
            DropIndex("dbo.Rooms", new[] { "RoomStatus_Id" });
            DropIndex("dbo.Rooms", new[] { "RoomType_Id" });
            DropIndex("dbo.Rooms", new[] { "HotelBlock_Id" });
            DropIndex("dbo.Equipments", new[] { "Room_Id" });
            DropTable("dbo.UserRole");
            DropTable("dbo.RoomStatus");
            DropTable("dbo.Language");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.CleanOrders");
            DropTable("dbo.Role");
            DropTable("dbo.Notifictations");
            DropTable("dbo.ExternalLogin");
            DropTable("dbo.HotelBlocks");
            DropTable("dbo.Claim");
            DropTable("dbo.User");
            DropTable("dbo.OrderEqps");
            DropTable("dbo.FixOrders");
            DropTable("dbo.Rooms");
            DropTable("dbo.Equipments");
        }
    }
}
