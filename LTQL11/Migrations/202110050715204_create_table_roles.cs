namespace LTQL11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_table_roles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 10),
                        RoleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleID);
            
            AlterColumn("dbo.Account", "Password", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Account", "Password", c => c.String(maxLength: 50, unicode: false));
            DropTable("dbo.Roles");
        }
    }
}
