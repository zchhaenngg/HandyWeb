namespace HandyWork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.hy_auth_permission",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40),
                        code = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 50),
                        description = c.String(maxLength: 500),
                        last_modified_by_id = c.String(maxLength: 40),
                        last_modified_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.hy_auth_role",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40),
                        name = c.String(nullable: false, maxLength: 50),
                        description = c.String(maxLength: 500),
                        created_by_id = c.String(maxLength: 40),
                        created_time = c.DateTime(nullable: false),
                        last_modified_by_id = c.String(maxLength: 40),
                        last_modified_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.hy_user",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40),
                        access_failed_times = c.Int(nullable: false),
                        is_locked = c.Boolean(nullable: false),
                        locked_time = c.DateTime(),
                        password_hash = c.String(nullable: false),
                        security_stamp = c.String(nullable: false),
                        phone_number = c.String(maxLength: 250),
                        phone_number_confirmed = c.Boolean(nullable: false),
                        two_factor_enabled = c.Boolean(nullable: false),
                        email_confirmed = c.Boolean(nullable: false),
                        user_name = c.String(nullable: false, maxLength: 50),
                        nick_name = c.String(nullable: false, maxLength: 50),
                        email = c.String(maxLength: 256),
                        is_valid = c.Boolean(nullable: false),
                        created_by_id = c.String(maxLength: 40),
                        created_time = c.DateTime(nullable: false),
                        last_modified_by_id = c.String(maxLength: 40),
                        last_modified_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.user_name, unique: true, name: "user_name_index")
                .Index(t => t.email, unique: true, name: "email_index");
            
            CreateTable(
                "dbo.hy_configuration",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40),
                        app_key = c.String(maxLength: 50),
                        app_value = c.String(),
                        category = c.String(maxLength: 50),
                        description = c.String(maxLength: 200),
                        last_modified_by_id = c.String(maxLength: 40),
                        last_modified_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.hy_data_history",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40),
                        category_name = c.String(nullable: false, maxLength: 50),
                        entity_name = c.String(maxLength: 50),
                        unique_key = c.String(nullable: false, maxLength: 40),
                        operation = c.String(maxLength: 50),
                        description = c.String(nullable: false),
                        created_by_id = c.String(maxLength: 40),
                        created_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.hy_auth_rolehy_auth_permission",
                c => new
                    {
                        hy_auth_role_id = c.String(nullable: false, maxLength: 40),
                        hy_auth_permission_id = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => new { t.hy_auth_role_id, t.hy_auth_permission_id })
                .ForeignKey("dbo.hy_auth_role", t => t.hy_auth_role_id, cascadeDelete: true)
                .ForeignKey("dbo.hy_auth_permission", t => t.hy_auth_permission_id, cascadeDelete: true)
                .Index(t => t.hy_auth_role_id)
                .Index(t => t.hy_auth_permission_id);
            
            CreateTable(
                "dbo.hy_userhy_auth_permission",
                c => new
                    {
                        hy_user_id = c.String(nullable: false, maxLength: 40),
                        hy_auth_permission_id = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => new { t.hy_user_id, t.hy_auth_permission_id })
                .ForeignKey("dbo.hy_user", t => t.hy_user_id, cascadeDelete: true)
                .ForeignKey("dbo.hy_auth_permission", t => t.hy_auth_permission_id, cascadeDelete: true)
                .Index(t => t.hy_user_id)
                .Index(t => t.hy_auth_permission_id);
            
            CreateTable(
                "dbo.hy_userhy_auth_role",
                c => new
                    {
                        hy_user_id = c.String(nullable: false, maxLength: 40),
                        hy_auth_role_id = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => new { t.hy_user_id, t.hy_auth_role_id })
                .ForeignKey("dbo.hy_user", t => t.hy_user_id, cascadeDelete: true)
                .ForeignKey("dbo.hy_auth_role", t => t.hy_auth_role_id, cascadeDelete: true)
                .Index(t => t.hy_user_id)
                .Index(t => t.hy_auth_role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.hy_userhy_auth_role", "hy_auth_role_id", "dbo.hy_auth_role");
            DropForeignKey("dbo.hy_userhy_auth_role", "hy_user_id", "dbo.hy_user");
            DropForeignKey("dbo.hy_userhy_auth_permission", "hy_auth_permission_id", "dbo.hy_auth_permission");
            DropForeignKey("dbo.hy_userhy_auth_permission", "hy_user_id", "dbo.hy_user");
            DropForeignKey("dbo.hy_auth_rolehy_auth_permission", "hy_auth_permission_id", "dbo.hy_auth_permission");
            DropForeignKey("dbo.hy_auth_rolehy_auth_permission", "hy_auth_role_id", "dbo.hy_auth_role");
            DropIndex("dbo.hy_userhy_auth_role", new[] { "hy_auth_role_id" });
            DropIndex("dbo.hy_userhy_auth_role", new[] { "hy_user_id" });
            DropIndex("dbo.hy_userhy_auth_permission", new[] { "hy_auth_permission_id" });
            DropIndex("dbo.hy_userhy_auth_permission", new[] { "hy_user_id" });
            DropIndex("dbo.hy_auth_rolehy_auth_permission", new[] { "hy_auth_permission_id" });
            DropIndex("dbo.hy_auth_rolehy_auth_permission", new[] { "hy_auth_role_id" });
            DropIndex("dbo.hy_user", "email_index");
            DropIndex("dbo.hy_user", "user_name_index");
            DropTable("dbo.hy_userhy_auth_role");
            DropTable("dbo.hy_userhy_auth_permission");
            DropTable("dbo.hy_auth_rolehy_auth_permission");
            DropTable("dbo.hy_data_history");
            DropTable("dbo.hy_configuration");
            DropTable("dbo.hy_user");
            DropTable("dbo.hy_auth_role");
            DropTable("dbo.hy_auth_permission");
        }
    }
}
