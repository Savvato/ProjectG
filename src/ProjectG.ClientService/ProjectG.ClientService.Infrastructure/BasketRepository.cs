namespace ProjectG.ClientService.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;
    using ProjectG.ClientService.Infrastructure.BasketApi.Interfaces;
    using ProjectG.ClientService.Infrastructure.Interfaces;

    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketGraphQLClient basketGraphQLClient;
        private readonly IBasketWriteApiClient basketWriteApiClient;

        public BasketRepository(
            IBasketGraphQLClient basketGraphQLClient, 
            IBasketWriteApiClient basketWriteApiClient)
        {
            this.basketGraphQLClient = basketGraphQLClient;
            this.basketWriteApiClient = basketWriteApiClient;
        }

        public async Task<IEnumerable<BasketPositionModel>> GetCustomerBasket(long customerId)
        {
            return await this.basketGraphQLClient.GetCustomerBasket(customerId);
        }

        public async Task Create(BasketPositionWriteModel basketPosition)
        {
            await this.basketWriteApiClient.Create(basketPosition);
        }
    }
}