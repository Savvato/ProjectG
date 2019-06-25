namespace ProjectG.OrderService.Infrastructure
{
    using System.Threading.Tasks;

    using ProjectG.OrderService.Infrastructure.CustomerApi.DTO;
    using ProjectG.OrderService.Infrastructure.CustomerApi.Interfaces;
    using ProjectG.OrderService.Infrastructure.Interfaces;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerReadApiClient customerReadApiClient;

        public CustomerRepository(ICustomerReadApiClient customerReadApiClient)
        {
            this.customerReadApiClient = customerReadApiClient;
        }

        public async Task<CustomerModel> Get(long customerId)
        {
            return await this.customerReadApiClient.Get(customerId);
        }
    }
}