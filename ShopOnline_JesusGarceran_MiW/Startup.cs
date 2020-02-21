using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopOnline_JesusGarceran_MiW.Startup))]
namespace ShopOnline_JesusGarceran_MiW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
