namespace ProjectG.BasketService.Infrastructure.ProductApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.BasketService.Infrastructure.ProductApi.Models;

    public interface IProductReadApiClient
    {
        Task<ProductModel> GetProductById(long productId);
    }
}