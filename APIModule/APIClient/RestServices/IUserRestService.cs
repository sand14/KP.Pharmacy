﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.WPF.App.APIClient.RestServices
{
    public interface IUserRestService
    {
        Task<bool> Login(string username, string password);
    }
}
