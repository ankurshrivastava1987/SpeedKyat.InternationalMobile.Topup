using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Kispanadi.Common;
using Kispanadi.Common.Audit;
using Kispanadi.Common.Wcf.Audit;
using SpeedKyat.InternationalMobile.Topup.Interface.Common;
using SpeedKyat.ServiceProvider.Interface.Common;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.IoC.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<WcfFacility>();

            container.Register(Component.For<IServiceBehavior>().Instance(new ServiceDebugBehavior
            {
                IncludeExceptionDetailInFaults = true,
            }));

            container.Register(Component.For<AuditContextStore>().LifestyleSingleton());
            container.Register(Component.For<IEndpointBehavior>().ImplementedBy<AuditClientEndpointBehaviour>().LifestyleSingleton());

            var applicationServiceBase = AppConfig.GetAppSetting<string>("ApplicationServiceBase");
            var messagingServiceBase = AppConfig.GetAppSetting<string>("MessagingServiceBase");

            var binding = new NetTcpBinding(SecurityMode.None)
            {
                PortSharingEnabled = true,
                MaxBufferSize = Int32.MaxValue,
                MaxReceivedMessageSize = Int32.MaxValue,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = Int32.MaxValue,
                    MaxBytesPerRead = Int32.MaxValue,
                    MaxDepth = Int32.MaxValue,
                    MaxNameTableCharCount = Int32.MaxValue,
                    MaxStringContentLength = Int32.MaxValue
                },
                MaxBufferPoolSize = Int32.MaxValue,
                TransactionFlow = false,
                TransactionProtocol = TransactionProtocol.Default,
                TransferMode = TransferMode.Buffered,
                OpenTimeout = new TimeSpan(0, 10, 0),
                CloseTimeout = new TimeSpan(0, 10, 0),
                SendTimeout = new TimeSpan(0, 10, 0),
                ReceiveTimeout = new TimeSpan(0, 10, 0)
            };

            container.Register(Component.For<IGlobalMobileTopupServiceFacade>()
             .AsWcfClient(
                 new DefaultClientModel(
                     WcfEndpoint.BoundTo(
                         binding)
                         .At(string.Format("net.tcp://{0}/SpeedKyat.International.MobileTopup.Service", applicationServiceBase)))
             ));

            container.Register(Component.For<ITopupManagementServiceFacade>()
             .AsWcfClient(
                 new DefaultClientModel(
                     WcfEndpoint.BoundTo(
                         binding)
                         .At(string.Format("net.tcp://{0}/SpeedKyat.International.MobileTopup.ManagementService", applicationServiceBase)))
             ));

            container.Register(Component.For<IAuthenticationServiceFacade>()
              .AsWcfClient(
                  new DefaultClientModel(
                      WcfEndpoint.BoundTo(
                          binding)
                          .At(string.Format("net.tcp://{0}/SpeedKyat.ServiceProvider.Authentication", messagingServiceBase)))
              ));
        }
    }
}