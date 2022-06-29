using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class IngridientMeal
    {
        public int Id { get; set; }
        public int IngridientDataId { get; set; }
        public string IngridientName { get; set; }
        public int MealId { get; set; }
        public double Weight { get; set; }
    }
}
