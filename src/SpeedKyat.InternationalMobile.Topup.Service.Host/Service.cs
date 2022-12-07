using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Kispanadi.Common;
using Kispanadi.Common.Castle;
using Kispanadi.Common.Castle.Services;
using Kispanadi.International.MobileTopup.Interface.Common;
using SpeedKyat.InternationalMobile.Topup.Application;
using SpeedKyat.InternationalMobile.Topup.Application.Impl;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Repositories;
using SpeedKyat.InternationalMobile.Topup.Interface.Common;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Service;

namespace SpeedKyat.InternationalMobile.Topup.Service.Host
{
    public partial class Service : BaseService<Service>
    {
        public static void Main()
        {
            BootstrapServiceContainer();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Debug("Service starting.");
            Logger.Debug("Service started.");
        }

        protected override void OnStop()
        {
            Logger.Debug("Service stopping.");
            Logger.Debug("Service stopped.");
        }

        protected override void Init(BaseService<Service> service, WindsorContainer c)
        {
            try
            {
                using (var container = PopulateContainer(c, null))
                {
                    if (!WindsorContainerValidator.IsValid(container, Logger))
                    {
                        Logger.Error("Container not Valid");
                        if (Environment.UserInteractive)
                        {
                            Console.ReadKey(true);
                        }
                        return;
                    }
                    if (Environment.UserInteractive)
                    {
                        ((Service) service).OnStart(null);

                        Console.WriteLine("SpeedKyat.International.MobileTopup.Service.Host is running.");
                        Console.WriteLine("Press <ENTER> to terminate.");
                        Console.ReadKey(true);

                        ((Service) service).OnStop();
                    }
                    else
                        Run(service);
                }
            }
            catch (AddressAlreadyInUseException aaiue)
            {
                Logger.Error(aaiue.Message);
                if (aaiue.InnerException != null) Logger.Error(aaiue.InnerException.Message);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        protected override WindsorContainer PopulateContainer(WindsorContainer container, IParameter parameter)
        {
            var applicationServiceBase = AppConfig.GetAppSetting<string>("ApplicationServiceBase");
            var messagingServiceBase = AppConfig.GetAppSetting<string>("MessagingServiceBase");
            var countryId = AppConfig.GetAppSetting<string>("CountryId");

            container.AddFacility<WcfFacility>();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(Component.For<IServiceBehavior>().Instance(new ServiceDebugBehavior
            {
                IncludeExceptionDetailInFaults = true
            }));

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

            container.Register(Component.For<IParameterInspector>().ImplementedBy<ServiceParameterInspector>().LifestyleSingleton());
            container.Register(Component.For<IErrorHandler>().ImplementedBy<ServiceErrorHandler>().LifestyleSingleton());

            container.Register(Component.For<ICustomerRepository>().ImplementedBy<CustomerRepository>());
            container.Register(Component.For<ITopupTransactionRepository>().ImplementedBy<TopupTransactionRepository>());
            container.Register(Component.For<ICurrencyRepository>().ImplementedBy<CurrencyDetailRepository>());
            container.Register(Component.For<IDenominationRepository>().ImplementedBy<DenominationRepository>());

            container.Register(Component.For<IGlobalMobileTopupService>().ImplementedBy<GlobalMobileTopupService>().LifestyleTransient().DependsOn(new { countryId }));
            container.Register(Component.For<ITopupManagementService>().ImplementedBy<TopupManagementService>().LifestyleTransient());

            container.Register(Component.For<IGlobalMobileTopupServiceFacade>()
                .ImplementedBy<GlobalMobileTopupServiceFacade>()
                .AsWcfService(
                    new DefaultServiceModel()
                        .AddEndpoints(
                            WcfEndpoint.BoundTo(binding)
                                .At(string.Format("net.tcp://{0}/SpeedKyat.International.MobileTopup.Service", applicationServiceBase))
                        )).LifestyleTransient());

            container.Register(Component.For<ITopupManagementServiceFacade>()
                .ImplementedBy<TopupManagementServiceFacade>()
                .AsWcfService(
                    new DefaultServiceModel()
                        .AddEndpoints(
                            WcfEndpoint.BoundTo(binding)
                                .At(string.Format("net.tcp://{0}/SpeedKyat.International.MobileTopup.ManagementService", applicationServiceBase))
                        )).LifestyleTransient());

            container.Register(Component.For<IGlobalTopupServiceFacade>()
                .AsWcfClient(
                    new DefaultClientModel(
                        WcfEndpoint.BoundTo(
                            binding)
                            .At(string.Format("net.tcp://{0}/Kispanadi.International.MobileTopup.TopupService", messagingServiceBase)))
                ));

           return container;
        }
    }
}