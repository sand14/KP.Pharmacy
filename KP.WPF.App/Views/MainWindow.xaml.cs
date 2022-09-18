using Auth.Views;
using KP.WPF.HomeModule.Views;
using Prism.Ioc;
using Prism.Regions;
using System.Windows;

namespace KP.WPF.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IContainerExtension _container;
        IRegionManager _regionManager;
        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;
            //_regionManager.RegisterViewWithRegion("HomeRegion", typeof(Home));
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(Login));
            
            
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(Home));
        }

       
    }
}
