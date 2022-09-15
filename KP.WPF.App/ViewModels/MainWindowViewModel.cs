using Auth.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
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

        public DelegateCommand<string> NavigateCommand { get; private set; }

        IEventAggregator ea;

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            this.ea = ea;

            
            
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
    }
}
