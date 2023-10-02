using sale.domain.Common;
using sale.domain.Entities;
using sale.domain.Enums;

namespace sale.domain.Events
{
    public class CreatedSaleEvent : BaseEvent
    {
        public CreatedSaleEvent(Sale sale)
        {
            Sale = sale;
        }

        public Sale Sale { get; }
    }
}
