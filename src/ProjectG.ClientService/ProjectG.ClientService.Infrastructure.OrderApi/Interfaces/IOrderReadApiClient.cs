namespace ProjectG.ClientService.Infrastructure.OrderApi.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.OrderApi.DTO;

    public interface IOrderReadApiClient
    {
        Task<OrderModel> GetById(long orderId);

        Task<IEnumerable<OrderModel>> GetByCustomerId(long customerId);
    }
}