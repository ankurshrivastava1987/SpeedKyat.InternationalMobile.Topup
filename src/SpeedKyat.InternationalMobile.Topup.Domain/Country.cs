using System;
using Kispanadi.Common.Ddd.Objects;

namespace SpeedKyat.InternationalMobile.Topup.Domain
{
    public class Country : IDomainObject
    {
        public Guid CountryId { get; protected set; }
        public string CountryName { get; protected set; }
        public string CountryNameBurmese { get; protected set; }
        public string CountryFlag { get; protected set; }
        public bool IsActive { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public string CountryCode { get; protected set; }
        public string CountryCode2 { get; protected set; }
        public CurrencyDetail CurrencyDetail { get; protected set; }

        public Country(Guid countryId, string countryName, string countryNameBurmese, string countryFlag, bool isActive,
            DateTime createdDate, string countryCode, string countryCode2)
        {
            CountryId = countryId;
            CountryName = countryName;
            CountryNameBurmese = countryNameBurmese;
            CountryFlag = countryFlag;
            IsActive = isActive;
            CreatedDate = createdDate;
            CountryCode = countryCode;
            CountryCode2 = countryCode2;
        }

        public void SetCurrencyDetail(CurrencyDetail currency)
        {
            CurrencyDetail = currency;
        }
    }
}