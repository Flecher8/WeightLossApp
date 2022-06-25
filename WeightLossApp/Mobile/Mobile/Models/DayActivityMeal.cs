using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class DayActivityMeal
    {
        public DayActivityMeal()
        {

        }

        public int Profile_ID { get; set; }
        public int Meal_ID { get; set; }
        public DateTime Datetime { get; set; }
    }
}
