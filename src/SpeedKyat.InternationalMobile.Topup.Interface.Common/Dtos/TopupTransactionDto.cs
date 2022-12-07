using System;
using System.Runtime.Serialization;
using Kispanadi.Common.Ddd.Interface;
using Newtonsoft.Json;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos
{
    [DataContract]
    public class TopupTransactionDto : IDto
    {
        [DataMember, JsonIgnore] private readonly Guid _topupTransactionId;
        [DataMember, JsonIgnore] private readonly Guid _customerId;
        [DataMember, JsonIgnore] private readonly string _countryCode;
        [DataMember, JsonIgnore] private readonly string _operatorName;
        [DataMember, JsonIgnore] private readonly string _topupNumber;
        [DataMember, JsonIgnore] private readonly double _amount;
        [DataMember, JsonIgnore] private readonly double _amountInMmk;
        [DataMember, JsonIgnore] private readonly DateTime _createdDate;
        [DataMember, JsonIgnore] private readonly string _topupStatus;
        [DataMember, JsonIgnore] private readonly string _errorMessage;
        [DataMember, JsonIgnore] private readonly string _okTransactionId;
        [DataMember, JsonIgnore] private readonly string _okTransactionStatus;
        [DataMember, JsonIgnore] private readonly Guid _topupTransactionReferenceId;
        [DataMember, JsonIgnore] private readonly string _reference1;
        [DataMember, JsonIgnore] private readonly string _reference2;
        [DataMember, JsonIgnore] private readonly CustomerDto _customer;
        [DataMember, JsonIgnore] private readonly CurrencyDetailDto _currencyDetail;

        public TopupTransactionDto(Guid topupTransactionId, Guid customerId, string countryCode, string operatorName,
            string topupNumber, double amount, double amountInMMK,
            DateTime createdDate, string topupStatus, string errorMessage, string okTransactionId,
            string okTransactionStatus, Guid topupTransactionReferenceId, string reference1,
            string reference2, CustomerDto customer,
            CurrencyDetailDto currencyDetail)
        {
            _topupTransactionId = topupTransactionId;
            _customerId = customerId;
            _countryCode = countryCode;
            _operatorName = operatorName;
            _topupNumber = topupNumber;
            _amount = amount;
            _amountInMmk = amountInMMK;
            _createdDate = createdDate;
            _topupStatus = topupStatus;
            _errorMessage = errorMessage;
            _okTransactionId = okTransactionId;
            _okTransactionStatus = okTransactionStatus;
            _topupTransactionReferenceId = topupTransactionReferenceId;
            _reference1 = reference1;
            _reference2 = reference2;
            _customer = customer;
            _currencyDetail = currencyDetail;
        }

        [JsonProperty]
        public Guid TopupTransactionId
        {
            get { return _topupTransactionId; }
        }

        [JsonProperty]
        public Guid CustomerId
        {
            get { return _customerId; }
        }

        [JsonProperty]
        public string CountryCode
        {
            get { return _countryCode; }
        }

        [JsonProperty]
        public string OperatorName
        {
            get { return _operatorName; }
        }

        [JsonProperty]
        public string TopupNumber
        {
            get { return _topupNumber; }
        }

        [JsonProperty]
        public double Amount
        {
            get { return _amount; }
        }

        [JsonProperty]
        public double AmountInMmk
        {
            get { return _amountInMmk; }
        }

        [JsonProperty]
        public DateTime CreatedDate
        {
            get { return _createdDate; }
        }

        [JsonProperty]
        public string TopupStatus
        {
            get { return _topupStatus; }
        }

        [JsonProperty]
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        [JsonProperty]
        public string OkTransactionId
        {
            get { return _okTransactionId; }
        }

        [JsonProperty]
        public string OkTransactionStatus
        {
            get { return _okTransactionStatus; }
        }
        
        [JsonProperty]
        public Guid TopupTransactionReferenceId
        {
            get { return _topupTransactionReferenceId; }
        }

        [JsonProperty]
        public string Reference1
        {
            get { return _reference1; }
        }

        [JsonProperty]
        public string Reference2
        {
            get { return _reference2; }
        }

        [JsonProperty]
        public CustomerDto Customer
        {
            get { return _customer; }
        }

        [JsonProperty]
        public CurrencyDetailDto CurrencyDetail
        {
            get { return _currencyDetail; }
        }
    }
}