using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class IngridientData
    {
        public IngridientData()
        {
            IngridientCategory = new HashSet<IngridientCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Calories { get; set; }
        public double? Proteins { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Fats { get; set; }

        public virtual Ingridient Ingridient { get; set; }
        public virtual ICollection<IngridientCategory> IngridientCategory { get; set; }
    }
}
