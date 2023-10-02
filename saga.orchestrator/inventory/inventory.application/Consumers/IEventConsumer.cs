namespace inventory.application.Consumers
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}
