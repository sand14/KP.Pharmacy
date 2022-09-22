using Auth;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace KP.WPF.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        IEventAggregator ea;

        public MainWindowViewModel(IEventAggregator ea, IRegionManager RegionManager)
        {
            _regionManager = RegionManager;
            this.ea = ea;
            ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);

        }

        private void MessageReceived(string message)
        {
            if (message == "Admin" || message == "NonAdmin")
            {
                _regionManager.RequestNavigate("ContentRegion", "Home");
                //RegionManager.Regions["ContentRegion"].RemoveAll();
                //RegionManager.Regions["ContentRegion"].Add("Home");
                //LoginVisibility = Visibility.Hidden;
                //HomeVisibility = Visibility.Visible;
            }
            if (message == "Logout")
            {
                _regionManager.RequestNavigate("ContentRegion", "Login");
            }
        }
    }
}
