namespace ProjectG.OrderService.OrderBuilder.Background
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class QueueConsumer : BackgroundService
    {
        private const string QueueName = "order-creator-queue";

        private readonly ConnectionFactory connectionFactory;
        private readonly ILogger<QueueConsumer> logger;

        public QueueConsumer(
            IConfiguration configuration, 
            ILogger<QueueConsumer> logger)
        {
            this.logger = logger;
            this.connectionFactory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IConnection connection = this.connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false);

                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, eventArgs) =>
                    {
                        byte[] body = eventArgs.Body;
                        string message = Encoding.UTF8.GetString(body);
                        this.logger.LogInformation($"RABBITMQ: consumpted {message}");
                    };
                    channel.BasicConsume(
                        queue: QueueName, 
                        autoAck: true, 
                        consumer: consumer);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                    }
                }
            }
        }
    }
}