using inventory.domain.Events;

namespace inventory.application.Consumers
{
    public interface IConsumerEventHandler
    {
        Task On(CreatedSaleEvent @event);
    }
}
