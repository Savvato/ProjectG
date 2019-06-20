namespace ProjectG.ProductService.Infrastructure.Kafka.Interfaces
{
    using System.Threading.Tasks;

    public interface IProducerService
    {
        Task Produce<TMessage>(string topic, TMessage messageModel);
    }
}