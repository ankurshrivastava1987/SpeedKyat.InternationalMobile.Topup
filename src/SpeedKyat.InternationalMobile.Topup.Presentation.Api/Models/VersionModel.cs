namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.Models
{
    public class VersionModel
    {
        public string BuildVersion { get; protected set; }
        public string AppVersion { get; protected set; }
        public string DeviceName { get; protected set; }
        public string DeviceOs { get; protected set; }
        public string HashValue { get; protected set; }

        public VersionModel(string buildVersion, string appVersion, string deviceName, string deviceOs, string hashValue)
        {
            BuildVersion = buildVersion;
            AppVersion = appVersion;
            DeviceName = deviceName;
            DeviceOs = deviceOs;
            HashValue = hashValue;
        }
    }
}