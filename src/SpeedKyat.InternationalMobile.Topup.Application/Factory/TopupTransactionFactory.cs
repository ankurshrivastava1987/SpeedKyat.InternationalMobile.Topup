using System;
using SpeedKyat.InternationalMobile.Topup.Domain;

namespace SpeedKyat.InternationalMobile.Topup.Application.Factory
{
    public class TopupTransactionFactory
    {
        public TopupTransaction CreateNewTopupTransaction(Guid customerId, string countryCode, string operatorName, string topupNumber, double amount, double amountInMmk, Guid currencyDetailId, string reference1, string reference2)
        {
            return new TopupTransaction(Guid.NewGuid(), customerId, countryCode, operatorName, topupNumber, amount, amountInMmk , DateTime.Now, "Created", string.Empty, string.Empty, string.Empty, string.Empty, Guid.Empty , currencyDetailId , reference1, reference2);
        }
    }
}