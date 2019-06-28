namespace ProjectG.ProductService.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Core.Models;
    using ProjectG.ProductService.Infrastructure.Cache.Interfaces;
    using Db;
    using Interfaces;

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
            return this.dbContext.Products.Where(product => product.Count > 0);
        }

        public async Task<Product> Get(long id)
        {
            Product cachedProduct = await this.cache.Get(id);

            if (cachedProduct == null)
            {
                cachedProduct = await this.Get().FirstOrDefaultAsync(product => product.Id == id);
                await this.cache.Set(cachedProduct);
            }

            return cachedProduct;
        }

        public async Task Add(Product product)
        {
            await this.dbContext.Products.AddAsync(product);
            await this.dbContext.SaveChangesAsync();

            await this.cache.Set(product);
        }

        public async Task Update(Product product)
        {
            this.dbContext.Products.Update(product);
            await this.dbContext.SaveChangesAsync();

            await this.cache.Set(product);
        }
    }
}