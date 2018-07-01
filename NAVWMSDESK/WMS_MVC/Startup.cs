using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NAVWMSDESK.Startup))]
namespace NAVWMSDESK
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
