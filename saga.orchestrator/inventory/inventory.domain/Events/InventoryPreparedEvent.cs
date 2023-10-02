using inventory.domain.Common;
using inventory.domain.Entities;
using inventory.domain.Enums;

namespace inventory.domain.Events
{
    public class InventoryPreparedEvent : BaseEvent
    {
        public InventoryPreparedEvent(Sale sale)
        {
            Sale = sale;
        }

        public Sale Sale { get; }
        public string EventName => this.GetType().Name;
    }
}
