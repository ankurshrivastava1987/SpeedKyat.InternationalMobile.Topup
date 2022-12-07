using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Kispanadi.Common.Wcf.Faults;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.CommunictionObjects;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common
{
    [ServiceContract]
    public interface ITopupManagementServiceFacade
    {
        [OperationContract, FaultContract(typeof (SpeedKyatServiceFault))]
        Task<ServiceResponse> GetTopupTransaction(DateTime? startDate, DateTime? endDate, string okAccountNumber, string operatorName);

        [OperationContract, FaultContract(typeof(SpeedKyatServiceFault))]
        Task<ServiceResponse> GetAllCountries();

        [OperationContract, FaultContract(typeof(SpeedKyatServiceFault))]
        Task<ServiceResponse> GetAllOperatorsByCountryId(Guid countryId);

        [OperationContract, FaultContract(typeof (SpeedKyatServiceFault))]
        Task<ServiceResponse> GetAllCurrencyDetail();

        [OperationContract, FaultContract(typeof (SpeedKyatServiceFault))]
        Task<ServiceResponse> GetCurrencyDetailByCountryName(string countryName);

        [OperationContract, FaultContract(typeof (SpeedKyatServiceFault))]
        Task<ServiceResponse> UpdateCurrencyDetail(string countryName, string currencyType, double exchangeRate);
    }
}