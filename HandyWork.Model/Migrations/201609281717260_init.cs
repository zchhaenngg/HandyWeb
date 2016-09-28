namespace HandyWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppConfiguration",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40),
                        AppKey = c.String(maxLength: 50),
                        AppValue = c.String(),
                        AppCategory = c.String(maxLength: 50),
                        Keep1 = c.String(maxLength: 500),
                        Keep2 = c.String(maxLength: 500),
                        Keep3 = c.String(maxLength: 500),
                        Keep4 = c.String(maxLength: 500),
                        Keep5 = c.String(maxLength: 500),
                        Keep6 = c.String(maxLength: 500),
                        Keep7 = c.String(maxLength: 500),
                        Keep8 = c.String(maxLength: 500),
                        Keep9 = c.String(maxLength: 500),
                        Keep10 = c.String(maxLength: 500),
                        Keep11 = c.String(maxLength: 500),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthPermission",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40),
                        Code = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        CreatedById = c.String(maxLength: 40),
                        LastModifiedById = c.String(maxLength: 40),
                        CreatedTime = c.DateTime(),
                        LastModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        CreatedById = c.String(maxLength: 40),
                        LastModifiedById = c.String(maxLength: 40),
                        CreatedTime = c.DateTime(),
                        LastModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40),
                        RealName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 250),
                        Email = c.String(maxLength: 256),
                        IsDomain = c.Boolean(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        LoginFailedCount = c.Int(nullable: false),
                        LastLoginFailedTime = c.DateTime(),
                        CreatedById = c.String(maxLength: 40),
                        LastModifiedById = c.String(maxLength: 40),
                        CreatedTime = c.DateTime(),
                        LastModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataHistory",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40),
                        CreatedById = c.String(maxLength: 40),
                        LastModifiedById = c.String(maxLength: 40),
                        CreatedTime = c.DateTime(),
                        LastModifiedTime = c.DateTime(),
                        Category = c.String(nullable: false, maxLength: 50),
                        ForeignId = c.String(nullable: false, maxLength: 40),
                        Description = c.String(),
                        Keep1 = c.String(maxLength: 500),
                        Keep2 = c.String(maxLength: 500),
                        Keep3 = c.String(maxLength: 500),
                        Keep4 = c.String(maxLength: 500),
                        Keep5 = c.String(maxLength: 500),
                        Keep6 = c.String(maxLength: 500),
                        Keep7 = c.String(maxLength: 500),
                        Keep8 = c.String(maxLength: 500),
                        Keep9 = c.String(maxLength: 500),
                        Keep10 = c.String(maxLength: 500),
                        Keep11 = c.String(maxLength: 500),
                        Keep12 = c.String(maxLength: 500),
                        Keep13 = c.String(maxLength: 500),
                        Keep14 = c.String(maxLength: 500),
                        Keep15 = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 40),
                        UserId = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AuthRole", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AuthRolePermission",
                c => new
                    {
                        PermissionId = c.String(nullable: false, maxLength: 40),
                        RoleId = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => new { t.PermissionId, t.RoleId })
                .ForeignKey("dbo.AuthPermission", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.AuthRole", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PermissionId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AuthUserPermission",
                c => new
                    {
                        PermissionId = c.String(nullable: false, maxLength: 40),
                        UserId = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => new { t.PermissionId, t.UserId })
                .ForeignKey("dbo.AuthPermission", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.PermissionId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthUserPermission", "UserId", "dbo.User");
            DropForeignKey("dbo.AuthUserPermission", "PermissionId", "dbo.AuthPermission");
            DropForeignKey("dbo.AuthRolePermission", "RoleId", "dbo.AuthRole");
            DropForeignKey("dbo.AuthRolePermission", "PermissionId", "dbo.AuthPermission");
            DropForeignKey("dbo.AuthUserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.AuthUserRole", "RoleId", "dbo.AuthRole");
            DropIndex("dbo.AuthUserPermission", new[] { "UserId" });
            DropIndex("dbo.AuthUserPermission", new[] { "PermissionId" });
            DropIndex("dbo.AuthRolePermission", new[] { "RoleId" });
            DropIndex("dbo.AuthRolePermission", new[] { "PermissionId" });
            DropIndex("dbo.AuthUserRole", new[] { "UserId" });
            DropIndex("dbo.AuthUserRole", new[] { "RoleId" });
            DropTable("dbo.AuthUserPermission");
            DropTable("dbo.AuthRolePermission");
            DropTable("dbo.AuthUserRole");
            DropTable("dbo.DataHistory");
            DropTable("dbo.User");
            DropTable("dbo.AuthRole");
            DropTable("dbo.AuthPermission");
            DropTable("dbo.AppConfiguration");
        }
    }
}
