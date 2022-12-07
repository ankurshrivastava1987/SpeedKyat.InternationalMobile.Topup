using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Kispanadi.Common.Owin.WebApi;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC.Installers
{
    public class KatanaDependencyResolverInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IKatanaDependencyResolver>().ImplementedBy<KatanaDependencyResolver>());
        }
    }
}