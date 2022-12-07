using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                // selection
                .FromThisAssembly()
                .BasedOn<IHttpController>()
                .If(t => t.Name.EndsWith("Controller"))
                
                // configuration
                .Configure(c => c.LifestyleTransient())
            );
        }
    }
}