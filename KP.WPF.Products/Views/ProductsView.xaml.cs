using KP.WPF.Core.Models;
using System.Windows.Controls;

namespace KP.WPF.Products.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
        }

        private void TableView_AddingNewRow(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            e.NewObject = new ProductModel()
            {
                Stock = new StockModel(),

            };

        }
    }
}
