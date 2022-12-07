namespace SpeedKyat.InternationalMobile.Topup.Presentation.Api.Helpers
{
    public class Constants
    {
        public static class HttpVerbs
        {
            public const string Delete = "DELETE";
        }

        public static class Routes
        {
            public static class Prefixes
            {
                public const string Country = "countries";
                public const string Countries = "allcountries";
                public const string Topup = "topup";
                public const string TopupTransaction = "topuptransactions/{okTransactionId}";
                public const string Transaction = "transactiondetails/{referenceId}";
            }
        
            public static class Paths
            {
                public const string Operator = "{countryId}/operators";
                public const string Version = "buildversion/{buildversion}/appversion/{appversion}";
            }
        }
    }
}