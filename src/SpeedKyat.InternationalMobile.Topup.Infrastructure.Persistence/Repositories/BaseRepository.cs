using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : EntityBase
    {
        public async Task<IEnumerable<T>> GetAll()
        {
            using (var db = new InternationTopupEntities())
            {
                return await db.Set<T>().ToListAsync();
            }
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
            using (var db = new InternationTopupEntities())
                {
                    db.Set<T>().Add(entity);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(T entity)
        {
            using (var db = new InternationTopupEntities())
            {
                db.Entry(entity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> Delete(T entity)
        {
            using (var db = new InternationTopupEntities())
            {
                db.Entry(entity).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
                     