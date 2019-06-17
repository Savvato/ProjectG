namespace ProjectG.ClientService.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;
    using ProjectG.ClientService.Infrastructure.DTO;

    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerModel>> Get();

        Task<CustomerDetailedModel> Get(long customerId);
    }
}