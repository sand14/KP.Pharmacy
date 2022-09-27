using APIModule;
using Auth;
using KP.WPF.App.APIClient;
using KP.WPF.App.Views;
using KP.WPF.HomeModule;
using KP.WPF.HomeModule.Views;
using KP.WPF.Products;
using KP.WPF.Users;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using KP.WPF.Auth.Views;
using KP.WPF.Stocks;

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

            //containerRegistry.RegisterForNavigation<Login,LoginViewModel>();
            ////containerRegistry.Register<UserRestService>();
            //containerRegistry.RegisterForNavigation<Home, HomeViewModel>();
            containerRegistry.Register<IClientApplicationConfiguration, ApplicationConfiguration>();
            containerRegistry.RegisterForNavigation<Login>("Login");
            containerRegistry.RegisterForNavigation<Home>("Home");


        }



        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {

            moduleCatalog.AddModule<AuthModule>();
            moduleCatalog.AddModule(typeof(HomeModuleModule), InitializationMode.OnDemand);
            moduleCatalog.AddModule<APIModuleModule>();
            moduleCatalog.AddModule<ProductsModule>();
            moduleCatalog.AddModule<UsersModule>();
            moduleCatalog.AddModule<StocksModule>();

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(Login));
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(Home));

        }
    }
}
