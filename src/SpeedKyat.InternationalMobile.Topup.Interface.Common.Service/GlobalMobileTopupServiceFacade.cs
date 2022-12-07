using System;
using System.Threading.Tasks;
using Kispanadi.Common.Ddd.Interface;
using Kispanadi.Common.Wcf.Faults;
using Newtonsoft.Json;
using SpeedKyat.InternationalMobile.Topup.Application;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.CommunictionObjects;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Service.Mapper;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Service
{
    public class GlobalMobileTopupServiceFacade : BaseServiceFacade, IGlobalMobileTopupServiceFacade
    {
        private readonly IGlobalMobileTopupService _globalMobileTopupService;

        public GlobalMobileTopupServiceFacade(IGlobalMobileTopupService globalMobileTopupService)
        {
            _globalMobileTopupService = globalMobileTopupService;
        }

        public async Task<ServiceResponse> DoTopup(string okAccountNumber, string countryCode, string operatorName, string topupNumber, double airTimeAmount, double totalAmount, string okTransactionId, string reference1, string reference2)
        {
            try
            {
                var response = await _globalMobileTopupService.DoTopup(okAccountNumber, countryCode, operatorName, topupNumber, airTimeAmount, totalAmount, okTransactionId, reference1, reference2);
                if (!response.Status) return new ServiceResponse(false, response.Message);

                var result = new TopupTransactionMapper().ToDto(response.Result);
                return new ServiceResponse(true, response.Message, JsonConvert.SerializeObject(result, GetJsonSerializerSettings()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new FaultExceptionBuilder<SpeedKyatServiceFault>().Build(ex);
            }
        }

        public async Task<ServiceResponse> GetTopupTransactionByOkTransactionId(string okTransactionId)
        {
            try
            {
                var response = await _globalMobileTopupService.GetTopupTransactionByOkTransactionId(okTransactionId);
                if (!response.Status) return new ServiceResponse(false, response.Message);

                var result = new TopupTransactionMapper().ToDto(response.Result);
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