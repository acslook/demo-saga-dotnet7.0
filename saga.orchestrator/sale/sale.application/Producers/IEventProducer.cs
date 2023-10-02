using sale.domain.Common;

namespace sale.application.Producers
{
    public interface IEventProducer
    {
        Task ProduceAsync<T>(string topic, T @event);
    }
}
