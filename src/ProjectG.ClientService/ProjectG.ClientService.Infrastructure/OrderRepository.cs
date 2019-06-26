namespace ProjectG.ClientService.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.Interfaces;
    using ProjectG.ClientService.Infrastructure.OrderApi.DTO;
    using ProjectG.ClientService.Infrastructure.OrderApi.Interfaces;

    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderReadApiClient orderReadApiClient;
        private readonly IOrderWriteApiClient orderWriteApiClient;

        public OrderRepository(
            IOrderReadApiClient orderReadApiClient, 
            IOrderWriteApiClient orderWriteApiClient)
        {
            this.orderReadApiClient = orderReadApiClient;
            this.orderWriteApiClient = orderWriteApiClient;
        }

        public async Task<OrderModel> Get(long orderId)
        {
            return await this.orderReadApiClient.GetById(orderId);
        }

        public async Task<IEnumerable<OrderModel>> GetByCustomerId(long customerId)
        {
            return await this.orderReadApiClient.GetByCustomerId(customerId);
        }

        public async Task Create(long customerId)
        {
            await this.orderWriteApiClient.Create(new OrderCreationRequestModel { CustomerId = customerId});
        }
    }
}