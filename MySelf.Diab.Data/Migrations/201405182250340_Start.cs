namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        ActivityDate = c.DateTime(nullable: false),
                        Friend_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friends", t => t.Friend_Id)
                .Index(t => t.Friend_Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Country = c.String(),
                        PostalCode = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LogProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        GlobalId = c.Guid(nullable: false),
                        Person_Id = c.Int(),
                        SecurityLink_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .ForeignKey("dbo.SecurityLinks", t => t.SecurityLink_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.SecurityLink_Id);
            
            CreateTable(
                "dbo.GlucoseLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Value = c.Int(nullable: false),
                        LogDate = c.DateTime(nullable: false),
                        GlobalId = c.Guid(nullable: false),
                        LogProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogProfiles", t => t.LogProfile_Id)
                .Index(t => t.LogProfile_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        Country = c.String(maxLength: 255),
                        PostalCode = c.String(maxLength: 50),
                        DateOfBirth = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecurityLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Link = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.LogProfileFriends",
                c => new
                    {
                        LogProfile_Id = c.Int(nullable: false),
                        Friend_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LogProfile_Id, t.Friend_Id })
                .ForeignKey("dbo.LogProfiles", t => t.LogProfile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Friends", t => t.Friend_Id, cascadeDelete: true)
                .Index(t => t.LogProfile_Id)
                .Index(t => t.Friend_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Terapies", "LogProfile_Id", "dbo.LogProfiles");
            DropForeignKey("dbo.LogProfiles", "SecurityLink_Id", "dbo.SecurityLinks");
            DropForeignKey("dbo.LogProfiles", "Person_Id", "dbo.People");
            DropForeignKey("dbo.GlucoseLevels", "LogProfile_Id", "dbo.LogProfiles");
            DropForeignKey("dbo.LogProfileFriends", "Friend_Id", "dbo.Friends");
            DropForeignKey("dbo.LogProfileFriends", "LogProfile_Id", "dbo.LogProfiles");
            DropForeignKey("dbo.FriendActivities", "Friend_Id", "dbo.Friends");
            DropIndex("dbo.LogProfileFriends", new[] { "Friend_Id" });
            DropIndex("dbo.LogProfileFriends", new[] { "LogProfile_Id" });
            DropIndex("dbo.Terapies", new[] { "LogProfile_Id" });
            DropIndex("dbo.GlucoseLevels", new[] { "LogProfile_Id" });
            DropIndex("dbo.LogProfiles", new[] { "SecurityLink_Id" });
            DropIndex("dbo.LogProfiles", new[] { "Person_Id" });
            DropIndex("dbo.FriendActivities", new[] { "Friend_Id" });
            DropTable("dbo.LogProfileFriends");
            DropTable("dbo.Terapies");
            DropTable("dbo.SecurityLinks");
            DropTable("dbo.People");
            DropTable("dbo.GlucoseLevels");
            DropTable("dbo.LogProfiles");
            DropTable("dbo.Friends");
            DropTable("dbo.FriendActivities");
        }
    }
}
