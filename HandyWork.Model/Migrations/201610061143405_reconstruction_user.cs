namespace HandyWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reconstruction_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.User", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.User", "PasswordHash", c => c.String(nullable: false));
            AddColumn("dbo.User", "SecurityStamp", c => c.String(nullable: false));
            AddColumn("dbo.User", "PhoneNumber", c => c.String(maxLength: 250));
            AddColumn("dbo.User", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "EmailConfirmed", c => c.Boolean(nullable: false));
            CreateIndex("dbo.User", "Email", unique: true, name: "EmailIndex");
            DropColumn("dbo.User", "Password");
            DropColumn("dbo.User", "Phone");
            DropColumn("dbo.User", "IsDomain");
            DropColumn("dbo.User", "IsLocked");
            DropColumn("dbo.User", "LoginFailedCount");
            DropColumn("dbo.User", "LastLoginFailedTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "LastLoginFailedTime", c => c.DateTime());
            AddColumn("dbo.User", "LoginFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.User", "IsLocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "IsDomain", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "Phone", c => c.String(maxLength: 250));
            AddColumn("dbo.User", "Password", c => c.String(maxLength: 50));
            DropIndex("dbo.User", "EmailIndex");
            DropColumn("dbo.User", "EmailConfirmed");
            DropColumn("dbo.User", "TwoFactorEnabled");
            DropColumn("dbo.User", "PhoneNumberConfirmed");
            DropColumn("dbo.User", "PhoneNumber");
            DropColumn("dbo.User", "SecurityStamp");
            DropColumn("dbo.User", "PasswordHash");
            DropColumn("dbo.User", "LockoutEndDateUtc");
            DropColumn("dbo.User", "LockoutEnabled");
            DropColumn("dbo.User", "AccessFailedCount");
        }
    }
}
