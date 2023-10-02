using MediatR;
using saga.orchestrator.domain.Enums;

namespace saga.orchestrator.domain.Common
{
    public abstract class BaseEvent : INotification
    {
        public string EventName => this.GetType().Name;
    }
}
