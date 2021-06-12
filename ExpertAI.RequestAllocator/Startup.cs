using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExpertAI.RequestAllocator.Startup))]
namespace ExpertAI.RequestAllocator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
