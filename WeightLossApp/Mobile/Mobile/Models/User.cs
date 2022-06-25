using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public partial class User
    {
        public User()
        {

        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
    }
}
