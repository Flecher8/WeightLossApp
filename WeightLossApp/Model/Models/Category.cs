using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Category
    {
        public Category()
        {
            IngridientCategory = new HashSet<IngridientCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Danger { get; set; }

        public virtual ICollection<IngridientCategory> IngridientCategory { get; set; }
    }
}
