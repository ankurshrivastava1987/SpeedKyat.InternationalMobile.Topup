namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.Models
{
    public class TopupModel
    {
        public string OkAccountNumber { get; protected set; }
        public string CountryCode { get; protected set; }
        public string OperatorName { get; protected set; }
        public string TopupNumber { get; protected set; }
        public double AirTimeAmount { get; protected set; }
        public double TotalAmount { get; protected set; }
        public string OkTransactionId { get; protected set; }
        public string Reference1 { get; protected set; }
        public string Reference2 { get; protected set; }
        public string HashValue { get; protected set; }
        public TopupModel(string okAccountNumber, string countryCode, string operatorName, string topupNumber, double airTimeAmount, double totalAmount, string okTransactionId, string reference1, string reference2, string hashValue)
        {
            OkAccountNumber = okAccountNumber;
            CountryCode = countryCode;
            OperatorName = operatorName;
            TopupNumber = topupNumber;
            AirTimeAmount = airTimeAmount;
            TotalAmount = totalAmount;
            OkTransactionId = okTransactionId;
            Reference1 = reference1;
            Reference2 = reference2;
            HashValue = hashValue;
        }
    }
}