using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.Common.Model.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? Producer { get; set; }
    }
}
