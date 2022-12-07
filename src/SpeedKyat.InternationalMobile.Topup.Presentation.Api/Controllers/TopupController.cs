using System.Threading.Tasks;
using System.Web.Http;
using Kispanadi.Common.Net.Installer.Infrastructure;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.CommandHandlers;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.Helpers;
using SpeedKyat.InternationalMobile.Topup.Presentation.Api.Models;

namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.Controllers
{
    [Authorize]
    [RoutePrefix(Constants.Routes.Prefixes.Topup)]
    public class TopupController : BaseController
    {
        private readonly IAsyncActionResultCommandHandler<TopupCommandHandlerParameter, IHttpActionResult> _paymentCommandHandler;

        public TopupController(IAsyncActionResultCommandHandler<TopupCommandHandlerParameter, IHttpActionResult> paymentCommandHandler)
        {
            _paymentCommandHandler = paymentCommandHandler;
        }

        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] TopupModel topupModel)
        {
            return await _paymentCommandHandler.ExecuteAsync(new TopupCommandHandlerParameter(Request, topupModel));
        }
    }
}