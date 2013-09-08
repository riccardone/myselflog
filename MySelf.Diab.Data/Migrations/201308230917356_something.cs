namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class something : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SecurityLinks", "LogProfile_Id", "dbo.LogProfiles");
            DropIndex("dbo.SecurityLinks", new[] { "LogProfile_Id" });
            AddColumn("dbo.LogProfiles", "SecurityLink_Id", c => c.Int());
            AddForeignKey("dbo.LogProfiles", "SecurityLink_Id", "dbo.SecurityLinks", "Id");
            CreateIndex("dbo.LogProfiles", "SecurityLink_Id");
            DropColumn("dbo.SecurityLinks", "LogProfile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecurityLinks", "LogProfile_Id", c => c.Int());
            DropIndex("dbo.LogProfiles", new[] { "SecurityLink_Id" });
            DropForeignKey("dbo.LogProfiles", "SecurityLink_Id", "dbo.SecurityLinks");
            DropColumn("dbo.LogProfiles", "SecurityLink_Id");
            CreateIndex("dbo.SecurityLinks", "LogProfile_Id");
            AddForeignKey("dbo.SecurityLinks", "LogProfile_Id", "dbo.LogProfiles", "Id");
        }
    }
}
