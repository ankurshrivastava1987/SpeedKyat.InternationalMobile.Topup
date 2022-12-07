using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Internal;
using Kispanadi.Common;
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
    public class GetAllCountriesQueryHandler : BaseLogger, IAsyncQueryHandler<GetAllCountriesQueryHandlerParameter, IHttpActionResult>
    {
        private readonly ITopupManagementServiceFacade _topupReportService;

        public GetAllCountriesQueryHandler(ITopupManagementServiceFacade topupReportService)
        {
            _topupReportService = topupReportService;
        }

        public async Task<IHttpActionResult> QueryAsync(GetAllCountriesQueryHandlerParameter parameter)
        {
            try
            {
                var stopOlderApp = AppConfig.GetAppSetting<bool>("StopOlderApp");
                if (stopOlderApp)
                {
                    var message = AppConfig.GetAppSetting<string>("ErrorMessage");
                    return new HttpActionResult<CountryDto>(parameter.Request, HttpStatusCode.BadRequest, new Response<CountryDto>(HttpStatusCode.BadRequest, false, message));
                }

                var response = await _topupReportService.GetAllCountries();
                var result = response.Success ? JsonConvert.DeserializeObject<List<CountryDto>>(response.Result) : null;
                var httpStatusCode = response.Success ? HttpStatusCode.Found : HttpStatusCode.NotFound;

                return new HttpActionResult<CountryDto>(parameter.Request, httpStatusCode, new Response<CountryDto>(httpStatusCode, response.Success, response.Message, result));
            }
            catch (FaultException<SpeedKyatServiceFault> faultException)
            {
                faultException.Detail.Exceptions.ForEach(ex => Logger.Error((string)ex.Message));
                return new HttpActionResult<CountryDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<CountryDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }

            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return new HttpActionResult<CountryDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<CountryDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }
        }
    }

    public class GetAllCountriesQueryHandlerParameter : BaseQueryHandlerParameter, IQueryParameterObject
    {
        public HttpRequestMessage Request { get; protected set; }

        public GetAllCountriesQueryHandlerParameter(HttpRequestMessage request)
        {
            Request = request;
        }
    }
}