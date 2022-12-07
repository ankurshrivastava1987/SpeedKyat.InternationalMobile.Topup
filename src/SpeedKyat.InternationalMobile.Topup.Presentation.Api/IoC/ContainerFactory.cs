using Castle.Windsor;
using Castle.Windsor.Installer;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC
{
    public class ContainerFactory
    {
        public static IWindsorContainer Create()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());
            return container;
        }
    }
}