using SpeedKyat.InternationalMobile.Topup.Domain;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Service.Mapper
{
    public class OperatorMapper : IMapper<Operator, OperatorDto>
    {
        public OperatorDto ToDto(Operator domainObject)
        {
            CountryDto country = null;

            if (domainObject.Country != null)
            {
                country = new CountryMapper().ToDto(domainObject.Country);
            }

            return new OperatorDto(domainObject.OperatorId, domainObject.OperatorName, domainObject.OperatorCode,
                domainObject.CountryId, domainObject.OperatorLogo, domainObject.IsActive, domainObject.CreatedDate,
                domainObject.ProductList, country);
        }
    }
}