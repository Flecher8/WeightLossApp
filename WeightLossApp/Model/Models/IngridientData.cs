using System;
using System.ComponentModel.DataAnnotations;

namespace WeightLossApp.Models
{
    public class IngridientData
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Carbohydrates { get; set; }
        public float Fats { get; set; }

    }
}
