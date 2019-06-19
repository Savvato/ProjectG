namespace ProjectG.ClientService.Infrastructure
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;
    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;
    using ProjectG.ClientService.Infrastructure.CustomerApi.Interfaces;
    using ProjectG.ClientService.Infrastructure.DTO;
    using ProjectG.ClientService.Infrastructure.Interfaces;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IBasketRepository basketRepository;

        private readonly ICustomerReadApiClient customerReadApiClient;
        private readonly ICustomerWriteApiClient customerWriteApiClient;

        public CustomerRepository(
            ICustomerReadApiClient customerReadApiClient, 
            ICustomerWriteApiClient customerWriteApiClient, 
            IBasketRepository basketRepository)
        {
            this.customerReadApiClient = customerReadApiClient;
            this.customerWriteApiClient = customerWriteApiClient;
            this.basketRepository = basketRepository;
        }

        public async Task<IEnumerable<CustomerModel>> Get()
        {
            return await this.customerReadApiClient.GetAllCustomers();
        }

        public async Task<CustomerDetailedModel> Get(long customerId)
        {
            CustomerModel customerModel = null;
            IEnumerable<BasketPositionModel> basket = null;

            async Task LoadCustomer() => customerModel = await this.customerReadApiClient.GetCustomerById(customerId);
            async Task LoadBasket() => basket = await this.basketRepository.GetCustomerBasket(customerId);

            await Task.WhenAll(LoadCustomer(), LoadBasket());

            if (customerModel == null)
            {
                throw new HttpRequestException("Customer is not found");
            }

            return new CustomerDetailedModel
            {
                Customer = customerModel,
                Basket = basket ?? new List<BasketPositionModel>()
            };
        }

        public async Task Create(CustomerWriteModel customer)
        {
            await this.customerWriteApiClient.Create(customer);
        }
    }
}