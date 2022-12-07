using System;
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
    public class OperatorsController : BaseController
    {
        private readonly IAsyncQueryHandler<GetAllOperatorsQueryHandlerParameter, IHttpActionResult> _getAllOperatorQueryHandler;

        public OperatorsController(IAsyncQueryHandler<GetAllOperatorsQueryHandlerParameter, IHttpActionResult> getAllOperatorQueryHandler)
        {
            _getAllOperatorQueryHandler = getAllOperatorQueryHandler;
        }


        [Route(Constants.Routes.Paths.Operator)]
        public async Task<IHttpActionResult> Get(Guid countryId)
        {
            return await _getAllOperatorQueryHandler.QueryAsync(new GetAllOperatorsQueryHandlerParameter(Request, countryId));
        }
    }
}