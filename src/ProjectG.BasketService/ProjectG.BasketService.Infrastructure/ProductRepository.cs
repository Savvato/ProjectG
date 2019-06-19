namespace ProjectG.BasketService.Infrastructure
{
    using System.Threading.Tasks;

    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.BasketService.Infrastructure.ProductApi.Interfaces;
    using ProjectG.BasketService.Infrastructure.ProductApi.Models;

    public class ProductRepository : IProductRepository
    {
        private readonly IProductReadApiClient productReadApiClient;

        public ProductRepository(IProductReadApiClient productReadApiClient)
        {
            this.productReadApiClient = productReadApiClient;
        }

        public async Task<ProductModel> Get(long productId)
        {
            return await this.productReadApiClient.GetProductById(productId);
        }
    }
}