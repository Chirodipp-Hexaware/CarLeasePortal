using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CarLeasePPT.Startup))]
namespace CarLeasePPT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
