using System.ServiceModel;
using System.Threading.Tasks;
using Kispanadi.Common.Wcf.Faults;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.CommunictionObjects;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common
{
    [ServiceContract]
    public interface IGlobalMobileTopupServiceFacade
    {
        [OperationContract, FaultContract(typeof (SpeedKyatServiceFault))]
        Task<ServiceResponse> DoTopup(string okAccountNumber, string countryCode, string operatorName, string topupNumber, double airTimeAmount, double totalAmount, string okTransactionId, string reference1, string reference2);

        [OperationContract, FaultContract(typeof(SpeedKyatServiceFault))]
        Task<ServiceResponse> GetTopupTransactionByOkTransactionId(string okTransactionId);
    }
}