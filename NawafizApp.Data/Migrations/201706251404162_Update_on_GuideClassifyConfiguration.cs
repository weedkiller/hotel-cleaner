namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_on_GuideClassifyConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GuideClassify", "Phone1", c => c.String(maxLength: 15));
            AlterColumn("dbo.GuideClassify", "Phone2", c => c.String(maxLength: 15));
            AlterColumn("dbo.GuideClassify", "Phone3", c => c.String(maxLength: 15));
            AlterColumn("dbo.GuideClassify", "Mobile2", c => c.String(maxLength: 15));
            AlterColumn("dbo.GuideClassify", "Mobile3", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GuideClassify", "Mobile3", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.GuideClassify", "Mobile2", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.GuideClassify", "Phone3", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.GuideClassify", "Phone2", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.GuideClassify", "Phone1", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
