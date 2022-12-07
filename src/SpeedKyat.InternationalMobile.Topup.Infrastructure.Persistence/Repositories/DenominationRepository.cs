using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Repositories
{
    public class DenominationRepository : BaseRepository<Denomination>, IDenominationRepository
    {
        public async Task<List<Denomination>> GetAllDenominationByOperatorId(Guid operatorId)
        {
            using (var db = new InternationTopupEntities())
            {
                return await db.Set<Denomination>().Where(x => x.OperatorId == operatorId).ToListAsync();
            }
        }
    }
}