using SpeedKyat.InternationalMobile.Topup.Domain;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Service.Mapper
{
    public class CountryMapper : IMapper<Country, CountryDto>
    {
        public CountryDto ToDto(Country domainObject)
        {
            CurrencyDetailDto currency = null;
            if (domainObject.CurrencyDetail != null)
            {
                currency = new CurrencyDetailMapper().ToDto(domainObject.CurrencyDetail);
            }
            return new CountryDto(domainObject.CountryId, domainObject.CountryName, domainObject.CountryNameBurmese,
                domainObject.CountryFlag, domainObject.IsActive, domainObject.CreatedDate, domainObject.CountryCode,
                domainObject.CountryCode2, currency);
        }
    }
}