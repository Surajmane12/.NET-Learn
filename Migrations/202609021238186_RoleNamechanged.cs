namespace Portfolio_Management_Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleNamechanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "Name", c => c.Int(nullable: false));
        }
    }
}
