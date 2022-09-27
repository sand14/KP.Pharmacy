using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using KP.WPF.APIModule.APIClient.RestServices;
using KP.WPF.App.APIClient.RestServices;
using KP.WPF.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace KP.WPF.HomeModule.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        IEventAggregator _ea;

        private readonly ProductRestService productRestService;

        private readonly UserRestService userRestService;

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        private ObservableCollection<ProductModel> products;
        public ObservableCollection<ProductModel> Products
        {
            get { return products; }
            set { SetProperty(ref products, value); }
        }

        private ObservableCollection<UserModel> users;
        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        private int numberOfProducts;

        public int NumberOfProducts
        {
            get { return numberOfProducts; }
            set { SetProperty(ref numberOfProducts, value); }
        }

        private int numberOfUsers;

        public int NumberOfUsers
        {
            get { return numberOfUsers;  }
            set { SetProperty(ref numberOfUsers, value); }
        }

        private int numberOfLowStockProducts;

        public int NumberOfLowStockProducts
        {
            get { return numberOfLowStockProducts; }
            set { SetProperty(ref numberOfLowStockProducts, value); }
        }



        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand HomeCommand { get; private set; }

        public HomeViewModel(IRegionManager regionManager, IEventAggregator ea, ProductRestService productRestService, UserRestService userRestService)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            LogoutCommand = new DelegateCommand(Logout);
            HomeCommand = new DelegateCommand(Home);
            this.productRestService = productRestService;
            this.userRestService = userRestService;
            _ea = ea;
            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private async Task Initialize()
        {
            await GetProducts();
            if (isEnabled == true)
            {
                await GetUsers();
            }
        }

        private async Task GetProducts()
        {
            Products = new ObservableCollection<ProductModel>(await productRestService.GetAllProductsAsync());
            NumberOfProducts = Products.Count;
            NumberOfLowStockProducts = Products.Count(x=>x.Stock.Quantity<5);
        }
        private async Task GetUsers()
        {
            Users = new ObservableCollection<UserModel>(await userRestService.GetAllUsersAsync());
            NumberOfUsers = Users.Count;
        }


        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ViewRegion", navigatePath);
        }

        public void Logout()
        {
            _ea.GetEvent<MessageSentEvent>().Publish("Logout");
        }

        public void Home()
        {
            _regionManager.Regions["ViewRegion"].RemoveAll();
            Task.Run(() => this.Initialize()).Wait();
        }

        private void MessageReceived(string message)
        {
            if (message == "Admin")
            {
                IsEnabled = true;
                Task.Run(() => this.Initialize()).Wait();
            }
            if (message == "NonAdmin")
            {
                IsEnabled = false;
                Task.Run(() => this.Initialize()).Wait();
            }
            if (message == "Logout")
            {
                _regionManager.Regions["ViewRegion"].RemoveAll();
            }
            if (message == "AdminDisabled")
            {
                _regionManager.Regions["ViewRegion"].RemoveAll();
                IsEnabled = false;
                Task.Run(() => this.Initialize()).Wait();
            }

        }


    }
}
