using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.FlashMessage.MvcExample.Startup))]
namespace Web.FlashMessage.MvcExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
