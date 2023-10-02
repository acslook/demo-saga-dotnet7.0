using saga.orchestrator.domain.Common;
using saga.orchestrator.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saga.orchestrator.domain.Entities
{
    public class Sale : BaseEntity
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public long UserId { get; set; }

        public decimal Value { get; set; }

        public SaleStatus Status { get; set; }

        public virtual string StatusName => Enum.GetName(typeof(SaleStatus), Status);

        public int Quantity { get; set; }
    }
}
