using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogVarellaMotos.Startup))]
namespace BlogVarellaMotos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
