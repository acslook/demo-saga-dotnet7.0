using MediatR;

namespace sale.domain.Common
{
    public abstract class BaseEvent : INotification
    {
        public string EventName => this.GetType().Name;
    }
}
