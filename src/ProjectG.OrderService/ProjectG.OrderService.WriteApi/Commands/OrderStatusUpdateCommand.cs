namespace ProjectG.OrderService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using ProjectG.Core;
    using ProjectG.OrderService.Core.Models;
    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.WriteApi.DTO;

    public class OrderStatusUpdateCommand : ICommandHandler<OrderStatusUpdateEventModel>
    {
        private readonly IOrderRepository orderRepository;

        public OrderStatusUpdateCommand(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task Execute(OrderStatusUpdateEventModel commandData)
        {
            Order order = await this.orderRepository.Get(commandData.OrderId);
            order.Status = commandData.MapStatusToEnum();
            await this.orderRepository.SaveChanges();
        }
    }
}