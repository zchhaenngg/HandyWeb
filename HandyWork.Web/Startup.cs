using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HandyWork.Web.Startup))]
namespace HandyWork.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
