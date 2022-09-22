using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using KP.WPF.APIModule.APIClient.RestServices;
using KP.WPF.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Mvvm;
using KP.WPF.App.APIClient.RestServices;
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace KP.WPF.Products.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private readonly ProductRestService productRestService;

        

        
        public DelegateCommand DeleteProductCommand { get; private set; }
        

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



        public ProductsViewModel(ProductRestService productRestService)
        {
            
            DeleteProductCommand = new DelegateCommand(DeleteProduct);
            
            this.productRestService = productRestService;
            Task.Run(() => this.Initialize()).Wait();
        }

        private async Task GetProducts()
        {
            Products = new ObservableCollection<ProductModel>(await productRestService.GetAllProductsAsync());
        }

        private async Task Initialize()
        {
            await GetProducts();
        }

        

       

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
            if(product.ProductId == Guid.Empty)
            {
                await productRestService.CreateProductAsync(product);
            }
            else
            {
                await productRestService.UpdateProductAsync(product.ProductId, product);
            }



            await GetProducts();
        }




    }
}
