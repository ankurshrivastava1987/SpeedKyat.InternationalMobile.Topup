using System;
using SpeedKyat.InternationalMobile.Topup.Domain;

namespace SpeedKyat.InternationalMobile.Topup.Application.Factory
{
    public class CustomerFactory
    {
        public Customer CreateNewCustomer(string phoneNumber)
        {
            return new Customer(Guid.NewGuid(), phoneNumber, DateTime.Now);
        }
    }
}
