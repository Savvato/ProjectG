namespace ProjectG.ProductService.Infrastructure.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectG.ProductService.Core.Models;

    public interface IProductRepository
    {
        IQueryable<Product> Get();

        Task<Product> Get(long id);

        Task Add(Product product);
    }
}