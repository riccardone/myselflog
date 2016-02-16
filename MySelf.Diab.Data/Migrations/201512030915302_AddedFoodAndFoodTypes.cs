namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFoodAndFoodTypes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LogProfileFriends", newName: "FriendLogProfiles");
            DropPrimaryKey("dbo.FriendLogProfiles");
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Calories = c.Int(nullable: false),
                        LogDate = c.DateTime(nullable: false),
                        GlobalId = c.Guid(nullable: false),
                        FoodType_Id = c.Int(),
                        LogProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodTypes", t => t.FoodType_Id)
                .ForeignKey("dbo.LogProfiles", t => t.LogProfile_Id)
                .Index(t => t.FoodType_Id)
                .Index(t => t.LogProfile_Id);
            
            CreateTable(
                "dbo.FoodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageIdentifier = c.String(),
                        GlobalId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddPrimaryKey("dbo.FriendLogProfiles", new[] { "Friend_Id", "LogProfile_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foods", "LogProfile_Id", "dbo.LogProfiles");
            DropForeignKey("dbo.Foods", "FoodType_Id", "dbo.FoodTypes");
            DropIndex("dbo.Foods", new[] { "LogProfile_Id" });
            DropIndex("dbo.Foods", new[] { "FoodType_Id" });
            DropPrimaryKey("dbo.FriendLogProfiles");
            DropTable("dbo.FoodTypes");
            DropTable("dbo.Foods");
            AddPrimaryKey("dbo.FriendLogProfiles", new[] { "LogProfile_Id", "Friend_Id" });
            RenameTable(name: "dbo.FriendLogProfiles", newName: "LogProfileFriends");
        }
    }
}
