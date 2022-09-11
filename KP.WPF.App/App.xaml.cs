using KP.WPF.App.Views;
using Prism.Ioc;
using Prism.Mvvm;
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

        }

        protected override void OnInitialized()
        {
            var login = Container.Resolve<Login>();
            var result = login.ShowDialog();
            if ((bool)result)
            {
                base.OnInitialized();
            }

        }
    }
}
