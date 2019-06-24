namespace ProjectG.OrderService.Infrastructure.RabbitMQ
{
    using System.Text;

    using global::RabbitMQ.Client;

    using Microsoft.Extensions.Configuration;

    using ProjectG.OrderService.Infrastructure.RabbitMQ.Interfaces;

    public class QueuePublisher : IQueuePublisher
    {
        private readonly ConnectionFactory connectionFactory;

        public QueuePublisher(IConfiguration configuration)
        {
            this.connectionFactory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };
        }

        public void Publish(string queueName, string message)
        {
            using (IConnection connection = this.connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false);

                    byte[] messageBody = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "", 
                        routingKey: queueName, 
                        basicProperties: null, 
                        body: messageBody);
                }
            }
        }
    }
}