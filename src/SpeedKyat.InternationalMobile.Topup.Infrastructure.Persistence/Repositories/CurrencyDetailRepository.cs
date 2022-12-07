using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Repositories
{
    public class CurrencyDetailRepository : BaseRepository<CurrencyDetail>, ICurrencyRepository
    {
        public async Task<List<CurrencyDetail>> GetAllCurrencyDetail()
        {
            try
            {
                using (var db = new InternationTopupEntities())
                {
                    return await db.Set<CurrencyDetail>().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CurrencyDetail> GetCurrencyDetailByCountryName(string countryName)
        {
            try
            {
                using (var db = new InternationTopupEntities())
                {
                    return await db.Set<CurrencyDetail>().Where(x => x.CountryName == countryName).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}