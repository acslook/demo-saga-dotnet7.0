using inventory.domain.Common;
using inventory.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.domain.Entities
{
    public class Sale : BaseEntity
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public long UserId { get; set; }

        public decimal Value { get; set; }

        public SaleStatus Status { get; set; }

        public int Quantity { get; set; }
    }
}
