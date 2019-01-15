using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP.NetMVC_Tutorial.Startup))]
namespace ASP.NetMVC_Tutorial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
