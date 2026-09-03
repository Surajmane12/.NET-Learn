namespace Portfolio_Management_Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TechColumnAdd2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Technology", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Technology");
        }
    }
}
