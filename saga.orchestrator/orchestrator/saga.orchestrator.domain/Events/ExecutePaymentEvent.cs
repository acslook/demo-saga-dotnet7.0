using saga.orchestrator.domain.Common;
using saga.orchestrator.domain.Entities;

namespace saga.orchestrator.domain.Events
{
    public class ExecutePaymentEvent : BaseEvent
    {
        public ExecutePaymentEvent(Sale sale)
        {
            Sale = sale;
        }

        public Sale Sale { get; }
    }
}
