namespace ProjectG.ClientService.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;

    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerModel>> Get();

        Task<CustomerModel> Get(long customerId);
    }
}