using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Repositories
{
    public class TopupTransactionRepository : BaseRepository<TopupTransaction>,ITopupTransactionRepository
    {
        public async Task<TopupTransaction> GetTopupTransactionByOkTransactionId(string okTransactionId)
        {
            try
            {
                using (var db = new InternationTopupEntities())
                {
                    return
                        await
                            db.Set<TopupTransaction>()
                                .Include(x => x.Customer)
                                .Include(x => x.CurrencyDetail)
                                .Where(x => x.OkTransactionId == okTransactionId)
                                .FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public async Task<List<TopupTransaction>> GetAllTopupTransactions(DateTime? startDate, DateTime? endDate, string okAccountNumber, string operatorName)
        {
            try
            {
                using (var db = new InternationTopupEntities())
                {
                    db.Database.CommandTimeout = 1500;
                    return await db.Set<TopupTransaction>().Where(
                            x =>(!string.IsNullOrEmpty(okAccountNumber) ? x.Customer.OkAccountNumber == okAccountNumber : !string.IsNullOrEmpty(x.Customer.OkAccountNumber)) &&
                                (!string.IsNullOrEmpty(operatorName) ? x.OperatorName == operatorName : !string.IsNullOrEmpty(x.OperatorName)) &&
                                (startDate != null ? (x.CreatedDate.Day >= startDate.Value.Day && x.CreatedDate.Month >= startDate.Value.Month && x.CreatedDate.Year >= startDate.Value.Year) : x.CreatedDate != null) &&
                                (endDate != null ? (x.CreatedDate.Day <= endDate.Value.Day && x.CreatedDate.Month <= endDate.Value.Month && x.CreatedDate.Year <= endDate.Value.Year) : x.CreatedDate != null))
                        .OrderByDescending(x => x.CreatedDate)
                        .Include(x => x.Customer)
                        .Include(x => x.CurrencyDetail)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}