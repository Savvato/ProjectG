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
        private readonly IProductWriteApiClient writeApiClient;

        public ProductRepository(
            IProductReadApiClient readApiClient, 
            IProductWriteApiClient writeApiClient)
        {
            this.readApiClient = readApiClient;
            this.writeApiClient = writeApiClient;
        }

        public async Task<IEnumerable<ProductModel>> Get()
        {
            return await this.readApiClient.GetAllProducts();
        }

        public async Task<ProductModel> Get(long id)
        {
            return await this.readApiClient.GetProductById(id);
        }

        public async Task Create(ProductWriteModel product)
        {
            await this.writeApiClient.Create(product);
        }

        public async Task Edit(ProductModel product)
        {
            throw new System.NotImplementedException();
        }
    }
}