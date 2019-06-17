namespace ProjectG.ClientService.Infrastructure.CustomerApi.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;

    public interface ICustomerReadApiClient
    {
        Task<IEnumerable<CustomerModel>> GetAllCustomers();

        Task<CustomerModel> GetCustomerById(long customerId);
    }
}