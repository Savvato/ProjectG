namespace ProjectG.OrderService.Infrastructure
{
    using System.Collections.Generic;
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
            return this.dbContext.Orders
                .Include(order => order.OrderPositions)
                .Include(order => order.StatusDetails)
                .AsQueryable();
        }

        public async Task<Order> Get(long orderId)
        {
            return await this.Get().FirstOrDefaultAsync(order => order.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetByCustomerId(long customerId)
        {
            return await this.Get().Where(order => order.CustomerId == customerId).ToListAsync();
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