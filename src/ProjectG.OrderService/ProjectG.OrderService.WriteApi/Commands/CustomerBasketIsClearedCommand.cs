namespace ProjectG.OrderService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using ProjectG.Core;
    using ProjectG.OrderService.Core.Models;
    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.WriteApi.DTO;

    public class CustomerBasketIsClearedCommand : ICommandHandler<CustomerBasketIsClearedEventModel>, ICapSubscribe
    {
        private const string BasketIsClearedTopicName = "basket.cleared";

        private readonly IOrderRepository orderRepository;

        public CustomerBasketIsClearedCommand(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [CapSubscribe(BasketIsClearedTopicName)]
        public async Task Execute(CustomerBasketIsClearedEventModel commandData)
        {
            Order order = await this.orderRepository.Get(commandData.OrderId);
            order.StatusDetails.IsBasketCleared = true;

            // Different transactions
            if (order.StatusDetails.IsReadyForPayments)
            {
                order.Status = OrderStatus.WaitingForPayment;
            }

            await this.orderRepository.SaveChanges();
        }
    }
}