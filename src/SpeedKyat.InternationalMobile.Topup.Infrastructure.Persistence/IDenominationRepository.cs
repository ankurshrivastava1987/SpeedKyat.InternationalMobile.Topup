using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence
{
    public interface IDenominationRepository : IRepository<Denomination>
    {
        Task<List<Denomination>> GetAllDenominationByOperatorId(Guid operatorId);
    }
}