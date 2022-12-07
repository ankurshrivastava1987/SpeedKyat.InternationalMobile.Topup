using System;
using System.Threading.Tasks;
using Kispanadi.Common.Ddd.Objects;
using SpeedKyat.InternationalMobile.Topup.Domain;

namespace SpeedKyat.InternationalMobile.Topup.Application
{
    public interface IGlobalMobileTopupService
    {
        Task<ServiceResposne<TopupTransaction>> DoTopup(string okAccountNumber, string countryCode, string operatorName, string topupNumber, double topupAmount, double totalAmount, string okTransactionId, string reference1, string reference2);
        Task<ServiceResposne<Country>> GetAllCountries();
        Task<ServiceResposne<Operator>> GetAllOperatorsByCountryId(Guid countryId);
        Task<ServiceResposne<TopupTransaction>> GetTopupTransactionByOkTransactionId(string okTransactionId);
    }
}
