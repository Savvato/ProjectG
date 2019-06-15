namespace ProjectG.ProductService.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.ProductService.Core.Models;
    using ProjectG.ProductService.Infrastructure.Cache.Interfaces;
    using ProjectG.ProductService.Infrastructure.Db;
    using ProjectG.ProductService.Infrastructure.Interfaces;

    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext dbContext;
        private readonly IProductCache cache;

        public ProductRepository(ProductDbContext dbContext, IProductCache cache)
        {
            this.dbContext = dbContext;
            this.cache = cache;
        }

        public IQueryable<Product> Get()
        {
            return this.dbContext.Products;
        }

        public async Task<Product> Get(long id)
        {
            Product cachedProduct = await this.cache.Get(id);

            if (cachedProduct == null)
            {
                cachedProduct = await this.dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
                await cache.Set(cachedProduct);
            }

            return cachedProduct;
        }

        public async Task Add(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            await this.cache.Set(product);
        }
    }
}