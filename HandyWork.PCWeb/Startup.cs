using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HandyWork.PCWeb.Startup))]
namespace HandyWork.PCWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
