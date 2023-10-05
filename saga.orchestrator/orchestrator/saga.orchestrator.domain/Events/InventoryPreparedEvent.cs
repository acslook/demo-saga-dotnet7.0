using saga.orchestrator.domain.Common;
using saga.orchestrator.domain.Entities;

namespace saga.orchestrator.domain.Events
{
    public class InventoryPreparedEvent : BaseEvent
    {
        public InventoryPreparedEvent(Sale sale)
        {
            Sale = sale;
        }

        public Sale Sale { get; }
    }
}
