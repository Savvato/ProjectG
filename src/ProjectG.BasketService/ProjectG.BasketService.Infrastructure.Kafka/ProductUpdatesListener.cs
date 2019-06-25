namespace ProjectG.BasketService.Infrastructure.Kafka
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Confluent.Kafka;

    using Core;

    using DTO;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    public class ProductUpdatesListener : BackgroundService
    {
        private const string ProductUpdatesTopicName = "product-updates-topic";

        private readonly ICommandHandler<ProductUpdatedEventModel> productUpdatesHandler;
        private readonly IConfiguration configuration;
        private readonly ILogger<ProductUpdatesListener> logger;

        public ProductUpdatesListener(
            IConfiguration configuration, 
            ILogger<ProductUpdatesListener> logger, 
            ICommandHandler<ProductUpdatedEventModel> productUpdatesHandler)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.productUpdatesHandler = productUpdatesHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("ProductUpdatesListener has started");

            ConsumerConfig config = new ConsumerConfig
            {
                GroupId = "basket-api-consumers",
                BootstrapServers = this.configuration["Kafka:Servers"],
                SaslUsername = this.configuration["Kafka:Username"],
                SaslPassword = this.configuration["Kafka:Password"],
                EnableAutoCommit = false
            };

            using (IConsumer<Null, string> consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe(ProductUpdatesTopicName);

                while (!stoppingToken.IsCancellationRequested)
                {
                    ConsumeResult<Null, string> result = null;
                    try
                    {
                        result = consumer.Consume(stoppingToken);
                        consumer.Commit(result);
                    }
                    catch (ConsumeException exception)
                    {
                        this.logger.LogError(exception, $"Error occured: {exception.Error.Reason}");
                    }

                    if (result != null)
                    {
                        ProductUpdatedEventModel eventModel = JsonConvert.DeserializeObject<ProductUpdatedEventModel>(result.Value);
                        try
                        {
                            await this.productUpdatesHandler.Execute(eventModel);
                        }
                        catch (Exception exception)
                        {
                            this.logger.LogError(exception, "Exception during products updates handling");
                        }
                    }
                }
            }

            this.logger.LogInformation("ProductUpdatesListener has finished");
        }
    }
}
