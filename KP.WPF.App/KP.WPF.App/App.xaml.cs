using KP.WPF.App.Modules.ModuleName;
using KP.WPF.App.Services;
using KP.WPF.App.Services.Interfaces;
using KP.WPF.App.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace KP.WPF.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
