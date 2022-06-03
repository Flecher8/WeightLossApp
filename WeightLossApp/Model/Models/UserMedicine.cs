using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class UserMedicine
    {
        public int MedicineId { get; set; }
        public int ProfileId { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
