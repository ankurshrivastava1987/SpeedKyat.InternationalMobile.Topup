using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByPhoneNumber(string phoneNumber);
    }
}
