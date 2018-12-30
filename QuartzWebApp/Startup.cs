using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuartzWebApp.Startup))]
namespace QuartzWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
