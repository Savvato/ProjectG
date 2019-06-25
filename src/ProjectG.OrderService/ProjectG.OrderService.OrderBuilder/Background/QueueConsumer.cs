namespace ProjectG.OrderService.OrderBuilder.Background
{
    using System;
    using System.Runtime.CompilerServices;
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

        private IConnection connection;
        private IModel channel;

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

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.connection = this.connectionFactory.CreateConnection();
            this.channel = this.connection.CreateModel();

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Queue Consumer: Stop is called");

            this.channel?.Dispose();
            this.connection?.Dispose();

            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Queue Consumer starts");

            this.channel.QueueDeclare(queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += (sender, eventArgs) =>
            {
                byte[] body = eventArgs.Body;
                string message = Encoding.UTF8.GetString(body);
                this.logger.LogInformation($"RABBITMQ: consumpted {message}");
            };
            this.channel.BasicConsume(
                queue: QueueName,
                autoAck: true,
                consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);

            this.logger.LogInformation("Queue Consumer finishes");
        }
    }
}