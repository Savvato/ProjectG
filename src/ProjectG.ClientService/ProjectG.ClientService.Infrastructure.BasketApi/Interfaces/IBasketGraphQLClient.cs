namespace ProjectG.ClientService.Infrastructure.BasketApi.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;

    public interface IBasketGraphQLClient
    {
        Task<IEnumerable<BasketPositionModel>> GetCustomerBasket(long customerId);
    }
}