namespace inventory.application.Producers
{
    public interface IEventProducer
    {
        Task ProduceAsync<T>(string topic, T @event);
    }
}

