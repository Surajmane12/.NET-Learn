namespace Portfolio_Management_Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Desgination", c => c.String());
            AddColumn("dbo.Users", "roleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "roleId");
            AddForeignKey("dbo.Users", "roleId", "dbo.Roles", "Id", cascadeDelete: true);
            DropColumn("dbo.Users", "Desgineation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Desgineation", c => c.String());
            DropForeignKey("dbo.Users", "roleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "roleId" });
            DropColumn("dbo.Users", "roleId");
            DropColumn("dbo.Users", "Desgination");
            DropTable("dbo.Roles");
        }
    }
}
