using Kispanadi.Common.Ddd.Interface;
using Kispanadi.Common.Ddd.Objects;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Service.Mapper
{
    public interface IMapper<in TDomainObject, out TDtoObject> 
        where TDtoObject : IDto where TDomainObject : IDomainObject
    { 
        TDtoObject ToDto(TDomainObject domainObject); 

    }
}