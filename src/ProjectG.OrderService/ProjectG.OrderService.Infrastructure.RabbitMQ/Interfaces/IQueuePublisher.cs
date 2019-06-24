namespace ProjectG.OrderService.Infrastructure.RabbitMQ.Interfaces
{
    public interface IQueuePublisher
    {
        void Publish(string queueName, string message);
    }
}