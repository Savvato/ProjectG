namespace ProjectG.ClientService.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;

    public interface IBasketRepository
    {
        Task<IEnumerable<BasketPositionModel>> GetCustomerBasket(long customerId);

        Task Create(BasketPositionWriteModel basketPosition);
    }
}