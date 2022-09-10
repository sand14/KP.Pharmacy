using System;
using System.Collections.Generic;
using KP.Core.Data;

namespace KP.Core.DomainModels
{
    public partial class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
