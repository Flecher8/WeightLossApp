using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mobile.Models;
using Newtonsoft.Json.Linq;

namespace Mobile.Services
{
    public class IngiridentMealService
    {
        public readonly Meal ParentMeal;
        public ObservableCollection<IngridientMeal> IngridientsRepository { get; set; }
        public static string ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";

        public IngiridentMealService(Meal parent)
        {
            IngridientsRepository = new ObservableCollection<IngridientMeal>();
            ParentMeal = parent;
        }

        public async Task LoadAsync()
        {
            IngridientsRepository.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ingridients?mealId=" + ParentMeal.Id);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    res = GetArrayStringResponce(res, "Ingridient");
                    List<IngridientMeal> temp;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        temp = JsonSerializer.Deserialize<List<IngridientMeal>>(res, options);

                        foreach (IngridientMeal i in temp)
                        {

                            IngridientsRepository.Add(i);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }
        
        public IEnumerable<IngridientMeal> ConvertIngredientDataToMealParts(IEnumerable<IngridientData> rawIngredientData)
        {
            return rawIngredientData.Select(ingredientData => new IngridientMeal()
            {
                IngridientDataId = ingredientData.ID,
                IngridientName = ingredientData.Name,
                // beware, maybe it should store double instead of string.... 
                Weight = "100"
            }).ToList();
        }

        public IEnumerable<IngridientMeal> GetOnlyUnknownIngredients(IEnumerable<IngridientMeal> initMealIngredients, IEnumerable<IngridientMeal> newMealIngredients)
        {
            return initMealIngredients.Where(i => !newMealIngredients.Any(ni => ni.IngridientDataId.Equals(i.IngridientDataId)));
        }

        public async Task PostAsync(IEnumerable<IngridientMeal> ingiridents)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var ingirident in ingiridents)
                {
                    HttpResponseMessage response = await client.GetAsync($"ingridients?id={ingirident.Id}&weight={ingirident.Weight}");
                    if (response.StatusCode is HttpStatusCode.Created)
                    {
                        Console.WriteLine("Ingridient of meal created." + response.StatusCode);
                    }
                }
            }
        }

        public async Task PutAsync(IngridientMeal ingridient)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await client.GetAsync($"ingridient?id={ingridient.ID}&weight={ingridient.Weight}");
                if (response.StatusCode is HttpStatusCode.Created) 
                { 
                    Console.WriteLine("Ingridient of meal modified." + response.StatusCode);
                }
            }
        }

        public async Task DeleteAsync(IngridientMeal ingridient)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"ingridient?id={ingridient.ID}");
                if (response.StatusCode is HttpStatusCode.Created)
                {
                    Console.WriteLine("Ingridient of meal modified." + response.StatusCode);
                }
            }
        }

        private string GetArrayStringResponce(string jsonResult, string token)
        {
            JObject o = JObject.Parse(jsonResult);
            var result = o.SelectToken(token);

            return result.ToString();
        }
    }
}
