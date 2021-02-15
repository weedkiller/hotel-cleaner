namespace NawafizApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Sort_field_to_GuideTown_tablse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GuideTown", "Sort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GuideTown", "Sort");
        }
    }
}
