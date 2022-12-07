using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Internal;
using Kispanadi.Common;
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
    public class CountryCommandHandler : BaseLogger, IAsyncActionResultCommandHandler<CountryCommandHandlerParameter, IHttpActionResult>
    {
        private readonly ITopupManagementServiceFacade _topupReportService;

        public CountryCommandHandler(ITopupManagementServiceFacade topupReportService)
        {
            _topupReportService = topupReportService;
        }

        public async Task<IHttpActionResult> ExecuteAsync(CountryCommandHandlerParameter parameter)
        {
            try
            {
                var versions = AppConfig.GetAppSetting<string>("BuildVersion");
                var buildVersions = versions.Split(',').ToList();

                if (buildVersions.Any(x => x == parameter.Version.BuildVersion))
                {
                    var message = AppConfig.GetAppSetting<string>("ErrorMessage");
                    return new HttpActionResult<CountryDto>(parameter.Request, HttpStatusCode.BadRequest, new Response<CountryDto>(HttpStatusCode.BadRequest, false, message));
                }

                var response = await _topupReportService.GetAllCountries();
                var result = response.Success ? JsonConvert.DeserializeObject<List<CountryDto>>(response.Result) : null;
                var httpStatusCode = response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

                return new HttpActionResult<CountryDto>(parameter.Request, httpStatusCode, new Response<CountryDto>(httpStatusCode, response.Success, response.Message, result));
            }

            catch (FaultException<SpeedKyatServiceFault> faultException)
            {
                faultException.Detail.Exceptions.ForEach(ex => Logger.Error(ex.Message));
                return new HttpActionResult<CountryDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<CountryDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return new HttpActionResult<CountryDto>(parameter.Request, HttpStatusCode.InternalServerError, new Response<CountryDto>(HttpStatusCode.InternalServerError, false, "We can not process your request at the moment. Please try again later."));
            }
        }
    }

    public class CountryCommandHandlerParameter : BaseCommandHandlerParameter, ICommandParameterObject
    {
        public VersionModel Version { get; protected set; }
        public CountryCommandHandlerParameter(HttpRequestMessage request, VersionModel version) : base(request)
        {
            Version = version;
        }
    }    
}