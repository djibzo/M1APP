using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(M1APP.Startup))]
namespace M1APP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
