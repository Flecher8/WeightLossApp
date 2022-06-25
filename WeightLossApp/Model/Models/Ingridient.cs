using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Ingridient
    {
        public int Id { get; set; }
        public int IngridientDataId { get; set; }
        public int MealId { get; set; }
        public double Weight { get; set; }

        public virtual IngridientData IdNavigation { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
