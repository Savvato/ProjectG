namespace ProjectG.ClientService.Infrastructure.ProductApi.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;

    public interface IProductReadApiClient
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();

        Task<ProductModel> GetProductById(long productId);
    }
}