namespace ProjectG.OrderService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using ProjectG.Core;
    using ProjectG.OrderService.Core.Models;
    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.WriteApi.DTO;

    public class OrderStatusUpdateCommand : ICommandHandler<OrderStatusUpdateEventModel>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICapPublisher eventBus;

        public OrderStatusUpdateCommand(
            IOrderRepository orderRepository, 
            ICapPublisher eventBus)
        {
            this.orderRepository = orderRepository;
            this.eventBus = eventBus;
        }

        public async Task Execute(OrderStatusUpdateEventModel commandData)
        {
            Order order = await this.orderRepository.Get(commandData.OrderId);
            order.Status = commandData.MapStatusToEnum();
            await this.orderRepository.SaveChanges();

            switch (order.Status)
            {
                case OrderStatus.Paid:
                    await this.eventBus.PublishAsync("order.status.paid", commandData);
                    break;
                case OrderStatus.Sent:
                    await this.eventBus.PublishAsync("order.status.sent", commandData);
                    break;
            }
        }
    }
}