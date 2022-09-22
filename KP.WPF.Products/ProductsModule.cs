using KP.WPF.Products.ViewModels;
using KP.WPF.Products.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace KP.WPF.Products
{
    public class ProductsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProductsView>("ProductsView");
            containerRegistry.Register<ProductsViewModel>();
        }
    }
}