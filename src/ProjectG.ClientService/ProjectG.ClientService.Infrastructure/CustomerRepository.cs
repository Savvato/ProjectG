namespace ProjectG.ClientService.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;
    using ProjectG.ClientService.Infrastructure.CustomerApi.Interfaces;
    using ProjectG.ClientService.Infrastructure.Interfaces;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerReadApiClient customerReadApiClient;

        public CustomerRepository(ICustomerReadApiClient customerReadApiClient)
        {
            this.customerReadApiClient = customerReadApiClient;
        }

        public async Task<IEnumerable<CustomerModel>> Get()
        {
            return await this.customerReadApiClient.GetAllCustomers();
        }

        public async Task<CustomerModel> Get(long customerId)
        {
            return await this.customerReadApiClient.GetCustomerById(customerId);
        }
    }
}