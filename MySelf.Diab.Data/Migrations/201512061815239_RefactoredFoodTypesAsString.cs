namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredFoodTypesAsString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "FoodTypes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Foods", "FoodTypes");
        }
    }
}
