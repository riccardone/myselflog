namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTerapy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Terapies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Value = c.Int(nullable: false),
                        IsSlow = c.Boolean(nullable: false),
                        LogDate = c.DateTime(nullable: false),
                        GlobalId = c.Guid(nullable: false),
                        LogProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogProfiles", t => t.LogProfile_Id)
                .Index(t => t.LogProfile_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Terapies", new[] { "LogProfile_Id" });
            DropForeignKey("dbo.Terapies", "LogProfile_Id", "dbo.LogProfiles");
            DropTable("dbo.Terapies");
        }
    }
}
