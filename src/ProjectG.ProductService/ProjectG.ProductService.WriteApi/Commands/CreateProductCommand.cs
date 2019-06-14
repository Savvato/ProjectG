namespace ProjectG.ProductService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    using ProjectG.Core;
    using ProjectG.ProductService.Core.Interfaces;
    using ProjectG.ProductService.Core.Models;
    using ProjectG.ProductService.WriteApi.DTO;
    using ProjectG.ProductService.WriteApi.Extensions;

    public class CreateProductCommand : ICommandHandler<ProductCreationModel>
    {
        private readonly IProductRepository productRepository;
        private readonly IDistributedCache cache;

        public CreateProductCommand(
            IProductRepository productRepository, 
            IDistributedCache cache)
        {
            this.productRepository = productRepository;
            this.cache = cache;
        }

        public async Task Execute(ProductCreationModel commandData)
        {
            Product product = commandData.ToProduct();

            await this.productRepository.Add(product);

            await cache.Set($"product.{product.Id}", product);
        }
    }
}
