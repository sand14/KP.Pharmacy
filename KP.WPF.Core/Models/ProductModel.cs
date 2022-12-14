using Prism.Mvvm;

namespace KP.WPF.Core.Models
{
    public class ProductModel : BindableBase
    {
        private Guid productId;
        public Guid ProductId
        {
            get { return productId; }
            set { SetProperty(ref productId, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        private string producer;
        public string Producer
        {
            get { return producer; }
            set { SetProperty(ref producer, value); }
        }

        private StockModel stock;
        public StockModel Stock
        {
            get { return stock; }
            set
            {
                SetProperty(ref stock, value);
            }
        }
    }
}
