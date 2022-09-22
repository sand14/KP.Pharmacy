using Prism.Mvvm;

namespace KP.WPF.Core.Models
{
    public class StockModel : BindableBase
    {
        private Guid stockId;
        public Guid StockId
        {
            get { return stockId; }
            set { SetProperty(ref stockId, value); }
        }

        private Guid productId;
        public Guid ProductId
        {
            get { return productId; }
            set { SetProperty(ref productId, value); }
        }

        private float quantity;
        public float Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        private ProductModel product;
        public ProductModel Product
        {
            get { return product; }
            set { SetProperty(ref product, value); }
        }

    }
}
