using System.Windows;
using System.Windows.Controls;

namespace KP.WPF.HomeModule.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }


        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            LogoutButton.IsChecked = false;
            ProductsButton.IsChecked = false;
            UsersButton.IsChecked = false;
            StocksButton.IsChecked = false;
        }

        private void HomeButton_OnChecked(object sender, RoutedEventArgs e)
        {
            HomeButton.IsChecked = false;
            ProductsButton.IsChecked = false;
            UsersButton.IsChecked = false;
            StocksButton.IsChecked = false;
        }
    }
}
