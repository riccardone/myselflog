namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecurityLinkChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecurityLinks", "Link", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecurityLinks", "Link", c => c.String());
        }
    }
}
