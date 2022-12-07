using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Internal;
using Kispanadi.Common.Castle;
using Kispanadi.Common.Handlers.Commands;
using Kispanadi.Common.Net.ActionResults;
using Kispanadi.Common.Net.Installer.Infrastructure;
using Kispanadi.Common.Wcf.Faults;
using Newtonsoft.Json;
using SpeedKyat.InternationalMobile.Topup.Interface.Common;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.Models;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.CommandHandlers
{
    public class TopupCommandHandler : BaseLogger, IAsyncActionResultCommandHandler<TopupCommandHandlerParameter, IHttpActionResult>
    {
        private readonly IGlobalMobileTopupServiceFacade _globalMobileTopupService;

        public TopupCommandHandler(IGlobalMobileTopupServiceFacade globalMobileTopupService)
        {
            _globalMobileTopupService = globalMobileTopupService;
        }

        public async Task<IHttpActionResult> ExecuteAsync(TopupCommandHandlerParameter parameter)
        {
            try
            {
                var topup = parameter.TopupModel;
                var response = await _globalMobileTopupService.DoTopup(topup.OkAccountNumber, topup.CountryCode, topup.OperatorName, topup.TopupNumber, topup.AirTimeAmount, topup.TotalAmount, topup.OkTransactionId, topup.Reference1, topup.Reference2);
                
                var result = response.Success ? JsonConvert.DeserializeObject<TopupTransactionDto>(response.Result) : null;
                var httpStatusCode = response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

                return new HttpActionResult<TopupTransactionDto>(parameter.Request, httpStatusCode, new Response<TopupTransactionDto>(httpStatusCode, response.Success, response.Message, result));
            }

            catch (FaultException<SpeedKyatServiceFault> faultException)
            {
                faultException.Detail.Exceptions.ForEach(ex => Logger.Error(ex.Message));
                return new HttpActionResult<TopupTransactionDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<TopupTransactionDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return new HttpActionResult<TopupTransactionDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<TopupTransactionDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }
        }
    }

    public class TopupCommandHandlerParameter : BaseCommandHandlerParameter, ICommandParameterObject
    {
        public TopupModel TopupModel { get; protected set; }

        public TopupCommandHandlerParameter(HttpRequestMessage request, TopupModel topupModel) : base(request)
        {
            TopupModel = topupModel;
        }
    }    
}