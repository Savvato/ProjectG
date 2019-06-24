namespace ProjectG.OrderService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using ProjectG.Core;
    using ProjectG.OrderService.Infrastructure.RabbitMQ.Interfaces;
    using ProjectG.OrderService.WriteApi.DTO;

    public class CreateOrderCommand : ICommandHandler<OrderCreationModel>
    {
        private const string QueueName = "order-creator-queue";

        private readonly IQueuePublisher queuePublisher;

        public CreateOrderCommand(IQueuePublisher queuePublisher)
        {
            this.queuePublisher = queuePublisher;
        }

        public Task Execute(OrderCreationModel commandData)
        {
            string message = JsonConvert.SerializeObject(commandData);
            this.queuePublisher.Publish(QueueName, message);
            return Task.CompletedTask;
        }
    }
}
