namespace ProjectG.OrderService.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectG.OrderService.Core.Models;

    public interface IOrderRepository
    {
        IQueryable<Order> Get();

        Task<Order> Get(long orderId);

        Task<IEnumerable<Order>> GetByCustomerId(long customerId);

        Task Create(Order order);

        Task SaveChanges();
    }
}