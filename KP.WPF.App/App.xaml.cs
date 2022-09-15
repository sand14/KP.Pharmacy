using APIModule;
using Auth;
using Auth.ViewModels;
using Auth.Views;
using KP.WPF.App.APIClient;
using KP.WPF.App.APIClient.RestServices;
using KP.WPF.App.ViewModels;
using KP.WPF.App.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using Unity.Lifetime;

namespace KP.WPF.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Login,LoginViewModel>();
            //containerRegistry.Register<UserRestService>();
            containerRegistry.Register<IHttpClientFactory,HttpClientFactory>();
            containerRegistry.Register<IClientApplicationConfiguration,ApplicationConfiguration>();
            
        }

       

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AuthModule>();
            moduleCatalog.AddModule<APIModuleModule>();
        }
    }
}
