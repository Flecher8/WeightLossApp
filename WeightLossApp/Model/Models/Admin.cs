using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Tier { get; set; }
    }
}
