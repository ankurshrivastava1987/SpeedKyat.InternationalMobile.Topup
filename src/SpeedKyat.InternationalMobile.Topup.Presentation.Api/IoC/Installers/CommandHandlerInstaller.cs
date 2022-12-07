using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Kispanadi.Common.Handlers.Commands;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC.Installers
{
    public class CommandHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes

                // selection
                .FromAssemblyInThisApplication()
                .BasedOn<ICommandHandler>()
                
                // configuration
                .WithServiceFirstInterface()
                .Configure(c => c.LifestyleTransient())
                );
        }
    }
}