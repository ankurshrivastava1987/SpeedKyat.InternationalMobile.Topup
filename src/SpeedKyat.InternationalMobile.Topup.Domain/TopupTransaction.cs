using System;
using Kispanadi.Common.Ddd.Objects;

namespace SpeedKyat.InternationalMobile.Topup.Domain
{
    public class TopupTransaction: IDomainObject
    {
       public Guid TopupTransactionId { get; protected set; }
       public Guid CustomerId { get; protected set; }
       public string CountryCode { get; protected set; }
       public string OperatorName { get; protected set; }
       public string TopupNumber { get; protected set; }
       public double Amount { get; protected set; }
       public double AmountInMmk { get; protected set; }
       public DateTime CreatedDate { get; protected set; }
       public string TopupStatus { get; protected set; }
       public string ErrorMessage { get; protected set; }
       public string OkTransactionId { get; protected set; }
       public string OkTransactionStatus { get; protected set; }
       public string DestinationNumber { get; protected set; }
        public Guid TopupTrasactionReferenceId { get; protected set; }
       public Guid CurrencyDetailId { get; protected set; }
       public string Reference1 { get; protected set; }
       public string Reference2 { get; protected set; }
        public Customer Customer { get; protected set; }
        public CurrencyDetail CurrencyDetail { get; protected set; }
        public TopupTransaction(Guid topupTransactionId, Guid customerId, string countryCode, string operatorName,
            string topupNumber, double amount, double amountInMmk,
            DateTime createdDate, string topupStatus, string errorMessage, string okTransactionId,
            string okTransactionStatus, string destinationNumber, Guid topupTrasactionReferenceId, Guid currencyDetailId, string reference1, string reference2)
        {
            TopupTransactionId = topupTransactionId;
            CustomerId = customerId;
            CountryCode = countryCode;
            OperatorName = operatorName;
            TopupNumber = topupNumber;
            Amount = amount;
            AmountInMmk = amountInMmk;
            CreatedDate = createdDate;
            TopupStatus = topupStatus;
            ErrorMessage = errorMessage;
            OkTransactionId = okTransactionId;
            OkTransactionStatus = okTransactionStatus;
            DestinationNumber = destinationNumber;
            TopupTrasactionReferenceId = topupTrasactionReferenceId;
            CurrencyDetailId = currencyDetailId;
            Reference1 = reference1;
            Reference2 = reference2;
        }

        public void SetTopupStatus(string status)
        {
            TopupStatus = status;
        }

        public void SetErrorMessage(string message)
        {
            ErrorMessage = message;
        }

        public void SetOkTransactionId(string id)
        {
            OkTransactionId = id;
        }

        public void SetOkTransactionStatus(string status)
        {
            OkTransactionStatus = status;
        }

        public void SetTopupTransactionRefId(Guid topupTransId)
        {
            TopupTrasactionReferenceId = topupTransId;
        }

        public void SetCustomer(Customer customer)
        {
            Customer = customer;
        }

        public void SetCurrencyDetail(CurrencyDetail currencyDetail)
        {
            CurrencyDetail = currencyDetail;
        }
    }
}