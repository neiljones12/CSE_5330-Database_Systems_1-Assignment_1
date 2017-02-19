using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(assignment1.Startup))]
namespace assignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
