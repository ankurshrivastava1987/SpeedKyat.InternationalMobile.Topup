using SpeedKyat.InternationalMobile.Topup.Domain;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Service.Mapper
{
    public class TopupTransactionMapper : IMapper<TopupTransaction, TopupTransactionDto>
    {
        public TopupTransactionDto ToDto(TopupTransaction domainObject)
        {
            CustomerDto customer = null;
            CurrencyDetailDto currencyDetail = null;

            if (domainObject.Customer != null)
            {
                customer = new CustomerMapper().ToDto(domainObject.Customer);
            }

            if (domainObject.CurrencyDetail != null)
            {
                currencyDetail = new CurrencyDetailMapper().ToDto(domainObject.CurrencyDetail);
            }

            return new TopupTransactionDto(domainObject.TopupTransactionId, domainObject.CustomerId,
                domainObject.CountryCode, domainObject.OperatorName, domainObject.TopupNumber, domainObject.Amount,
                domainObject.AmountInMmk, domainObject.CreatedDate,
                domainObject.TopupStatus, domainObject.ErrorMessage, domainObject.OkTransactionId,
                domainObject.OkTransactionStatus, domainObject.TopupTrasactionReferenceId, domainObject.Reference1, domainObject.Reference2, customer, currencyDetail);
        }
    }
}