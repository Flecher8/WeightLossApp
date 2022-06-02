using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            UserMedicine = new HashSet<UserMedicine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfMedicine { get; set; }
        public string Dose { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserMedicine> UserMedicine { get; set; }
    }
}
