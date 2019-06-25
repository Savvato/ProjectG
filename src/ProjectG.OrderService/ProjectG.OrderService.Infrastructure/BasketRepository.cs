namespace ProjectG.OrderService.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.OrderService.Infrastructure.BasketApi.DTO;
    using ProjectG.OrderService.Infrastructure.BasketApi.Interfaces;
    using ProjectG.OrderService.Infrastructure.Interfaces;

    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketGraphQLClient basketReadApiClient;

        public BasketRepository(IBasketGraphQLClient basketReadApiClient)
        {
            this.basketReadApiClient = basketReadApiClient;
        }

        public async Task<IEnumerable<BasketPositionModel>> GetCustomerBasket(long customerId)
        {
            return await this.basketReadApiClient.GetCustomerBasket(customerId);
        }
    }
}