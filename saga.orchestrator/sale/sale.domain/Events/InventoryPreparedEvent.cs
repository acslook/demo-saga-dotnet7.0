using sale.domain.Common;
using sale.domain.Entities;

namespace sale.domain.Events
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
