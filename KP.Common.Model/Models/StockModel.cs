namespace KP.Common.Model.Models
{
    public class StockModel
    {
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }

        public ProductModel? Product { get; set; }

    }
}
