using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySelf.Web.Startup))]
namespace MySelf.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
