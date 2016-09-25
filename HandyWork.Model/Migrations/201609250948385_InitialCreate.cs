namespace HandyWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppConfiguration",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40, unicode: false),
                        AppKey = c.String(maxLength: 50, unicode: false),
                        AppValue = c.String(maxLength: 5000, unicode: false),
                        AppCategory = c.String(maxLength: 50, unicode: false),
                        Keep1 = c.String(maxLength: 500, unicode: false),
                        Keep2 = c.String(maxLength: 500, unicode: false),
                        Keep3 = c.String(maxLength: 500, unicode: false),
                        Keep4 = c.String(maxLength: 500, unicode: false),
                        Keep5 = c.String(maxLength: 500, unicode: false),
                        Keep6 = c.String(maxLength: 500, unicode: false),
                        Keep7 = c.String(maxLength: 500, unicode: false),
                        Keep8 = c.String(maxLength: 500, unicode: false),
                        Keep9 = c.String(maxLength: 500, unicode: false),
                        Keep10 = c.String(maxLength: 500, unicode: false),
                        Keep11 = c.String(maxLength: 500, unicode: false),
                        Description = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthPermission",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40, unicode: false),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 500, unicode: false),
                        CreatedById = c.String(maxLength: 40, unicode: false),
                        LastModifiedById = c.String(maxLength: 40, unicode: false),
                        CreatedTime = c.DateTime(),
                        LastModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 500, unicode: false),
                        CreatedById = c.String(maxLength: 40, unicode: false),
                        LastModifiedById = c.String(maxLength: 40, unicode: false),
                        CreatedTime = c.DateTime(),
                        LastModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40, unicode: false),
                        RealName = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(maxLength: 50, unicode: false),
                        Phone = c.String(maxLength: 250, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        IsDomain = c.Boolean(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        LoginFailedCount = c.Int(nullable: false),
                        LastLoginFailedTime = c.DateTime(),
                        CreatedById = c.String(maxLength: 40, unicode: false),
                        LastModifiedById = c.String(maxLength: 40, unicode: false),
                        CreatedTime = c.DateTime(),
                        LastModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataHistory",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedById = c.String(maxLength: 40, unicode: false),
                        LastModifiedById = c.String(maxLength: 40, unicode: false),
                        CreatedTime = c.DateTime(),
                        LastModifiedTime = c.DateTime(),
                        Category = c.String(nullable: false, maxLength: 50, unicode: false),
                        ForeignId = c.String(nullable: false, maxLength: 40, unicode: false),
                        Description = c.String(maxLength: 5000, unicode: false),
                        Keep1 = c.String(maxLength: 500, unicode: false),
                        Keep2 = c.String(maxLength: 500, unicode: false),
                        Keep3 = c.String(maxLength: 500, unicode: false),
                        Keep4 = c.String(maxLength: 500, unicode: false),
                        Keep5 = c.String(maxLength: 500, unicode: false),
                        Keep6 = c.String(maxLength: 500, unicode: false),
                        Keep7 = c.String(maxLength: 500, unicode: false),
                        Keep8 = c.String(maxLength: 500, unicode: false),
                        Keep9 = c.String(maxLength: 500, unicode: false),
                        Keep10 = c.String(maxLength: 500, unicode: false),
                        Keep11 = c.String(maxLength: 500, unicode: false),
                        Keep12 = c.String(maxLength: 500, unicode: false),
                        Keep13 = c.String(maxLength: 500, unicode: false),
                        Keep14 = c.String(maxLength: 500, unicode: false),
                        Keep15 = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 40, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 40, unicode: false),
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
                        PermissionId = c.String(nullable: false, maxLength: 40, unicode: false),
                        RoleId = c.String(nullable: false, maxLength: 40, unicode: false),
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
                        PermissionId = c.String(nullable: false, maxLength: 40, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 40, unicode: false),
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
