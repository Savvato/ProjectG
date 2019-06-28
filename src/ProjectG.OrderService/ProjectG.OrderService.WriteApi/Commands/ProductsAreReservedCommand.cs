namespace ProjectG.OrderService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using ProjectG.Core;
    using ProjectG.OrderService.Core.Models;
    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.WriteApi.DTO;

    public class ProductsAreReservedCommand : ICommandHandler<ProductsAreReservedEventModel>, ICapSubscribe
    {
        private const string ProductsReservedTopicName = "product.reserved";

        private readonly IOrderRepository orderRepository;

        public ProductsAreReservedCommand(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [CapSubscribe(ProductsReservedTopicName)]
        public async Task Execute(ProductsAreReservedEventModel commandData)
        {
            Order order = await this.orderRepository.Get(commandData.OrderId);
            order.StatusDetails.AreProductsReserved = true;

            // Different transactions
            if (order.StatusDetails.IsReadyForPayments)
            {
                order.Status = OrderStatus.WaitingForPayment;
            }
            await this.orderRepository.SaveChanges();
        }
    }
}