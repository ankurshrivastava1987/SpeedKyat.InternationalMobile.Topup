using System;
using System.Collections.Generic;
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
    public class GetAllOperatorsQueryHandler : BaseLogger, IAsyncQueryHandler<GetAllOperatorsQueryHandlerParameter, IHttpActionResult>
    {
        private readonly ITopupManagementServiceFacade _topupReportService;

        public GetAllOperatorsQueryHandler(ITopupManagementServiceFacade topupReportService)
        {
            _topupReportService = topupReportService;
        }

        public async Task<IHttpActionResult> QueryAsync(GetAllOperatorsQueryHandlerParameter parameter)
        {
            try
            {
                var response = await _topupReportService.GetAllOperatorsByCountryId(parameter.CountryId);
                if (!response.Success) return new FailureResult(parameter.Request, HttpStatusCode.BadRequest, response.Message);

                var result = response.Success ? JsonConvert.DeserializeObject<List<OperatorDto>>(response.Result) : null;
                var httpStatusCode = response.Success ? HttpStatusCode.Found : HttpStatusCode.NotFound;

                return new HttpActionResult<OperatorDto>(parameter.Request, httpStatusCode, new Response<OperatorDto>(httpStatusCode, response.Success, response.Message, result));
            }

            catch (FaultException<SpeedKyatServiceFault> faultException)
            {
                faultException.Detail.Exceptions.ForEach(ex => Logger.Error(ex.Message));
                return new HttpActionResult<OperatorDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<OperatorDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return new HttpActionResult<OperatorDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<OperatorDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }
        }
    }

    public class GetAllOperatorsQueryHandlerParameter : BaseQueryHandlerParameter, IQueryParameterObject
    {
        public HttpRequestMessage Request { get; protected set; }
        public Guid CountryId { get; protected set; }

        public GetAllOperatorsQueryHandlerParameter(HttpRequestMessage request, Guid countryId)
        {
            Request = request;
            CountryId = countryId;
        }
    }
}