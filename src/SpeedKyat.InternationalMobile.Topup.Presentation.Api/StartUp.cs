using Kispanadi.Common.Authorization;
using Kispanadi.Common.Net.Installer.Hosting;
using Kispanadi.Common.Owin.Audit;
using Kispanadi.Common.Owin.Ssl;
using Kispanadi.Common.Owin.Windsor;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC;

[assembly: OwinStartup(typeof(StartUp))]

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ContainerFactory.Create();

            app.UseWindsorContainer(container);
            app.UseAuthentication(container);
            app.UseSsl();
            app.UseAudit();
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(WebApiConfig.Configure(container));
        }
    }
}