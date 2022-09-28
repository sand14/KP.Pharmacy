using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using KP.WPF.APIModule.APIClient.RestServices;
using KP.WPF.Core.Models;
using Prism.Regions;
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace KP.WPF.Stocks.ViewModels
{
    public class StocksViewModel : ViewModelBase, INavigationAware
    {
        private ObservableCollection<ProductModel> products;
        public ObservableCollection<ProductModel> Products
        {
            get { return products; }
            set { SetProperty(ref products, value, () => Products); }
        }

        private ProductModel selectedProduct;
        public ProductModel SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; }
        }


        private readonly ProductRestService productRestService;

        public DelegateCommand DeleteProductCommand { get; private set; }

        public StocksViewModel(ProductRestService productRestService)
        {
            DeleteProductCommand = new DelegateCommand(DeleteProduct);

            this.productRestService = productRestService;   
            //Task.Run(() => this.Initialize()).Wait();
        }

        private async Task GetProducts()
        {
            Products = new ObservableCollection<ProductModel>(await productRestService.GetAllProductsAsync());
        }
        //private async Task Initialize()
        //{
        //    await GetProducts();
        //}

        private async void DeleteProduct()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a Product");
                return;
            }
            Guid productId = SelectedProduct.ProductId;
            await productRestService.DeleteProductAsync(productId);

            await GetProducts();
        }

        [Command]
        public async void ValidateRow(RowValidationArgs args)
        {

            ProductModel product = (ProductModel)args.Item; 
            await productRestService.UpdateProductAsync(product);
            await GetProducts();
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await GetProducts();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
