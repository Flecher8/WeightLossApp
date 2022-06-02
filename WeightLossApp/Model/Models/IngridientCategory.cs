using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class IngridientCategory
    {
        public int IngridientId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual IngridientData Ingridient { get; set; }
    }
}
