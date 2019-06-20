namespace ProjectG.ProductService.WriteApi.Commands
{
    using System.IO;
    using System.Threading.Tasks;

    using ProjectG.ProductService.Infrastructure.Interfaces;
    using ProjectG.ProductService.WriteApi.DTO;
    using ProjectG.Core;
    using ProjectG.ProductService.Core.Models;
    using ProjectG.ProductService.Infrastructure.Kafka.Interfaces;

    public class EditProductCommand : ICommandHandler<ProductEditModel>
    {
        private const string ProductUpdatesTopicName = "product-updates-topic";

        private readonly IProductRepository productRepository;
        private readonly IProducerService producerService;

        public EditProductCommand(
            IProductRepository productRepository, 
            IProducerService producerService)
        {
            this.productRepository = productRepository;
            this.producerService = producerService;
        }

        public async Task Execute(ProductEditModel commandData)
        {
            Product product = await this.productRepository.Get(commandData.Id);

            if (product == null)
            {
                throw new InvalidDataException($"Product with ID: {commandData.Id} is not found");
            }

            product.Name = commandData.Name;
            product.Description = commandData.Description;
            product.Count = commandData.Count;
            product.Price = commandData.Price;

            await this.productRepository.Update(product);
            await this.producerService.Produce(ProductUpdatesTopicName, product);
        }
    }
}