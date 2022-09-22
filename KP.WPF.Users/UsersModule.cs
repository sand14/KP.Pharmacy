using KP.WPF.Users.ViewModels;
using KP.WPF.Users.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace KP.WPF.Users
{
    public class UsersModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UsersView, UsersViewModel>("UsersView");
        }
    }
}