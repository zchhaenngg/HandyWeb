namespace HandyWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.User", "UserName", unique: true, name: "UserNameIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", "UserNameIndex");
        }
    }
}
