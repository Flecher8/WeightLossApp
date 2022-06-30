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
        
        public async Task PostAsync(IEnumerable<Food> ingiridents)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var ingirident in ingiridents)
                {
                    string address = "ingridients?id=" + ingirident.FoodId + "&mealid=" + ParentMeal.Id + "&weight=" + ingirident.Weight.ToString();
                    HttpResponseMessage response = await client.PostAsync(address, null);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Ingridient of meal created." + response.StatusCode);
                    }
                }
            }
        }

        // Post DayActivityMeal
        public async Task PostDAMAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string address = "dam?date=" + DateTime.Now.ToString("s") + "&mealid=" + ParentMeal.Id + "&profileid=" + AppProfile.Instance.Id;
                HttpResponseMessage response = await client.PostAsync(address, null);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Ingridient DAM Added" + response.StatusCode);
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
