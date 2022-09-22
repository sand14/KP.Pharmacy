using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using KP.WPF.App.APIClient.RestServices;
using KP.WPF.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace KP.WPF.Users.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {

        private readonly UserRestService userRestService;

        
        public DelegateCommand DeleteUserCommand { get; private set; }
        

        private ObservableCollection<UserModel> users;
        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set { SetProperty(ref users, value, () => Users); }
        }

        private UserModel selectedUser;
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; }
        }

        public UsersViewModel(UserRestService userRestService)
        {
            DeleteUserCommand = new DelegateCommand(DeleteUser);
            this.userRestService = userRestService;
            Task.Run(() => this.Initialize()).Wait();
        }

        private async Task GetUsers()
        {
            Users = new ObservableCollection<UserModel>(await userRestService.GetAllUsersAsync());
        }

        private async Task Initialize()
        {
            await GetUsers();
        }

        

        private async void DeleteUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select an User");
                return;
            }
            Guid userId = SelectedUser.UserId;
            await userRestService.DeleteUserAsync(userId);

            await GetUsers();
        }
        
        [Command]
        public async void ValidateRow(RowValidationArgs args)
        {

            UserModel user = (UserModel)args.Item;
            if (user.UserId == Guid.Empty)
            {
                await userRestService.CreateUserAsync(user);
            }
            else
            {
                await userRestService.UpdateUserAsync(user.UserId, user);
            }



            await GetUsers();
        }

    }
}
