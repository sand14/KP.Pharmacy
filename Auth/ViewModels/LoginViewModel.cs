using Auth.Views;
using CommonServiceLocator;
using KP.WPF.App.APIClient.RestServices;
using KP.WPF.Core.Models;
using KP.WPF.HomeModule;
using KP.WPF.HomeModule.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Prism.Unity;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace Auth.ViewModels
{
    public partial class LoginViewModel :  BindableBase
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
            
            public DelegateCommand<string> NavigateCommand { get; private set; }



        public RegionManager RegionManager { get; }
        public IUnityContainer UnityContainer { get; set; }
        IModuleManager _moduleManager;
        IModuleCatalog _moduleCatalog;

        public LoginViewModel(IModuleCatalog moduleCatalog, IModuleManager moduleManager, UserRestService userRestService, RegionManager RegionManager,IUnityContainer unityContainer, IEventAggregator ea)
            {
                this.userRestService = userRestService;
                NavigateCommand = new DelegateCommand<string>(Navigate);
                
                
                this.RegionManager = RegionManager;
                _moduleManager = moduleManager;
                _moduleCatalog = moduleCatalog;
                this.ea = ea;
                
                MessageVisibilty = Visibility.Collapsed;
                LoginCommand = new DelegateCommand(OnLoginAsync);
            }


        private void Navigate(string navigatePath)
            {
            if (navigatePath != null)
                RegionManager.RequestNavigate("ContentRegion", navigatePath);
            }

        IRegion _region;
        private async void OnLoginAsync()
            {
                success = await userRestService.Login(Username, Password);
            if (success)
            {


                UserModel user = await userRestService.GetUser(Username);
                RegionManager.RegisterViewWithRegion("HomeRegion", typeof(Home));
                if (user.IsAdmin)
                    ea.GetEvent<MessageSentEvent>().Publish("Admin");
                else
                    ea.GetEvent<MessageSentEvent>().Publish("NonAdmin");
                
                
                
                
                // var view = region.Single(v => v.GetType().Name == requests[i].ViewName);
                //region.Deactivate(view);


            }
            else
                MessageVisibilty = Visibility.Visible;


            }

        
    }
}
