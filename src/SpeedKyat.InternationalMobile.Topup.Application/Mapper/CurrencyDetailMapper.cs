using SpeedKyat.InternationalMobile.Topup.Domain;

namespace SpeedKyat.InternationalMobile.Topup.Application.Mapper
{
    public class CurrencyDetailMapper : IMapper<CurrencyDetail, Infrastructure.Persistence.Data.CurrencyDetail>
    {
        public CurrencyDetail ToDomain(Infrastructure.Persistence.Data.CurrencyDetail dbObject)
        {
            return new CurrencyDetail(dbObject.CurrencyDetailId, dbObject.CountryName, dbObject.CurrencyType, dbObject.ExchangeRate, dbObject.CreatedDate);
        }

        public Infrastructure.Persistence.Data.CurrencyDetail ToDbObject(CurrencyDetail domainObject)
        {
            return new Infrastructure.Persistence.Data.CurrencyDetail
            {
                CurrencyDetailId = domainObject.CurrencyDetailId,
                CountryName = domainObject.CountryName,
                CurrencyType = domainObject.CurrencyType,
                ExchangeRate = domainObject.ExchangeRate,
                CreatedDate = domainObject.CreatedDate
            };
        }
    }
}