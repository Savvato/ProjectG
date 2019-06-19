namespace ProjectG.BasketService.Infrastructure.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.BasketService.Infrastructure.ProductApi.Models;

    public interface IProductRepository
    {
        Task<ProductModel> Get(long productId);
    }
}