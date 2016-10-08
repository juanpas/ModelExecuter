using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ModelExecuter.Web.Startup))]
namespace ModelExecuter.Web
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
