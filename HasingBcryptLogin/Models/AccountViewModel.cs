using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HasingBcryptLogin.Models
{
    public class AccountViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsCreatingUser { get; set; } // Determines whether the form is for creating a new user
    }
}