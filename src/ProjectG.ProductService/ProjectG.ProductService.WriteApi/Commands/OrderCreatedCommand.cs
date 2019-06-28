namespace ProjectG.ProductService.WriteApi.Commands
{
    using System.Linq;
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using ProjectG.Core;
    using ProjectG.ProductService.Core.Models;
    using ProjectG.ProductService.Infrastructure.Interfaces;
    using ProjectG.ProductService.Infrastructure.OrderApi.DTO;
    using ProjectG.ProductService.WriteApi.DTO;

    public class OrderCreatedCommand : ICommandHandler<OrderCreatedEventModel>, ICapSubscribe
    {
        private const string OrderCreatedTopicName = "order.created";
        private const string ProductsReservedTopicName = "product.reserved";

        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly ICapPublisher eventBus;

        public OrderCreatedCommand(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICapPublisher eventBus)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.eventBus = eventBus;
        }

        [CapSubscribe(OrderCreatedTopicName)]
        public async Task Execute(OrderCreatedEventModel commandData)
        {
            OrderModel order = await this.orderRepository.Get(commandData.OrderId);

            await Task.WhenAll(order.OrderPositions.Select(this.UpdateProduct));

            await eventBus.PublishAsync(
                name: ProductsReservedTopicName,
                contentObj: new ProductsAreReservedEventModel
                {
                    OrderId = commandData.OrderId
                });
        }

        private async Task UpdateProduct(OrderPositionModel orderPosition)
        {
            Product product = await this.productRepository.Get(orderPosition.ProductId);
            product.Count -= orderPosition.Count;
            await this.productRepository.Update(product);
        }

        private class ProductsAreReservedEventModel
        {
            public long OrderId { get; set; }
        }
    }
}