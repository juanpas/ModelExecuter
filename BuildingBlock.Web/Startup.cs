using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BuildingBlock.Web.Startup))]
namespace BuildingBlock.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
