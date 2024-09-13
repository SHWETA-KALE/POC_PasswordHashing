using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HasingBcryptLogin.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }

        public virtual string PasswordHash { get; set; }
    }
}