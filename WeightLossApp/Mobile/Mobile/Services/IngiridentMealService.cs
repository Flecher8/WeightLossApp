using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;

namespace Mobile.Services
{
    public class IngiridentMealService
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

        public IEnumerable<IngridientMeal> GetOnlyUnknownIngredients(IEnumerable<IngridientMeal> initMealIngredients, IEnumerable<IngridientMeal> newMealIngredients)
        {
            return initMealIngredients.Where(i => !newMealIngredients.Any(ni => ni.IngridientId.Equals(i.IngridientId)));
        }

        public async Task PostAsync(IEnumerable<IngridientMeal> ingiridents)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var ingirident in ingiridents)
                {
                    HttpResponseMessage response = await client.GetAsync($"ingridient?ingridientDataId={ingirident.IngridientId}&name={ingirident.IngridientName}&weight={ingirident.Weight}");
                    if (response.StatusCode is HttpStatusCode.Created)
                    {
                        Console.WriteLine("Ingridient of meal created." + response.StatusCode);
                    }
                }
                
            }
        }
    }
}
