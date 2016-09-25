namespace HandyWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAppValue : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AppConfiguration");
            AlterColumn("dbo.AppConfiguration", "Id", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.AppConfiguration", "AppKey", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.AppConfiguration", "Id");
            DropColumn("dbo.AppConfiguration", "AppValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppConfiguration", "AppValue", c => c.String(maxLength: 5000, unicode: false));
            DropPrimaryKey("dbo.AppConfiguration");
            AlterColumn("dbo.AppConfiguration", "AppKey", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.AppConfiguration", "Id", c => c.String(nullable: false, maxLength: 40, unicode: false));
            AddPrimaryKey("dbo.AppConfiguration", "Id");
        }
    }
}
