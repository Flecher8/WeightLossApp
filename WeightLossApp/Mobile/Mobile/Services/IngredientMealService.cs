using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Mobile.Models;

namespace Mobile.Services
{
    public class IngredientMealService
    {
        public IEnumerable<IngridientMeal> ConvertIngredientDataToMealParts(IEnumerable<IngridientData> rawIngredientData)
        {
            return rawIngredientData.Select(ingredientData => new IngridientMeal()
            {
                IngridientId = ingredientData.ID,
                IngridientName = ingredientData.Name,
                Weight = "100"
            }).ToList();
        }
    }
}
