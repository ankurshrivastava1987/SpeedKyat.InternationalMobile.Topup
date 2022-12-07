using System;
using System.Linq;
using System.Threading.Tasks;
using Kispanadi.Common.Ddd.Interface;
using Kispanadi.Common.Wcf.Faults;
using Newtonsoft.Json;
using SpeedKyat.InternationalMobile.Topup.Application;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.CommunictionObjects;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Service.Mapper;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Service
{
    public class TopupManagementServiceFacade : BaseServiceFacade, ITopupManagementServiceFacade
    {
        private readonly ITopupManagementService _topupManagementService;
        private readonly IGlobalMobileTopupService _globalMobileTopupService;

        public TopupManagementServiceFacade(ITopupManagementService topupManagementService, IGlobalMobileTopupService globalMobileTopupService)
        {
            _topupManagementService = topupManagementService;
            _globalMobileTopupService = globalMobileTopupService;
        }

        public async Task<ServiceResponse> GetTopupTransaction(DateTime? startDate, DateTime? endDate, string okAccountNumber, string operatorName)
        {
            try
            {
                var response = await _topupManagementService.GetTopupTransaction(startDate, endDate, okAccountNumber, operatorName);
                if (!response.Status) return new ServiceResponse(false, response.Message);

                var result = response.Results.Select(x => new TopupTransactionMapper().ToDto(x)).ToList();
                return new ServiceResponse(true, response.Message, JsonConvert.SerializeObject(result, GetJsonSerializerSettings()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new FaultExceptionBuilder<SpeedKyatServiceFault>().Build(ex);
            }
        }

        public async Task<ServiceResponse> GetAllCountries()
        {
            try
            {
                var response = await _globalMobileTopupService.GetAllCountries();
                if (!response.Status) return new ServiceResponse(false, response.Message);

                var result = response.Results.Select(x => new CountryMapper().ToDto(x)).ToList();
                return new ServiceResponse(true, response.Message, JsonConvert.SerializeObject(result, GetJsonSerializerSettings()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new FaultExceptionBuilder<SpeedKyatServiceFault>().Build(ex);
            }
        }

        public async Task<ServiceResponse> GetAllOperatorsByCountryId(Guid countryId)
        {
            try
            {
                var response = await _globalMobileTopupService.GetAllOperatorsByCountryId(countryId);
                if (!response.Status) return new ServiceResponse(false, response.Message);

                var result = response.Results.Select(x => new OperatorMapper().ToDto(x)).ToList();
                return new ServiceResponse(true, response.Message, JsonConvert.SerializeObject(result, GetJsonSerializerSettings()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new FaultExceptionBuilder<SpeedKyatServiceFault>().Build(ex);
            }
        }

        public async Task<ServiceResponse> GetAllCurrencyDetail()
        {
            try
            {
                var response = await _topupManagementService.GetAllCurrencyDetail();
                if (!response.Status) return new ServiceResponse(false, response.Message);

                var result = response.Results.Select(x => new CurrencyDetailMapper().ToDto(x)).ToList();
                return new ServiceResponse(true, response.Message, JsonConvert.SerializeObject(result, GetJsonSerializerSettings()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new FaultExceptionBuilder<SpeedKyatServiceFault>().Build(ex);
            }
        }

        public async Task<ServiceResponse> GetCurrencyDetailByCountryName(string countryName)
        {
            try
            {
                var response = await _topupManagementService.GetCurrencyDetailByCountryName(countryName);
                if (!response.Status) return new ServiceResponse(false, response.Message);

                var result = new CurrencyDetailMapper().ToDto(response.Result);
                return new ServiceResponse(true, response.Message, JsonConvert.SerializeObject(result, GetJsonSerializerSettings()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new FaultExceptionBuilder<SpeedKyatServiceFault>().Build(ex);
            }
        }

        public async Task<ServiceResponse> UpdateCurrencyDetail(string countryName, string currencyType, double exchangeRate)
        {
            try
            {
                var response = await _topupManagementService.UpdateCurrencyDetail(countryName, currencyType, exchangeRate);
                if (!response.Status) return new ServiceResponse(false, response.Message);

                var result = new CurrencyDetailMapper().ToDto(response.Result);
                return new ServiceResponse(true, response.Message, JsonConvert.SerializeObject(result, GetJsonSerializerSettings()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new FaultExceptionBuilder<SpeedKyatServiceFault>().Build(ex);
            }
        }
    }
}