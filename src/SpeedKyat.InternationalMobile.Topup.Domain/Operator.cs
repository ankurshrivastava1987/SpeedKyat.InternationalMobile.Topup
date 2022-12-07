using System;
using System.Collections.Generic;
using Kispanadi.Common.Ddd.Objects;

namespace SpeedKyat.InternationalMobile.Topup.Domain
{
    public class Operator : IDomainObject
    {
        public Guid OperatorId { get; protected set; }
        public string OperatorName { get; protected set; }
        public string OperatorCode { get; protected set; }
        public Guid CountryId { get; protected set; }
        public string OperatorLogo { get; protected set; }
        public bool IsActive { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public Country Country { get; protected set; }
        public List<int> ProductList { get; protected set; }
        public Operator(Guid operatorId, string operatorName, string operatorCode, Guid countryId, string operatorLogo, bool isActive, DateTime createdDate, List<int> productList ,Country country )
        {
            OperatorId = operatorId;
            OperatorName = operatorName;
            OperatorCode = operatorCode;
            CountryId = countryId;
            OperatorLogo = operatorLogo;
            IsActive = isActive;
            CreatedDate = createdDate;
            ProductList = productList;
            Country = country;
        }

        public void SetProduct(int product)
        {
            ProductList.Add(product);
        }
    }
}