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

        public async Task GetAsync(int profileId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"meal?profileId={profileId}");
                if (response.StatusCode is HttpStatusCode.Created)
                {
                    Console.WriteLine("Meal retrieved." + response.StatusCode);
                }
            }
        }

        public async Task LoadAsync()
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

        public async Task PostAsync(string mealName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await client.GetAsync($"meal?name={mealName}");
                if (response.StatusCode is HttpStatusCode.Created) 
                { 
                    Console.WriteLine("Meal created." + response.StatusCode);
                }
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
