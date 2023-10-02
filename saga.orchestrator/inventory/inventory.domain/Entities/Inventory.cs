using inventory.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.domain.Entities
{
    public class Inventory : BaseEntity
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public void DebitQuantity(int quantity)
        {
            this.Quantity -= quantity;
        }

        public void CreditQuantity(int quantity)
        {
            this.Quantity += quantity;
        }
    }
}
