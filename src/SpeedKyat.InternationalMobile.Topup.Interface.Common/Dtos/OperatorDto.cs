using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Kispanadi.Common.Ddd.Interface;
using Newtonsoft.Json;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos
{
    [DataContract]
    public class OperatorDto : IDto
    {
        [DataMember, JsonIgnore] private readonly Guid _operatorId;
        [DataMember, JsonIgnore] private readonly string _operatorName;
        [DataMember, JsonIgnore] private readonly string _operatorCode;
        [DataMember, JsonIgnore] private readonly Guid _countryId;
        [DataMember, JsonIgnore] private readonly string _operatorLogo;
        [DataMember, JsonIgnore] private readonly bool _isActive;
        [DataMember, JsonIgnore] private readonly DateTime _createdDate;
        [DataMember, JsonIgnore] private readonly List<int> _productList;
        [DataMember, JsonIgnore] private readonly CountryDto _country;

        public OperatorDto(Guid operatorId, string operatorName, string operatorCode, Guid countryId,
            string operatorLogo, bool isActive, DateTime createdDate, List<int> productList, CountryDto country)
        {
            _operatorId = operatorId;
            _operatorName = operatorName;
            _operatorCode = operatorCode;
            _countryId = countryId;
            _operatorLogo = operatorLogo;
            _isActive = isActive;
            _createdDate = createdDate;
            _productList = productList;
            _country = country;
        }

        [JsonProperty]
        public Guid OperatorId
        {
            get { return _operatorId; }
        }

        [JsonProperty]
        public string OperatorName
        {
            get { return _operatorName; }
        }

        [JsonProperty]
        public string OperatorCode
        {
            get { return _operatorCode; }
        }

        [JsonProperty]
        public Guid CountryId
        {
            get { return _countryId; }
        }

        [JsonProperty]
        public string OperatorLogo
        {
            get { return _operatorLogo; }
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
        public List<int> ProductList
        {
            get { return _productList; }
        }

        [JsonProperty]
        public CountryDto Country
        {
            get { return _country; }
        }
    }
}