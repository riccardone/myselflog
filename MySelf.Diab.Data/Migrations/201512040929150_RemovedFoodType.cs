namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedFoodType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foods", "FoodType_Id", "dbo.FoodTypes");
            DropIndex("dbo.Foods", new[] { "FoodType_Id" });
            AddColumn("dbo.Foods", "FoodIdentifier", c => c.String());
            DropColumn("dbo.Foods", "FoodType_Id");
            DropTable("dbo.FoodTypes");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Foods", "FoodType_Id", c => c.Int());
            DropColumn("dbo.Foods", "FoodIdentifier");
            CreateIndex("dbo.Foods", "FoodType_Id");
            AddForeignKey("dbo.Foods", "FoodType_Id", "dbo.FoodTypes", "Id");
        }
    }
}
