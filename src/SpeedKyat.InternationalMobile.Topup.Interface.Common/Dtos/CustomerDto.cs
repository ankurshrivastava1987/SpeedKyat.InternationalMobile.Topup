using System;
using System.Runtime.Serialization;
using Kispanadi.Common.Ddd.Interface;
using Newtonsoft.Json;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos
{
    [DataContract]
    public class CustomerDto: IDto
    {
        [DataMember, JsonIgnore] private readonly Guid _customerId;
        [DataMember, JsonIgnore] private readonly string _okAccountNumber;
        [DataMember, JsonIgnore] private readonly DateTime _createdDate;
        public CustomerDto(Guid customerId,string okAccountNumber,DateTime createdDate)
        {
           _customerId = customerId;
           _okAccountNumber = okAccountNumber;
           _createdDate = createdDate;
        }

        [JsonProperty]
        public Guid CustomerId
        {
            get { return _customerId; }
        }

        [JsonProperty]
        public string OkAccountNumber
        {
            get { return _okAccountNumber; }
        }

        [JsonProperty]
        public DateTime CreatedDate
        {
            get { return _createdDate; }
        }
    }
}