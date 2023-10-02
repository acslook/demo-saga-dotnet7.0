using sale.domain.Common;
using sale.domain.Enums;
using System.Text.RegularExpressions;

namespace sale.domain.Entities
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
