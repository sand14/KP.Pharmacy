﻿namespace KP.Common.Model.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? isAdmin { get; set; }
    }
}
