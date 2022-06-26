using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Member
    {
        public Member()
        {
           
        }
        public int Id { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Goal { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Gender { get; set; }
    }
}
