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

    public class OrderCreatedCommand : ICommandHandler<OrderCreatedEventModel>
    {
        private const string OrderCreatedTopicName = "order.created";

        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;


        public OrderCreatedCommand(
            IOrderRepository orderRepository, 
            IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        [CapSubscribe(OrderCreatedTopicName)]
        public async Task Execute(OrderCreatedEventModel commandData)
        {
            OrderModel order = await this.orderRepository.Get(commandData.OrderId);

            await Task.WhenAll(order.OrderPositions.Select(this.UpdateProduct));
        }

        private async Task UpdateProduct(OrderPositionModel orderPosition)
        {
            Product product = await this.productRepository.Get(orderPosition.ProductId);
            product.Count -= orderPosition.Count;
            await this.productRepository.Update(product);
        }
    }
}