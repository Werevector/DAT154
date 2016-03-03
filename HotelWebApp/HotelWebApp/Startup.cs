using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelWebApp.Startup))]
namespace HotelWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
