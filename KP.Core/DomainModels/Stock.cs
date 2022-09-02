using System;
using System.Collections.Generic;

namespace KP.Core.DomainModels
{
    public partial class Stock
    {
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
