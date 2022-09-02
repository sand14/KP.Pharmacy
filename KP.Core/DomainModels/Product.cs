using System;
using System.Collections.Generic;

namespace KP.Core.DomainModels
{
    public partial class Product
    {
        public Product()
        {
            Stocks = new HashSet<Stock>();
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string Producer { get; set; } = null!;

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
