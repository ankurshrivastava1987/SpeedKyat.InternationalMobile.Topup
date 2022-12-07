using System;
using System.Threading.Tasks;
using Kispanadi.Common.Ddd.Objects;
using SpeedKyat.InternationalMobile.Topup.Domain;

namespace SpeedKyat.InternationalMobile.Topup.Application
{
    public interface ITopupManagementService
    {
        Task<ServiceResposne<TopupTransaction>> GetTopupTransaction(DateTime? startDate, DateTime? endDate, string okAccountNumber, string operatorName);
        Task<ServiceResposne<CurrencyDetail>> GetAllCurrencyDetail();
        Task<ServiceResposne<CurrencyDetail>> GetCurrencyDetailByCountryName(string countryName);
        Task<ServiceResposne<CurrencyDetail>> UpdateCurrencyDetail(string countryName, string currencyType, double exchangeRate);
    }
}