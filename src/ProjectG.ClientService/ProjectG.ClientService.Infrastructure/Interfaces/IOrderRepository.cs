namespace ProjectG.ClientService.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.OrderApi.DTO;

    public interface IOrderRepository
    {
        Task<OrderModel> Get(long orderId);

        Task<IEnumerable<OrderModel>> GetByCustomerId(long customerId);

        Task Create(long customerId);
    }
}