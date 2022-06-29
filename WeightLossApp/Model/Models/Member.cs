using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Member
    {
        public Member()
        {
            Profile = new HashSet<Profile>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Goal { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
