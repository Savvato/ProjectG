namespace ProjectG.ProductService.Infrastructure.Cache
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    using ProjectG.ProductService.Core.Models;
    using ProjectG.ProductService.Infrastructure.Cache.Extensions;
    using ProjectG.ProductService.Infrastructure.Cache.Interfaces;

    public class ProductCache : IProductCache
    {
        private readonly IDistributedCache cache;

        public ProductCache(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<Product> Get(long id)
        {
            return await this.cache.Get<Product>(this.GetKey(id));
        }

        public async Task Set(Product product)
        {
            if (product == null) return;

            await this.cache.Set<Product>(this.GetKey(product.Id), product);
        }

        private string GetKey(long productId)
        {
            return $"product.{productId}";
        }
    }
}