using System;
using SpeedKyat.InternationalMobile.Topup.Domain;

namespace SpeedKyat.InternationalMobile.Topup.Application.Factory
{
    public class CurrencyDetailFactory
    {
        public CurrencyDetail CreateNewCurrencyDetail(string countryName, string currencyType, double exchangeRate)
        {
            return new CurrencyDetail(Guid.NewGuid(), countryName, currencyType, exchangeRate, DateTime.Now);
        }
    }
}