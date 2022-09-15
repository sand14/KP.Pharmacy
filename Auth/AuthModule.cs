using Auth.ViewModels;
using Auth.Views;
using KP.WPF.App.APIClient;
using KP.WPF.App.APIClient.RestServices;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Auth
{
    public class AuthModule : IModule
    {


        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();
            region.RegisterViewWithRegion("ContentRegion",typeof(Login));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Login>();
            containerRegistry.Register<LoginViewModel>();
        }
    }
}