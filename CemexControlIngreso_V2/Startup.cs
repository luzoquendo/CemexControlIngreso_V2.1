using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CemexControlIngreso_V2.Startup))]
namespace CemexControlIngreso_V2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
