using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pack4Travel.Startup))]
namespace Pack4Travel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
