using System.Threading.Tasks;
using System.Web.Http;
using Kispanadi.Common.Net.Installer.Infrastructure;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.CommandHandlers;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.Helpers;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.Models;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.Controllers
{
    [Authorize]
    [RoutePrefix(Constants.Routes.Prefixes.Countries)]
    public class CountriesWithVersionController : BaseController
    {
        private readonly IAsyncActionResultCommandHandler<CountryCommandHandlerParameter, IHttpActionResult> _getAllCountriesQueryHandler;

        public CountriesWithVersionController(IAsyncActionResultCommandHandler<CountryCommandHandlerParameter, IHttpActionResult> getAllCountriesQueryHandler)
        {
            _getAllCountriesQueryHandler = getAllCountriesQueryHandler;
        }

        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] VersionModel version)
        {
            return await _getAllCountriesQueryHandler.ExecuteAsync(new CountryCommandHandlerParameter(Request, version));
        }
    }
}