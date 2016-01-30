namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredFoodTypesAsList : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Foods", "FoodIdentifier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "FoodIdentifier", c => c.String());
        }
    }
}
