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
                IngridientData_ID = ingredientData.ID.ToString(),
                IngridientName = ingredientData.Name,
                Weight = 100
            }).ToList();
        }

        public IEnumerable<IngridientMeal> GetOnlyUnknownIngredients(IEnumerable<IngridientMeal> initMealIngredients, IEnumerable<IngridientMeal> newMealIngredients)
        {
            return initMealIngredients.Where(i => !newMealIngredients.Any(ni => ni.IngridientData_ID.Equals(i.IngridientData_ID)));
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
                    string address = "ingridients?id=" + ingirident.IngridientData_ID + "&mealid=" + ingirident.Meal_ID + "&weight=" + ingirident.Weight.ToString();
                    HttpResponseMessage response = await client.PostAsync(address, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string res = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("----------------------------");

                        res = GetArrayStringResponce(res, "insert_Inventory");
                        res = GetArrayStringResponce(res, "returning");
                        
                        List<IngridientMeal> temp = new List<IngridientMeal>();
                        temp = JsonSerializer.Deserialize<List<IngridientMeal>>(res);
                        
                        ingirident.ID = temp[0].ID;
                    
                        Console.WriteLine("Ingridient of meal created." + response.StatusCode);
                    }
                }
            }
        }

        public async Task PutAsync(IngridientMeal ingridient)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string address = "ingridients?id=" + ingridient.ID + "&weight" + ingridient.Weight;

                                 HttpResponseMessage response = await client.PutAsync(address, null);
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
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"ingridients?id={ingridient.ID}");
                if (response.StatusCode is HttpStatusCode.NoContent)
                {
                    Console.WriteLine("Ingridient of meal deleted." + response.StatusCode);
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
