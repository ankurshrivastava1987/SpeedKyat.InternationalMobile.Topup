using SpeedKyat.InternationalMobile.Topup.Domain;

namespace SpeedKyat.InternationalMobile.Topup.Application.Mapper
{
    public class CustomerMapper : IMapper<Customer, Infrastructure.Persistence.Data.Customer>
    {
        public Customer ToDomain(Infrastructure.Persistence.Data.Customer dbObject)
        {
            return new Customer(dbObject.CustomerId, dbObject.OkAccountNumber, dbObject.CreatedDate);
        }

        public Infrastructure.Persistence.Data.Customer ToDbObject(Customer domainObject)
        {
            return new Infrastructure.Persistence.Data.Customer
            {
                CustomerId = domainObject.CustomerId,
                OkAccountNumber = domainObject.OkAccountNumber,
                CreatedDate = domainObject.CreatedDate
            };
        }
    }
}