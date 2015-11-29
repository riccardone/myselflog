namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduniqueid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "UniqueId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "UniqueId");
        }
    }
}
