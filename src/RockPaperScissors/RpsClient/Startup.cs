using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RpsClient.Startup))]
namespace RpsClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
