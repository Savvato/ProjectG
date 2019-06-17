namespace ProjectG.ClientService.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.Interfaces;
    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;
    using ProjectG.ClientService.Infrastructure.ProductApi.Interfaces;

    public class ProductRepository : IProductRepository
    {
        private readonly IProductReadApiClient readApiClient;

        public ProductRepository(IProductReadApiClient readApiClient)
        {
            this.readApiClient = readApiClient;
        }

        public async Task<IEnumerable<ProductModel>> Get()
        {
            return await this.readApiClient.GetAllProducts();
        }

        public async Task<ProductModel> Get(long id)
        {
            return await this.readApiClient.GetProductById(id);
        }
    }
}