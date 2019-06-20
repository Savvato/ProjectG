namespace ProjectG.ProductService.Infrastructure.Kafka
{
    using System;
    using System.Threading.Tasks;

    using Confluent.Kafka;

    using Microsoft.Extensions.Configuration;

    using Newtonsoft.Json;

    using ProjectG.ProductService.Infrastructure.Kafka.Interfaces;

    public class ProducerService : IProducerService
    {
        private readonly IConfiguration configuration;

        public ProducerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Produce<TMessage>(string topic, TMessage messageModel)
        {
            ProducerConfig config = new ProducerConfig()
            {
                BootstrapServers = configuration["Kafka:Servers"],
                SaslUsername = configuration["Kafka:Username"],
                SaslPassword = configuration["Kafka:Password"],
            };

            using (IProducer<Null, string> producer = new ProducerBuilder<Null, string>(config).Build())
            {
                string serializedModel = JsonConvert.SerializeObject(messageModel);
                await producer.ProduceAsync(
                    topic: topic, 
                    message: new Message<Null, string>
                    {
                        Value = serializedModel
                    });
                producer.Flush(TimeSpan.FromSeconds(5));
            }
        }
    }
}