namespace ProjectG.ProductService.Infrastructure
{
    using System.Threading.Tasks;

    using ProjectG.ProductService.Infrastructure.Interfaces;
    using ProjectG.ProductService.Infrastructure.OrderApi.DTO;
    using ProjectG.ProductService.Infrastructure.OrderApi.Interfaces;

    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderReadApiClient orderReadApiClient;

        public OrderRepository(IOrderReadApiClient orderReadApiClient)
        {
            this.orderReadApiClient = orderReadApiClient;
        }

        public async Task<OrderModel> Get(long id)
        {
            return await this.orderReadApiClient.GetOrderById(id);
        }
    }
}