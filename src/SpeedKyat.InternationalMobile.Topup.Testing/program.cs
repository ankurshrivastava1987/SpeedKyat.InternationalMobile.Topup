using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Kispanadi.Common;
using Newtonsoft.Json;
using SpeedKyat.InternationalMobile.Topup.Interface.Common;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.Models;
using SpeedKyat.ServiceProvider.Interface.Common;

namespace SpeedKyat.InternationalMobile.Topup.Testing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var c = BuildContainer())
            {
                Console.WriteLine("Client is running");

                CallApi();

                //var service = c.Resolve<IGlobalMobileTopupServiceFacade>();
                //var managementService = c.Resolve<ITopupManagementServiceFacade>();


                //var response = managementService.GetTopupTransaction(null, null, string.Empty, string.Empty).Result;
                //var result = JsonConvert.DeserializeObject<List<TopupTransactionDto>>(response.Result);

                //var allcountries = managementService.GetAllCountries().Result;
                //var allOperators = managementService.GetAllOperatorsByCountryId(new Guid("28E32893-FD79-4DB6-981F-BE8750EEBC15")).Result;
                //var allCurrency = managementService.GetAllCurrencyDetail().Result;
                //var currency = managementService.GetCurrencyDetailByCountryName("India").Result;
                //var updateCurrency = managementService.UpdateCurrencyDetail("India", "Rs", 21).Result;

                //var re = service.DoTopup("00959451248993", "+91", "Vodafone", "00919524095250", 1, 1050, "9999999", "789", "456").Result;
                //var re = service.DoTopup("00959451248993", "+86", "China Unicom China", "18313495973", 10, 1000, "9999999", "789", "456").Result;
                //var v = service.GetAllCountries().Result;
                //var r = service.GetAllOperatorsByCountryId(new Guid("28E32893-FD79-4DB6-981F-BE8750EEBC15")).Result;
                //var response = service.DoTopup("00959261834224" ,"+95", "MPT Myanmar", "+959451248993", 500, 500, "123456").Result;
            }
        }

        private static void CallApi()
        {
            var sKey = "5aV5R18f1UiW7hyd5CWNQ7gX9Ox9MYXMbEdKb09rTXk1bWVoSG1FSWlHTUhNNTY4dHNGQzBZR3BYMzRXdzJsVmx3eUFWSnpISTlzamVYSHFLSzRnZks4aGd6Sk5zcldxZTBmYUc1bmoyNzMxcGdLQXdOTWw3RjBheFFlZkFsWmJtQ0ZvMXJqT2hVbmhSRzI5b0piZWZ2c1o5RW0wYUhqUmhjT25iZ0pGbzdEbmJUSnJZZjExSzQ1MVRwb3dFdHhhdlc5ejFPaEhBVjBockY3cTU0YjVRc0duZ1hUdjJXQTVKbjFGVFR6Szd0MHpFWEpPcjcwSlIyS0NQV1RlNHZQblU1WUVOMldtRlI4ZzFSaUlORkU0ZjBuUjExYzNhODk2LTM5NWItNDI1Yi05Y2VkLTNlMGE4ZTgzNWFjZA==";
            var aKey = "xyo3zNJhy9Sf+ZDhJvFOXg==";

            //var keyValues = new List<KeyValuePair<string, object>>
            //{
            //    new KeyValuePair<string, object>("BuildVersion", 50),
            //    new KeyValuePair<string, object>("AppVersion", "1.2.1"),
            //    new KeyValuePair<string, object>("DeviceName", "OPPO CPH1725"),
            //    new KeyValuePair<string, object>("DeviceOs", "7.1.1")
            //};

            //var keyValues = new List<KeyValuePair<string, object>>
            //{
            //    new KeyValuePair<string, object>("OkAccountNumber", "0095451248993"),
            //    new KeyValuePair<string, object>("CountryCode", "+91"),
            //    new KeyValuePair<string, object>("OperatorName", "Vodafone"),
            //    new KeyValuePair<string, object>("TopupNumber", "8072294058"),
            //    new KeyValuePair<string, object>("AirTimeAmount", 5.0),
            //    new KeyValuePair<string, object>("TotalAmount", 105.0),
            //    new KeyValuePair<string, object>("OkTransactionId", "5981491"),
            //    new KeyValuePair<string, object>("Reference1", ""),
            //    new KeyValuePair<string, object>("Reference2", ""),
            //    new KeyValuePair<string, object>("IsBuildversion", false),
            //    new KeyValuePair<string, object>("IsAppVersion", true)
            //};


            //var signatureString = EncryptionManager.ConvertFieldsToSignatureString(keyValues);
            //var hashValue = EncryptionManager.GetHmac(signatureString, sKey);
            //var authenticationCode = EncryptionManager.GetHmac(aKey, sKey);//""72A5725A64D1D2139C109A039DE45265E80E7DC5""

            //var version = new VersionModel("52", "1.2.1", "OPPO CPH1725", "7.1.1", hashValue);
            //var ss = JsonConvert.SerializeObject(version);

            //var topup = new TopupModel("0095451248993", "+91", "Vodafone", "8072294058", 5, 105, "5981491", "", "", hashValue);
            //var toupJson = JsonConvert.SerializeObject(topup);

            var version2 = new VersionModel("52", "1.2.21", "OPPO1 CPH1725", "7.1.1", string.Empty);
            
            var obj = version2;
            var keyList = obj.GetProperties();
            var json = JsonConvert.SerializeObject(obj);
            var keyValues = keyList.Select(keyValuePair => new KeyValuePair<string, object>(keyValuePair.Key, keyValuePair.Value)).ToList();
            keyValues.Remove(new KeyValuePair<string, object>("HashValue", string.Empty));
            var signatureString = EncryptionManager.ConvertFieldsToSignatureString(keyValues);
            var hashValue = EncryptionManager.GetHmac(signatureString, sKey);
            var authenticationCode = EncryptionManager.GetHmac(aKey, sKey);   //A750D11F03A27009C0D41C6FC8D9A3B9D27D8402

            json = json.Replace("\"HashValue\":\"\"", "\"HashValue\": " + "\"" + hashValue + "\"");

        }

        public static IWindsorContainer BuildContainer()
        {
            var applicationServiceBase = AppConfig.GetAppSetting<string>("ApplicationServiceBase");
            var container = new WindsorContainer();
            container.AddFacility<WcfFacility>();
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
            return container;
        }
    }

    public static class ObjectExtensions
    {
        public static List<KeyValuePair<string, object>> GetProperties(this object me)
        {
            List<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            foreach (var property in me.GetType().GetProperties())
            {
                result.Add(new KeyValuePair<string, object>(property.Name, property.GetValue(me)));
            }
            return result;
        }
    }
}

