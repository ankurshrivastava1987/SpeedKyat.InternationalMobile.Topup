using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Internal;
using Kispanadi.Common.Castle;
using Kispanadi.Common.Handlers.Queries;
using Kispanadi.Common.Net.ActionResults;
using Kispanadi.Common.Net.Installer.Infrastructure;
using Kispanadi.Common.Wcf.Faults;
using Newtonsoft.Json;
using SpeedKyat.InternationalMobile.Topup.Interface.Common;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.QueryHandlers
{
    public class GetTopupTransactionByOkTransactionIdQueryHandler : BaseLogger, IAsyncQueryHandler<GetTopupTransactionByOkTransactionIdQueryHandlerParameter, IHttpActionResult>
    {
        private readonly IGlobalMobileTopupServiceFacade _globalMobileTopupService;

        public GetTopupTransactionByOkTransactionIdQueryHandler(IGlobalMobileTopupServiceFacade globalMobileTopupService)
        {
            _globalMobileTopupService = globalMobileTopupService;
        }

        public async Task<IHttpActionResult> QueryAsync(GetTopupTransactionByOkTransactionIdQueryHandlerParameter parameter)
        {
            try
            {
                var response = await _globalMobileTopupService.GetTopupTransactionByOkTransactionId(parameter.OkTransactionId);
                var result = response.Success ? JsonConvert.DeserializeObject<TopupTransactionDto>(response.Result) : null;
                var httpStatusCode = response.Success ? HttpStatusCode.Found : HttpStatusCode.NotFound;

                return new HttpActionResult<TopupTransactionDto>(parameter.Request, httpStatusCode, new Response<TopupTransactionDto>(httpStatusCode, response.Success, response.Message, result));
            }
            catch (FaultException<SpeedKyatServiceFault> faultException)
            {
                faultException.Detail.Exceptions.ForEach(ex => Logger.Error((string)ex.Message));
                return new HttpActionResult<TopupTransactionDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<TopupTransactionDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }

            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return new HttpActionResult<TopupTransactionDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<TopupTransactionDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }
        }
    }

    public class GetTopupTransactionByOkTransactionIdQueryHandlerParameter : BaseQueryHandlerParameter, IQueryParameterObject
    {
        public HttpRequestMessage Request { get; protected set; }
        public string OkTransactionId { get; protected set; }

        public GetTopupTransactionByOkTransactionIdQueryHandlerParameter(HttpRequestMessage request, string okTransactionId)
        {
            Request = request;
            OkTransactionId = okTransactionId;
        }
    }
}