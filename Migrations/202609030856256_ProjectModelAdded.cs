namespace Portfolio_Management_Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Cost = c.Single(nullable: false),
                        GithubUrl = c.String(),
                        LiveUrl = c.String(),
                        ImageUrl = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "userId", "dbo.Users");
            DropIndex("dbo.Projects", new[] { "userId" });
            DropTable("dbo.Projects");
        }
    }
}
