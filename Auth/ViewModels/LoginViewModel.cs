using KP.WPF.App.APIClient.RestServices;
using KP.WPF.Core.Models;
using KP.WPF.HomeModule.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;

namespace Auth.ViewModels
{
    public partial class LoginViewModel : BindableBase
    {

        private readonly UserRestService userRestService;
        IEventAggregator ea;

        private string _title = "Login dialog";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private bool success;

        private Visibility _MessageVisibilty;
        public Visibility MessageVisibilty { get { return _MessageVisibilty; } set { SetProperty(ref _MessageVisibilty, value); } }



        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        public DelegateCommand LoginCommand { get; set; }

        public RegionManager RegionManager { get; }

        public LoginViewModel(UserRestService userRestService, RegionManager RegionManager, IEventAggregator ea)
        {
            this.userRestService = userRestService;

            this.RegionManager = RegionManager;
            this.ea = ea;
            MessageVisibilty = Visibility.Collapsed;
            LoginCommand = new DelegateCommand(OnLoginAsync);
            ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(string message)
        {
            if (message == "Logout")
            {
                userRestService.Logout();
                Username = "";
            }
        }

        private async void OnLoginAsync()
        {
            success = await userRestService.Login(Username, Password);
            if (success)
            {
                UserModel user = await userRestService.GetUser(Username);
                
                if (user.IsAdmin)
                    ea.GetEvent<MessageSentEvent>().Publish("Admin");
                else
                    ea.GetEvent<MessageSentEvent>().Publish("NonAdmin");
            }
            else
                MessageVisibilty = Visibility.Visible;
        }
        

    }
}
