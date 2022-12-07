using SpeedKyat.InternationalMobile.Topup.Domain;
using SpeedKyat.InternationalMobile.Topup.Interface.Common.Dtos;

namespace SpeedKyat.InternationalMobile.Topup.Interface.Common.Service.Mapper
{
    public class CustomerMapper: IMapper<Customer, CustomerDto>
    {
        public CustomerDto ToDto(Customer domainObject)
        {
            return new CustomerDto(domainObject.CustomerId, domainObject.OkAccountNumber, domainObject.CreatedDate);
        }
    }
}