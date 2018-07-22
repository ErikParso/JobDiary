using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyJobDiaryService.Startup))]

namespace MyJobDiaryService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}