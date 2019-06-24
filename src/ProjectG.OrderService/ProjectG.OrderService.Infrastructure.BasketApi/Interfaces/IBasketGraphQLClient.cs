namespace ProjectG.OrderService.Infrastructure.BasketApi.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.OrderService.Infrastructure.BasketApi.DTO;

    public interface IBasketGraphQLClient
    {
        Task<IEnumerable<BasketPositionModel>> GetCustomerBasket(long customerId);
    }
}