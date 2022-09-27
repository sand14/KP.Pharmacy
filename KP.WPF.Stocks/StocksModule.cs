using KP.WPF.Stocks.ViewModels;
using KP.WPF.Stocks.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace KP.WPF.Stocks
{
    public class StocksModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StocksView>("StocksView");
            containerRegistry.Register<StocksViewModel>();
        }
    }
}