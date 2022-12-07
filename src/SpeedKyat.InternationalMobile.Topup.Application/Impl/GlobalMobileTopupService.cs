using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kispanadi.Common.Ddd.Objects;
using Kispanadi.International.MobileTopup.Interface.Common;
using Kispanadi.International.MobileTopup.Interface.Common.Dtos;
using Newtonsoft.Json;
using SpeedKyat.InternationalMobile.Topup.Application.Factory;
using SpeedKyat.InternationalMobile.Topup.Application.Mapper;
using SpeedKyat.InternationalMobile.Topup.Domain;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence;

namespace SpeedKyat.InternationalMobile.Topup.Application.Impl
{
    public class GlobalMobileTopupService : IGlobalMobileTopupService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITopupTransactionRepository _topupTransactionRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IDenominationRepository _denominationRepository;
        private readonly IGlobalTopupServiceFacade _globalTopupService;
        private readonly string _countryId;

        public GlobalMobileTopupService(ICustomerRepository customerRepository,
            ITopupTransactionRepository topupTransactionRepository,
            ICurrencyRepository currencyRepository,
            IDenominationRepository denominationRepository,
            IGlobalTopupServiceFacade globalTopupService,
            string countryId)
        {
            _customerRepository = customerRepository;
            _topupTransactionRepository = topupTransactionRepository;
            _currencyRepository = currencyRepository;
            _denominationRepository = denominationRepository;
            _globalTopupService = globalTopupService;
            _countryId = countryId;
        }

        public async Task<ServiceResposne<TopupTransaction>> DoTopup(string okAccountNumber, string countryCode, string operatorName, string topupNumber, double airTimeAmount, double totalAmount, string okTransactionId, string reference1, string reference2)
        {
            var topupTransaction = await _topupTransactionRepository.GetTopupTransactionByOkTransactionId(okTransactionId);
            if (topupTransaction != null) return new ServiceResposne<TopupTransaction>(false, "Already have top-up transaction with this OkTransaction Id.");

            var customer = await GetCustomer(okAccountNumber);
            var countryResponse = await _globalTopupService.GetCountryByCountryCode(countryCode);
            if (!countryResponse.Success) return new ServiceResposne<TopupTransaction>(false, "Invalid country code.");

            var country = JsonConvert.DeserializeObject<CountryDto>(countryResponse.Result);
            var thisCurrency = await _currencyRepository.GetCurrencyDetailByCountryName(country.CountryName);
            if (thisCurrency == null) return new ServiceResposne<TopupTransaction>(false, "Currency not found.");

            var newTopupTransaction = new TopupTransactionFactory().CreateNewTopupTransaction(customer.CustomerId, countryCode, operatorName, topupNumber, airTimeAmount, totalAmount, thisCurrency.CurrencyDetailId, reference1, reference2);
            await _topupTransactionRepository.Add(new TopupTransactionMapper().ToDbObject(newTopupTransaction));
            newTopupTransaction.SetCustomer(customer);
            newTopupTransaction.SetOkTransactionId(okTransactionId);
            newTopupTransaction.SetOkTransactionStatus("Success");

            await _topupTransactionRepository.Update(new TopupTransactionMapper().ToDbObject(newTopupTransaction));

            var topupResponse = await _globalTopupService.Topup(countryCode, operatorName, topupNumber, airTimeAmount, okTransactionId, newTopupTransaction.TopupTransactionId.ToString());
            if (!topupResponse.Success)
            {
                newTopupTransaction.SetErrorMessage(topupResponse.Message);
                await _topupTransactionRepository.Update(new TopupTransactionMapper().ToDbObject(newTopupTransaction));
                return new ServiceResposne<TopupTransaction>(false, topupResponse.Message);
            }

            var topupResult = JsonConvert.DeserializeObject<TransactionDetailDto>(topupResponse.Result);
            newTopupTransaction.SetTopupStatus(topupResult.TopupStatus);
            newTopupTransaction.SetTopupTransactionRefId(topupResult.TransactionDetailId);
            newTopupTransaction.SetErrorMessage(topupResult.ErrorMessage);

            await _topupTransactionRepository.Update(new TopupTransactionMapper().ToDbObject(newTopupTransaction));

            return new ServiceResposne<TopupTransaction>(true, "Success", newTopupTransaction);
        }

        public async Task<ServiceResposne<Country>> GetAllCountries()
        {
            var response = await _globalTopupService.GetAllCountries();
            if (!response.Success) return new ServiceResposne<Country>(false, response.Message);
            
            var result = JsonConvert.DeserializeObject<List<Country>>(response.Result);
            foreach (var country in result)
            {
                var thisCurrency = await _currencyRepository.GetCurrencyDetailByCountryName(country.CountryName);
                if (thisCurrency == null) continue;
                
                country.SetCurrencyDetail(new CurrencyDetailMapper().ToDomain(thisCurrency));
            }
            return new ServiceResposne<Country>(true, "Success", result );
        }

        public async Task<ServiceResposne<Operator>> GetAllOperatorsByCountryId(Guid countryId)
        {
            var response = await _globalTopupService.GetAllOperatorsByCountryId(countryId);
            if (!response.Success) return new ServiceResposne<Operator>(false, response.Message);

            var result = JsonConvert.DeserializeObject<List<Operator>>(response.Result);

            if (string.Equals(countryId.ToString(), _countryId, StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (var @operator in result)
                {
                    var denominationList = await _denominationRepository.GetAllDenominationByOperatorId(@operator.OperatorId);
                    if (denominationList.Count <= 0) continue;

                    foreach (var denomination in denominationList.OrderBy(x => x.Denomination1))
                    {
                        @operator.SetProduct(denomination.Denomination1);
                    }
                }
            }

            return new ServiceResposne<Operator>(true, "Success", result);
        }

        public async Task<ServiceResposne<TopupTransaction>> GetTopupTransactionByOkTransactionId(string okTransactionId)
        {
            var topupTransaction = await _topupTransactionRepository.GetTopupTransactionByOkTransactionId(okTransactionId);
            if (topupTransaction == null) return new ServiceResposne<TopupTransaction>(false, "Transaction not found.");

            var transaction = new TopupTransactionMapper().ToDomain(topupTransaction);
            return new ServiceResposne<TopupTransaction>(true, "Success", transaction);
        }

        protected async Task<Customer> GetCustomer(string okAccountNumber)
        {
            var customer = await _customerRepository.GetCustomerByPhoneNumber(okAccountNumber);
            if (customer != null) return new CustomerMapper().ToDomain(customer);

            var newCustomer = new CustomerFactory().CreateNewCustomer(okAccountNumber);
            await _customerRepository.Add(new CustomerMapper().ToDbObject(newCustomer));
            return newCustomer;
        }
    }
}