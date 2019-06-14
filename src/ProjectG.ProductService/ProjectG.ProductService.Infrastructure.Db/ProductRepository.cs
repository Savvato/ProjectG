namespace ProjectG.ProductService.Infrastructure.Db
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectG.ProductService.Core.Interfaces;
    using ProjectG.ProductService.Core.Models;

    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Product> GetQuery()
        {
            return this.dbContext.Products;
        }

        public async Task Add(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }
    }
}