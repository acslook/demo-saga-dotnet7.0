using inventory.domain.Common;
using inventory.domain.Entities;
using inventory.domain.Enums;

namespace inventory.domain.Events
{
    public class CreatedSaleEvent : BaseEvent
    {
        public CreatedSaleEvent()
        {
        }

        public Sale Sale { get; set; }
        public string EventName => this.GetType().Name;
    }
}
