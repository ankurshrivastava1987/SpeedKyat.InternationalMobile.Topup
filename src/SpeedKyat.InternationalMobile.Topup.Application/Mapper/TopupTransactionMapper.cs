using SpeedKyat.InternationalMobile.Topup.Domain;

namespace SpeedKyat.InternationalMobile.Topup.Application.Mapper
{
    public class TopupTransactionMapper : IMapper<TopupTransaction, Infrastructure.Persistence.Data.TopupTransaction>
    {
        public TopupTransaction ToDomain(Infrastructure.Persistence.Data.TopupTransaction dbObject)
        {
            var topupTransaction = new TopupTransaction(dbObject.TopupTransactionId, dbObject.CustomerId, dbObject.CountryCode,
                dbObject.OperatorName, dbObject.TopupNumber, dbObject.Amount, dbObject.AmountInMMK, dbObject.CreatedDate,
                dbObject.TopupStatus, dbObject.ErrorMessage, dbObject.OkTransactionId,
                dbObject.OkTransactionStatus, dbObject.DestinationNumber, dbObject.TopupTransactionReferenceId.GetValueOrDefault(), dbObject.CurrencyDetailId, dbObject.Reference1, dbObject.Reference2);
            
            if (dbObject.CurrencyDetail != null)
            {
                topupTransaction.SetCurrencyDetail(new CurrencyDetailMapper().ToDomain(dbObject.CurrencyDetail));
            }

            if (dbObject.Customer != null)
            {
                topupTransaction.SetCustomer(new CustomerMapper().ToDomain(dbObject.Customer));
            }

            return topupTransaction;

        }

        public Infrastructure.Persistence.Data.TopupTransaction ToDbObject(TopupTransaction domainObject)
        {
            return new Infrastructure.Persistence.Data.TopupTransaction
            {
                TopupTransactionId = domainObject.TopupTransactionId,
                CustomerId = domainObject.CustomerId,
                CountryCode = domainObject.CountryCode,
                OperatorName = domainObject.OperatorName,
                TopupNumber = domainObject.TopupNumber,
                Amount = domainObject.Amount,
                AmountInMMK = domainObject.AmountInMmk,
                CreatedDate = domainObject.CreatedDate,
                TopupStatus = domainObject.TopupStatus,
                ErrorMessage = domainObject.ErrorMessage,
                OkTransactionId = domainObject.OkTransactionId,
                OkTransactionStatus = domainObject.OkTransactionStatus,
                DestinationNumber = domainObject.DestinationNumber,
                TopupTransactionReferenceId = domainObject.TopupTrasactionReferenceId,
                CurrencyDetailId = domainObject.CurrencyDetailId
            };
        }
    }
}