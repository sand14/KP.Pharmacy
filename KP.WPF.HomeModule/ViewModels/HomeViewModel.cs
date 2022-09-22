using Auth;
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

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        public HomeViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            LogoutCommand = new DelegateCommand(Logout);
            _ea = ea;
            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
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

        private void MessageReceived(string message)
        {
            if (message == "Admin")
            {
                IsEnabled = true;
            }
            if (message == "NonAdmin")
            {
                IsEnabled = false;
            }
            if (message == "Logout")
            {
                _regionManager.Regions["ViewRegion"].RemoveAll();
            }
        }


    }
}
