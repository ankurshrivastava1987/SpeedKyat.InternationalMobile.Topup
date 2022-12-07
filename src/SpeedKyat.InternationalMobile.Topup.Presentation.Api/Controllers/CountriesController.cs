using System.Threading.Tasks;
using System.Web.Http;
using Kispanadi.Common.Handlers.Queries;
using Kispanadi.Common.Net.Installer.Infrastructure;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.Helpers;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.QueryHandlers;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.Controllers
{
    [Authorize]
    [RoutePrefix(Constants.Routes.Prefixes.Country)]
    public class CountriesController : BaseController
    {
        private readonly IAsyncQueryHandler<GetAllCountriesQueryHandlerParameter, IHttpActionResult> _getAllCountriesQueryHandler;

        public CountriesController(IAsyncQueryHandler<GetAllCountriesQueryHandlerParameter, IHttpActionResult> getAllCountriesQueryHandler)
        {
            _getAllCountriesQueryHandler = getAllCountriesQueryHandler;
        }

        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return await _getAllCountriesQueryHandler.QueryAsync(new GetAllCountriesQueryHandlerParameter(Request));
        }
    }
}