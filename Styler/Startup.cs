using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Styler.Startup))]
namespace Styler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
