namespace ProjectG.BasketService.Infrastructure.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    using ProjectG.BasketService.Core.Models;
    using ProjectG.BasketService.Infrastructure.Cache.Extensions;
    using ProjectG.BasketService.Infrastructure.Cache.Interfaces;

    public class BasketCache : IBasketCache
    {
        private static readonly DistributedCacheEntryOptions DefaultCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        private readonly IDistributedCache cache;

        public BasketCache(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<IEnumerable<BasketPosition>> Get(long customerId)
        {
            return await this.cache.Get<IEnumerable<BasketPosition>>(this.GetCacheKey(customerId));
        }

        public async Task Set(long customerId, IEnumerable<BasketPosition> basket)
        {
            await this.cache.Set(this.GetCacheKey(customerId), basket, DefaultCacheEntryOptions);
        }

        public async Task Remove(long customerId)
        {
            await this.cache.RemoveAsync(this.GetCacheKey(customerId));
        }

        private string GetCacheKey(long customerId)
        {
            return $"basket.{customerId}";
        }
    }
}