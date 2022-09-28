namespace KP.Common.Model.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? Producer { get; set; }

        public virtual StockModel? Stock { get; set; }

    }
}
