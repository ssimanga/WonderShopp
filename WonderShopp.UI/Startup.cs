using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WonderShopp.UI.Startup))]
namespace WonderShopp.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
