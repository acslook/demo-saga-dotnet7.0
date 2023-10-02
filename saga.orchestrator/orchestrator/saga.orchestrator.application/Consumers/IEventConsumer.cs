namespace saga.orchestrator.application.Consumers
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}
