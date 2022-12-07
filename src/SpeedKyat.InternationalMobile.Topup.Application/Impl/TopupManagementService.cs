using System;
using System.Linq;
using System.Threading.Tasks;
using Kispanadi.Common.Ddd.Objects;
using SpeedKyat.InternationalMobile.Topup.Application.Factory;
using SpeedKyat.InternationalMobile.Topup.Application.Mapper;
using SpeedKyat.InternationalMobile.Topup.Domain;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence;

namespace SpeedKyat.InternationalMobile.Topup.Application.Impl
{
    public class TopupManagementService : ITopupManagementService
    {
        private readonly ITopupTransactionRepository _topupTransactionRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public TopupManagementService(ITopupTransactionRepository topupTransactionRepository,
            ICurrencyRepository currencyRepository)
        {
            _topupTransactionRepository = topupTransactionRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<ServiceResposne<TopupTransaction>> GetTopupTransaction(DateTime? startDate, DateTime? endDate, string okAccountNumber, string operatorName)
        {
            var topupTransactions = await _topupTransactionRepository.GetAllTopupTransactions(startDate, endDate, okAccountNumber, operatorName);
            if (topupTransactions == null) return new ServiceResposne<TopupTransaction>(false, "Invalid transaction Id.");

            var transactions = topupTransactions.Select(x => new TopupTransactionMapper().ToDomain(x)).ToList();
            return new ServiceResposne<TopupTransaction>(true, "Success", transactions);
        }

        public async Task<ServiceResposne<CurrencyDetail>> GetAllCurrencyDetail()
        {
            var allCurrency = await _currencyRepository.GetAllCurrencyDetail();
            if (allCurrency.Count <= 0) return new ServiceResposne<CurrencyDetail>(false, "Currency not found.");

            var currencyResults = allCurrency.Select(x => new CurrencyDetailMapper().ToDomain(x)).ToList();
            return new ServiceResposne<CurrencyDetail>(true, "Success", currencyResults);
        }

        public async Task<ServiceResposne<CurrencyDetail>> GetCurrencyDetailByCountryName(string countryName)
        {
            var currency = await _currencyRepository.GetCurrencyDetailByCountryName(countryName);
            if (currency == null) return new ServiceResposne<CurrencyDetail>(false, "Currency not found.");

            var currencyResult = new CurrencyDetailMapper().ToDomain(currency);
            return new ServiceResposne<CurrencyDetail>(true, "Success", currencyResult);
        }

        public async Task<ServiceResposne<CurrencyDetail>> UpdateCurrencyDetail(string countryName, string currencyType, double exchangeRate)
        {
            var currency = await _currencyRepository.GetCurrencyDetailByCountryName(countryName);
            if (currency != null)
            {
                var currencyResult = new CurrencyDetailMapper().ToDomain(currency);
                currencyResult.SetCurrencyType(currencyType);
                currencyResult.SetExchangeRate(exchangeRate);
                var isUpdate = await _currencyRepository.Update(new CurrencyDetailMapper().ToDbObject(currencyResult));

                return !isUpdate
                    ? new ServiceResposne<CurrencyDetail>(false, "Failed to update.")
                    : new ServiceResposne<CurrencyDetail>(true, "Success", currencyResult);
            }

            var newCurrency = new CurrencyDetailFactory().CreateNewCurrencyDetail(countryName, currencyType, exchangeRate);
            var isAdded = await _currencyRepository.Add(new CurrencyDetailMapper().ToDbObject(newCurrency));

            return !isAdded
                ? new ServiceResposne<CurrencyDetail>(false, "Failed to add.")
                : new ServiceResposne<CurrencyDetail>(true, "Success", newCurrency);
        }
    }
}