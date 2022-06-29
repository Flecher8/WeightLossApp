using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public partial class Nutrients
    {
        public Nutrients()
        {

        }

        public double? ENERC_KCAL { get; set; }
        public double? PROCNT { get; set; }
        public double? FAT { get; set; }
        public double? CHOCDF { get; set; }
        public double? FIBTG { get; set; }

        public override string ToString()
        {
            return "Callories: " + ENERC_KCAL + " Proteines: " + PROCNT + "\nFats: " + FAT + " Carbs: " + CHOCDF;
        }
    }
}
