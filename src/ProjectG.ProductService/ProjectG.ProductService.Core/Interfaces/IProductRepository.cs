namespace ProjectG.ProductService.Core.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectG.ProductService.Core.Models;

    public interface IProductRepository
    {
        IQueryable<Product> GetQuery();

        Task Add(Product product);
    }
}