using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Favorit.es.Startup))]
namespace Favorit.es
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
