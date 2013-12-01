namespace MySelf.Diab.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecurityLinkIndexUnique : DbMigration
    {
        private const string IndexName = "IX_SecurityLinks_Link_Unique";

        public override void Up()
        {
            CreateIndex("SecurityLinks", "Link", true, IndexName);
        }
        
        public override void Down()
        {
            DropIndex("SecurityLinks", IndexName);
        }
    }
}
