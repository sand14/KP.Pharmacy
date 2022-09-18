using Auth.ViewModels;
using Auth.Views;
using KP.WPF.App.APIClient;
using KP.WPF.App.APIClient.RestServices;
using KP.WPF.HomeModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Auth
{
    public class AuthModule : IModule
    {


        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Login>("Login");
            
            
        }

        
    }
}