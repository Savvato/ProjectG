namespace ProjectG.ProductService.Infrastructure.Cache.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.ProductService.Core.Models;

    public interface IProductCache
    {
        Task<Product> Get(long id);

        Task Set(Product product);
    }
}