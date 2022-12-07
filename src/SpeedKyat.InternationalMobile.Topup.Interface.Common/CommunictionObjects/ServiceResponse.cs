using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.CommunictionObjects
{
    [DataContract]
    public class ServiceResponse : IServiceResponse
    {
        [DataMember, JsonIgnore] private readonly bool _success;
        [DataMember, JsonIgnore] private readonly string _message;
        [DataMember, JsonIgnore] private readonly string _result;

        public ServiceResponse(bool success, string message, string result)
        {
            _success = success;
            _message = message;
            _result = result;
        }

        public ServiceResponse(bool success, string message)
        {
            _success = success;
            _message = message;
        }

        [JsonProperty]
        public bool Success
        {
            get { return _success; }
        }

        [JsonProperty]
        public string Message
        {
            get { return _message; }
        }

        [JsonProperty]
        public string Result
        {
            get { return _result; }
        }
    }
}