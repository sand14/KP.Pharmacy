using Auth;
using Auth.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using Unity;

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

        private Visibility _LoginVisibilty;
        public Visibility LoginVisibilty { get { return _LoginVisibilty; } set { SetProperty(ref _LoginVisibilty, value); } }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        IEventAggregator ea;

        public MainWindowViewModel(IEventAggregator ea)
        {
            
            
            NavigateCommand = new DelegateCommand<string>(Navigate);
            this.ea = ea;
            ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);


        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }

        private void MessageReceived(string message)
        {
            if (message == "Admin")
            {
                LoginVisibilty = Visibility.Hidden;
            }
            if (message == "NonAdmin")
            {
                LoginVisibilty = Visibility.Hidden;
            }
            
        }
    }
}
