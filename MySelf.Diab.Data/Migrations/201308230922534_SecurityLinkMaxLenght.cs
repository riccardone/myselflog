namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecurityLinkMaxLenght : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecurityLinks", "Link", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecurityLinks", "Link", c => c.String(nullable: false));
        }
    }
}
