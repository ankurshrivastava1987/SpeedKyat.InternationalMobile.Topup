using System.ServiceModel;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.CommunictionObjects
{
    [ServiceContract]
    public interface IServiceResponse
    {
        bool Success { get; }
        string Message { get; }
        string Result { get; }
    }
}