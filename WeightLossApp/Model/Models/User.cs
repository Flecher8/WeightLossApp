using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}
