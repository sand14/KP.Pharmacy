using KP.Core.Data;
using System;
using System.Collections.Generic;

namespace KP.Core.DomainModels
{
    public partial class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string Producer { get; set; } = null!;

        public virtual Stock Stock { get; set; } = null!;
    }
}
