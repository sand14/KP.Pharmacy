using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.WPF.App.Models
{
    public class UserModel : BindableBase
    {
       

        public UserModel()
        {
            
        }

        private Guid userId;
        public Guid UserId 
        { 
            get { return userId; } 
            set { SetProperty(ref userId, value); }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
            }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                SetProperty(ref isAdmin, value);
            }
        }
    }
}
