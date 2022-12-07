using System;
using Kispanadi.Common.Ddd.Objects;

namespace SpeedKyat.InternationalMobile.Topup.Domain
{
    public class Customer : IDomainObject
    {
        public Guid CustomerId { get; protected set; }
        public string OkAccountNumber { get; protected set; }
        public DateTime CreatedDate { get; protected set; }

        public Customer(Guid customerId, string okAccountNumber, DateTime createdDate)
        {
            CustomerId = customerId;
            OkAccountNumber = okAccountNumber;
            CreatedDate = createdDate;
        }
    }
}