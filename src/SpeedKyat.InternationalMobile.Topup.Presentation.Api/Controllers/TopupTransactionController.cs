using System.Threading.Tasks;
using System.Web.Http;
using Kispanadi.Common.Handlers.Queries;
using Kispanadi.Common.Net.Installer.Infrastructure;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.Helpers;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.QueryHandlers;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.Controllers
{
    [Authorize]
    [RoutePrefix(Constants.Routes.Prefixes.TopupTransaction)]
    public class TopupTransactionController : BaseController
    {
        private readonly IAsyncQueryHandler<GetTopupTransactionByOkTransactionIdQueryHandlerParameter, IHttpActionResult> _getTopupTransactionByTransactionIdQueryHandler;

        public TopupTransactionController(IAsyncQueryHandler<GetTopupTransactionByOkTransactionIdQueryHandlerParameter, IHttpActionResult> getTopupTransactionByTransactionIdQueryHandler)
        {
            _getTopupTransactionByTransactionIdQueryHandler = getTopupTransactionByTransactionIdQueryHandler;
        }

        [Route("")]
        public async Task<IHttpActionResult> Get(string okTransactionId)
        {
            return await _getTopupTransactionByTransactionIdQueryHandler.QueryAsync(new GetTopupTransactionByOkTransactionIdQueryHandlerParameter(Request, okTransactionId));
        }
    }
}