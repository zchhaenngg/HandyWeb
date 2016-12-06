namespace HandyWork.DAL.Migrations
{
    using Model.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HandyWork.DAL.HyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HandyWork.DAL.HyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.LoginId = "005f249a-2711-4202-8773-351a1686b09b";
            context.hy_users.AddOrUpdate(
              p => p.id,
              new hy_user
              {
                  id = "005f249a-2711-4202-8773-351a1686b09b",
                  access_failed_times = 0,
                  is_locked = false,
                  two_factor_enabled = true,
                  email_confirmed = true,
                  user_name = "admin",
                  nick_name = "超级管理员",
                  email = "13248191050@163.com",
                  is_valid = true,
                  password_hash = "AODJ9Jk+7hJuVPjhWxGIzf25y1UHa30g5IxptN9xKY/qmjwUD4iS9cwjlZ54Y2ewIQ==",
                  security_stamp = Guid.NewGuid().ToString(),
                  created_time = DateTime.UtcNow,
                  last_modified_time = DateTime.UtcNow
              }
            );
            context.SaveChanges();
        }
    }
}
