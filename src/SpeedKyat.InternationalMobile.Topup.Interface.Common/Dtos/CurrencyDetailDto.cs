using System;
using System.Runtime.Serialization;
using Kispanadi.Common.Ddd.Interface;
using Newtonsoft.Json;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos
{
    [DataContract]
    public class CurrencyDetailDto : IDto
    {
        [DataMember, JsonIgnore] private readonly Guid _currencyDetailId;
        [DataMember, JsonIgnore] private readonly string _countryName;
        [DataMember, JsonIgnore] private readonly string _currencyType;
        [DataMember, JsonIgnore] private readonly double _exchangeRate;
        [DataMember, JsonIgnore] private readonly DateTime _createdDate;

        public CurrencyDetailDto(Guid currencyDetailId, string countryName, string currencyType, double exchangeRate, DateTime createdDate)
        {
            _currencyDetailId = currencyDetailId;
            _countryName = countryName;
            _currencyType = currencyType;
            _exchangeRate = exchangeRate;
            _createdDate = createdDate;
        }

        [JsonProperty]
        public Guid CurrencyDetailId
        {
            get { return _currencyDetailId; }
        }

        [JsonProperty]
        public string CountryName
        {
            get { return _countryName; }
        }

        [JsonProperty]
        public string CurrencyType
        {
            get { return _currencyType; }
        }

        [JsonProperty]
        public double ExchangeRate
        {
            get { return _exchangeRate; }
        }

        
        [JsonProperty]
        public DateTime CreatedDate
        {
            get { return _createdDate; }
        }
    }
}