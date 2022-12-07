using System;
using Kispanadi.Common.Ddd.Objects;

namespace SpeedKyat.InternationalMobile.Topup.Domain
{
    public class CurrencyDetail : IDomainObject
    {
        public Guid CurrencyDetailId { get; protected set; }
        public string CountryName { get; protected set; }
        public string CurrencyType { get; protected set; }
        public double ExchangeRate { get; protected set; }
        public DateTime CreatedDate { get; protected set; }

        public CurrencyDetail(Guid currencyDetailId, string countryName, string currencyType, double exchangeRate, DateTime createdDate)
        {
            CurrencyDetailId = currencyDetailId;
            CountryName = countryName;
            CurrencyType = currencyType;
            ExchangeRate = exchangeRate;
            CreatedDate = createdDate;
        }

        public void SetCurrencyType(string currencyType)
        {
            CurrencyType = currencyType;
        }

        public void SetExchangeRate(double exchange)
        {
            ExchangeRate = exchange;
        }
    }
}