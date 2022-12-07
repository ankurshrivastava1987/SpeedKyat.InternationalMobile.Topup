using SpeedKyat.InternationalMobile.Topup.Domain;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Service.Mapper
{
    public class CurrencyDetailMapper : IMapper<CurrencyDetail, CurrencyDetailDto>
    {
        public CurrencyDetailDto ToDto(CurrencyDetail domainObject)
        {
            return new CurrencyDetailDto(domainObject.CurrencyDetailId, domainObject.CountryName, domainObject.CurrencyType, domainObject.ExchangeRate, domainObject.CreatedDate);
        }
    }
}