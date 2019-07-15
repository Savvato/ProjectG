namespace ProjectG.BasketService.Infrastructure
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.BasketService.Core.Models;
    using ProjectG.BasketService.Infrastructure.Cache.Interfaces;
    using ProjectG.BasketService.Infrastructure.Db;
    using ProjectG.BasketService.Infrastructure.Interfaces;

    public class BasketRepository : IBasketRepository
    {
        private readonly BasketDbContext dbContext;
        private readonly IBasketCache basketCache;

        public BasketRepository(BasketDbContext dbContext, IBasketCache basketCache)
        {
            this.dbContext = dbContext;
            this.basketCache = basketCache;
        }

        public IQueryable<BasketPosition> Get()
        {
            return this.dbContext.BasketPositions;
        }

        public async Task<BasketPosition> Get(long id)
        {
            return await this.dbContext.BasketPositions.FirstOrDefaultAsync(position => position.Id == id);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public async Task<IEnumerable<BasketPosition>> GetByCustomerId(long customerId)
        {
            IEnumerable<BasketPosition> basket = await this.basketCache.Get(customerId);

            if (basket == null || !basket.Any())
            {
                basket = await this.dbContext.BasketPositions.Where(position => position.CustomerId == customerId).ToListAsync();
                await this.basketCache.Set(customerId, basket);
            }

            return basket;
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

        public async Task RemoveCustomerBasket(long customerId)
        {
            this.dbContext.BasketPositions.RemoveRange(this.dbContext.BasketPositions.Where(position => position.CustomerId == customerId));
            await this.dbContext.SaveChangesAsync();

            await this.basketCache.Remove(customerId);
        }

        public async Task SaveChanges()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Refresh(params long[] customerIds)
        {
            foreach (long customerId in customerIds)
            {
                IEnumerable<BasketPosition> basket = await this.dbContext
                    .BasketPositions
                    .Where(position => position.CustomerId == customerId)
                    .ToListAsync();

                await this.basketCache.Set(customerId, basket);
            }
        }
    }
}
