using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence
{
    public interface ITopupTransactionRepository : IRepository<TopupTransaction>
    {
        Task<TopupTransaction> GetTopupTransactionByOkTransactionId(string okTransactionId);
        Task<List<TopupTransaction>> GetAllTopupTransactions(DateTime? startDate, DateTime? endDate, string okAccountNumber, string operatorName);
    }
}
