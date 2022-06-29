using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MealService
    {
        public ObservableCollection<Meal> MealRepository { get; set; }
        public static string ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";

        public MealService()
        {
            MealRepository = new ObservableCollection<Meal>();
        }

        public async Task GetAsync()
        {
            MealRepository.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var dayActivityMeal in AppProfile.Instance.DayActivityMealList)
                {
                    HttpResponseMessage response = await client.GetAsync("meal?id=" + dayActivityMeal.Meal_ID);

                    if (response.IsSuccessStatusCode)
                    {
                        string res = await response.Content.ReadAsStringAsync();
                        res = GetArrayStringResponce(res, "Meal");
                        List<Meal> temp;

                        try
                        {
                            JsonSerializerOptions options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true,
                            };

                            temp = JsonSerializer.Deserialize<List<Meal>>(res, options);

                            MealRepository.Add(temp[0]);

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
        }

        private async Task PostAsync(Meal meal)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");

                string address = "meal?name=" + meal.Name;

                HttpResponseMessage response = await client.PostAsync(address, null);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("----------------------------");

                    res = GetArrayStringResponce(res, "insert_Inventory");
                    res = GetArrayStringResponce(res, "returning");
                    
                    List<Meal> temp = new List<Meal>();
                    temp = JsonSerializer.Deserialize<List<Meal>>(res);

                    // WARNING possible bug_ due to lower register WARNING  
                    meal.Id = temp[0].Id;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }

        public async Task PutAsync(Meal meal)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string adress = "meal?id=" + meal.Id + "&name=" + meal.Name; 

                HttpResponseMessage response = await client.PutAsync(adress, null);
                Console.WriteLine(adress);
                Console.WriteLine(response.ToString());
            }
        }

        public async Task DeleteAsync(Meal meal)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"meal?id={meal.Id}");
                if (response.StatusCode is HttpStatusCode.NoContent)
                {
                    Console.WriteLine("Meal deleted." + response.StatusCode);
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
