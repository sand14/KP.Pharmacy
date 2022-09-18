using Auth;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.WPF.HomeModule.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        IEventAggregator ea;

        bool isEnabled;

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public HomeViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            this.ea = ea;
            ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ViewRegion", navigatePath);
        }

        private void MessageReceived(string message)
        {
            if (message == "Admin")
            {
                IsEnabled = true;
                
            }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }
    }
}
