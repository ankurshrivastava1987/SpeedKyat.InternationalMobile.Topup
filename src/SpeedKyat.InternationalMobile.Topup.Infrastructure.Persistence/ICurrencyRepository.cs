using System.Collections.Generic;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence
{
    public interface ICurrencyRepository : IRepository<CurrencyDetail>
    {
        Task<List<CurrencyDetail>> GetAllCurrencyDetail();
        Task<CurrencyDetail> GetCurrencyDetailByCountryName(string countryName);
    }
}