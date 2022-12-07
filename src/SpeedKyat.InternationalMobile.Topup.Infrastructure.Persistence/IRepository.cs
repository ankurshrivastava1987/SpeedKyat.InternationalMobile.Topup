using System.Collections.Generic;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
