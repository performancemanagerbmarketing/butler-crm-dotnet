using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ButlerDotCom.Startup))]
namespace ButlerDotCom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
