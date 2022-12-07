using System.Runtime.Serialization;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.CommunictionObjects
{
    [DataContract]
    public class ServiceRequest : IServiceRequest
    {
        [DataMember] private string _id;

        public ServiceRequest(string id)
        {
            _id = id;
        }

        public string Id
        {
            get { return _id; }
        }
    }

    [DataContract]
    public class PayloadServiceRequest : ServiceRequest
    {
        [DataMember] private string _payload;

        public PayloadServiceRequest(string id, string payload) : base(id)
        {
            _payload = payload;
        }

        public string Payload
        {
            get { return _payload; }
        }
    }
}