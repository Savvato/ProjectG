namespace ProjectG.OrderService.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.OrderService.Infrastructure.BasketApi.DTO;

    public interface IBasketRepository
    {
        Task<IEnumerable<BasketPositionModel>> GetCustomerBasket(long customerId);
    }
}