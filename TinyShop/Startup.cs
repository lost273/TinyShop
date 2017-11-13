using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TinyShop.Startup))]
namespace TinyShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
