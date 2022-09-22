using KP.WPF.Auth.ViewModels;
using KP.WPF.Auth.Views;
using Prism.Ioc;
using Prism.Modularity;

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
            containerRegistry.Register<LoginViewModel>();


        }


    }
}