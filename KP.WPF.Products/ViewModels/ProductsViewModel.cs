using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using KP.WPF.APIModule.APIClient.RestServices;
using KP.WPF.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prism.Regions;
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace KP.WPF.Products.ViewModels
{
    public class ProductsViewModel : ViewModelBase, INavigationAware
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
        }

        private async Task GetProducts()
        {
            Products = new ObservableCollection<ProductModel>(await productRestService.GetAllProductsAsync());
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
            if (product.ProductId == Guid.Empty)
            {
                ProductModel createdProduct = await productRestService.CreateProductAsync(product);
                if (createdProduct == null || createdProduct.ProductId == Guid.Empty)
                {
                    MessageBox.Show("Error adding product, Name and Producer should be valid");
                }
            }
            else
            {
                ProductModel updatedProduct = await productRestService.UpdateProductAsync(product);
                if (updatedProduct == null || updatedProduct.ProductId == Guid.Empty)
                    MessageBox.Show("Error updating product, Name and Producer should be valid");
            }



            await GetProducts();
        }
    }
}
