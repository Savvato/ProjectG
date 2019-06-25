namespace ProjectG.OrderService.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.OrderService.Core.Models;
    using ProjectG.OrderService.Infrastructure.Db;
    using ProjectG.OrderService.Infrastructure.Interfaces;

    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Order> Get()
        {
            return this.dbContext.Orders.Include(order => order.OrderPositions).AsQueryable();
        }

        public async Task<Order> Get(long orderId)
        {
            return await this.dbContext.Orders.Include(order => order.OrderPositions).FirstOrDefaultAsync(order => order.Id == orderId);
        }

        public async Task Create(Order order)
        {
            await this.dbContext.Orders.AddAsync(order);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}