using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Meal
    {
        public Meal()
        {
            Ingridient = new HashSet<Ingridient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ingridient> Ingridient { get; set; }
    }
}
