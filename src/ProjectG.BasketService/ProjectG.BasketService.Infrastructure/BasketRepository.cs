
namespace ProjectG.BasketService.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.BasketService.Core.Models;
    using ProjectG.BasketService.Infrastructure.Db;
    using ProjectG.BasketService.Infrastructure.Interfaces;

    public class BasketRepository : IBasketRepository
    {
        private readonly BasketDbContext dbContext;

        public BasketRepository(BasketDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<BasketPosition> Get()
        {
            return this.dbContext.BasketPositions;
        }

        public async Task<BasketPosition> Get(long id)
        {
            return await this.dbContext.BasketPositions.FirstOrDefaultAsync(position => position.Id == id);
        }

        public async Task<IEnumerable<BasketPosition>> GetByCustomerId(long customerId)
        {
            return await this.dbContext.BasketPositions.Where(position => position.CustomerId == customerId).ToListAsync();
        }

        public async Task Add(BasketPosition basketPosition)
        {
            await this.dbContext.BasketPositions.AddAsync(basketPosition);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(BasketPosition basketPosition)
        {
            return await this.dbContext.BasketPositions.AnyAsync(position =>
                position.CustomerId == basketPosition.CustomerId && position.ProductId == basketPosition.ProductId);
        }
    }
}
