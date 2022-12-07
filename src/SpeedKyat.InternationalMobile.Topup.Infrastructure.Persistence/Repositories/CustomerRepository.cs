using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>,ICustomerRepository
    {
        public async Task<Customer> GetCustomerByPhoneNumber(string phoneNumber)
        {
            using (var db = new InternationTopupEntities())
            {
                return await db.Set<Customer>().Where(x=>x.OkAccountNumber == phoneNumber)
                    .FirstOrDefaultAsync();
            }
        }
    }
}