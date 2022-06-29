using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class IngridientMeal
    {
        public int ID { get; set; }
        public string IngridientData_ID { get; set; }
        public string IngridientName { get; set; }
        public int Meal_ID { get; set; }
        public double Weight { get; set; }

    }
}
