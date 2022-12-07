using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Kispanadi.Common.Handlers.Queries;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC.Installers
{
    public class QueryHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register( Classes
                // selection
                .FromAssemblyInThisApplication()
                .BasedOn<IQueryHandler>()

                // configuration
                .WithServiceFirstInterface()
                .Configure(c => c.LifestyleTransient())
                );
        }
    }
}