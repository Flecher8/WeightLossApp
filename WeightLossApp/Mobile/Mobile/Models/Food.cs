using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public partial class Food
    {
        public Food()
        {

        }
        public string FoodId { get; set; }
        public string Label { get; set; }
        public string Category { get; set; }
        public string CategoryLabel { get; set; }
        public string Image { get; set; }
        public int Weight { get; set; }
        public Nutrients Nutrients { get; set; }

    }
}
