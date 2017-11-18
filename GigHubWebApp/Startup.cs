using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigHubWebApp.Startup))]
namespace GigHubWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
