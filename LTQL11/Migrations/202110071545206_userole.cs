namespace LTQL11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userole : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Account");
            AddColumn("dbo.Account", "UserName", c => c.String());
            AddColumn("dbo.Account", "RoleID", c => c.String(maxLength: 10));
            AlterColumn("dbo.Account", "UseName", c => c.String());
            AddPrimaryKey("dbo.Account", "Usename");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Account");
            AlterColumn("dbo.Account", "UseName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.Account", "RoleID");
            DropColumn("dbo.Account", "UserName");
            AddPrimaryKey("dbo.Account", "Usename");
        }
    }
}
