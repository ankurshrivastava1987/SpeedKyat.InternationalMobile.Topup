using Kispanadi.Common.Ddd.Objects;
using SpeedKyat.InternationalMobile.Topup.Infrastructure.Persistence.Data;

namespace SpeedKyat.InternationalMobile.Topup.Application.Mapper
{
    public interface IMapper<TDomainObject, TDbObject> 
        where TDomainObject : IDomainObject
        where TDbObject : EntityBase
    { 
        TDomainObject ToDomain(TDbObject dbObject); 
        TDbObject ToDbObject(TDomainObject domainObject);
    }
}