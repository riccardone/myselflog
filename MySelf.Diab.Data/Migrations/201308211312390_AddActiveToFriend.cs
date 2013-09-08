namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActiveToFriend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "Active");
        }
    }
}
