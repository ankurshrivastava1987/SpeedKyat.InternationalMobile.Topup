using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Kispanadi.Common.Net.Installer.Infrastructure;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC.Installers
{
    public class AuditingHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<AuditingMessageHandler>());
        }
    }
}