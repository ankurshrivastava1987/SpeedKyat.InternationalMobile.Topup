using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC.Installers
{
    public class LoggerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.UseLog4Net());
        }
    }
}