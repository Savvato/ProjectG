namespace ProjectG.OrderService.WriteApi.Commands
{
    using System.IO;
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using Microsoft.Extensions.Logging;

    using ProjectG.Core;
    using ProjectG.OrderService.Core.Models;
    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.WriteApi.DTO;

    public class CustomerBasketIsClearedCommand : ICommandHandler<CustomerBasketIsClearedEventModel>, ICapSubscribe
    {
        private const string BasketIsClearedTopicName = "basket.cleared";

        private readonly IOrderRepository orderRepository;

        private readonly ILogger<CustomerBasketIsClearedCommand> logger;

        public CustomerBasketIsClearedCommand(
            IOrderRepository orderRepository, 
            ILogger<CustomerBasketIsClearedCommand> logger)
        {
            this.orderRepository = orderRepository;
            this.logger = logger;
        }

        [CapSubscribe(BasketIsClearedTopicName)]
        public async Task Execute(CustomerBasketIsClearedEventModel commandData)
        {
            this.logger.LogInformation($"{BasketIsClearedTopicName} event is being handled. OrderID: {commandData.OrderId}");

            Order order = await this.orderRepository.Get(commandData.OrderId);

            if (order == null)
            {
                throw new InvalidDataException($"Order with ID: {commandData.OrderId} is not found");
            }

            order.StatusDetails.IsBasketCleared = true;

            if (order.StatusDetails.IsReadyForPayments)
            {
                order.Status = OrderStatus.WaitingForPayment;
            }

            await this.orderRepository.SaveChanges();
        }
    }
}