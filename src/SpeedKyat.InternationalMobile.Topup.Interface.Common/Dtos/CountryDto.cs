using System;
using System.Runtime.Serialization;
using Kispanadi.Common.Ddd.Interface;
using Newtonsoft.Json;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos
{
    [DataContract]
    public class CountryDto : IDto
    {
        [DataMember, JsonIgnore] private readonly Guid _countryId;
        [DataMember, JsonIgnore] private readonly string _countryName;
        [DataMember, JsonIgnore] private readonly string _countryNameBurmese;
        [DataMember, JsonIgnore] private readonly string _countryFlag;
        [DataMember, JsonIgnore] private readonly bool _isActive;
        [DataMember, JsonIgnore] private readonly DateTime _createdDate;
        [DataMember, JsonIgnore] private readonly string _countryCode;
        [DataMember, JsonIgnore] private readonly string _countryCode2;
        [DataMember, JsonIgnore] private readonly CurrencyDetailDto _currencyDetail;

        public CountryDto(Guid countryId, string countryName, string countryNameBurmese, string countryFlag, bool isActive, DateTime createdDate, string countryCode, string countryCode2, CurrencyDetailDto currencyDetail)
        {
            _countryId = countryId;
            _countryName = countryName;
            _countryNameBurmese = countryNameBurmese;
            _countryFlag = countryFlag;
            _isActive = isActive;
            _createdDate = createdDate;
            _countryCode = countryCode;
            _countryCode2 = countryCode2;
            _currencyDetail = currencyDetail;
        }

        [JsonProperty]
        public Guid CountryId
        {
            get { return _countryId; }
        }

        [JsonProperty]
        public string CountryName
        {
            get { return _countryName; }
        }

        [JsonProperty]
        public string CountryNameBurmese
        {
            get { return _countryNameBurmese; }
        }

        [JsonProperty]
        public string CountryFlag
        {
            get { return _countryFlag; }
        }

        [JsonProperty]
        public bool IsActive
        {
            get { return _isActive; }
        }

        [JsonProperty]
        public DateTime CreatedDate
        {
            get { return _createdDate; }
        }

        [JsonProperty]
        public string CountryCode
        {
            get { return _countryCode; }
        }

        [JsonProperty]
        public string CountryCode2
        {
            get { return _countryCode2; }
        }

        [JsonProperty]
        public CurrencyDetailDto CurrencyDetail
        {
            get { return _currencyDetail; }
        }
    }
}