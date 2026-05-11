namespace OrnekDevExtremeProjesi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainId = c.Int(nullable: false),
                        Action = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        LogDate = c.DateTime(nullable: false),
                        UsersId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UsersId, cascadeDelete: true)
                .Index(t => t.UsersId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UsersRole", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UsersRole",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.ApprovalProcess",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainId = c.Int(nullable: false),
                        UsersId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        AdminNote = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Main", t => t.MainId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UsersId, cascadeDelete: true)
                .Index(t => t.MainId)
                .Index(t => t.UsersId);
            
            CreateTable(
                "dbo.Main",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainId = c.Int(nullable: false),
                        UsersId = c.Int(nullable: false),
                        NoteText = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Main", t => t.MainId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UsersId, cascadeDelete: true)
                .Index(t => t.MainId)
                .Index(t => t.UsersId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "UsersId", "dbo.Users");
            DropForeignKey("dbo.Notes", "MainId", "dbo.Main");
            DropForeignKey("dbo.ApprovalProcess", "UsersId", "dbo.Users");
            DropForeignKey("dbo.ApprovalProcess", "MainId", "dbo.Main");
            DropForeignKey("dbo.Main", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.ActivityLogs", "UsersId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.UsersRole");
            DropIndex("dbo.Notes", new[] { "UsersId" });
            DropIndex("dbo.Notes", new[] { "MainId" });
            DropIndex("dbo.Main", new[] { "CategoryId" });
            DropIndex("dbo.ApprovalProcess", new[] { "UsersId" });
            DropIndex("dbo.ApprovalProcess", new[] { "MainId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.ActivityLogs", new[] { "UsersId" });
            DropTable("dbo.Notes");
            DropTable("dbo.Category");
            DropTable("dbo.Main");
            DropTable("dbo.ApprovalProcess");
            DropTable("dbo.UsersRole");
            DropTable("dbo.Users");
            DropTable("dbo.ActivityLogs");
        }
    }
}
