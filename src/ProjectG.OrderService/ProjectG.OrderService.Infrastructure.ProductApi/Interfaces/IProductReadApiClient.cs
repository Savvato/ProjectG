namespace ProjectG.OrderService.Infrastructure.ProductApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.OrderService.Infrastructure.ProductApi.DTO;

    public interface IProductReadApiClient
    {
        Task<ProductModel> GetProductById(long productId);
    }
}