using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Mobile.Helpers;
using Mobile.Models;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Mobile.Services;
using System.Threading;
namespace Sandbox
{
    public class NutrientsStatePageVM : PropertyChangedIpmlementator
    {
        private List<Food> foods;


        //here are stored progress values by categories for this specific user after initialize function
        private float calories;
        private float fats;
        private float proteins;
        private float carbs;
       
        //here are stored planed quantities by categories for this specific user after initialize function
        private Nutrients planNutrients;
        private List<Mobile.Models.Ingridient> ingridients;
        private string ApiUrl { get; set; }
        private string FoodApiUrl { get; set; }

        public float Calories
        {
            get { return calories; }
            set
            {
                calories = value;
                OnPropertyChanged();
            }
        }

        public float Fats
        {
            get { return fats; }
            set 
            { 
                fats = value; 
                OnPropertyChanged(); 
            }
        }

        public float Proteins
        {
            get { return proteins; }
            set
            {
                proteins = value; 
                OnPropertyChanged();
            }
        }

        public float Carbs
        {
            get { return carbs; }
            set
            {
                carbs = value; 
                OnPropertyChanged();
            }
        }



        public NutrientsStatePageVM()
        {
            ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";
            FoodApiUrl = "https://api.edamam.com/api/food-database/v2/";
        }
        public void initialize(AppProfile appProfile)
        {
            string NutritionKey = "";
            NutritionKey += appProfile.Member.Gender;
            DateTime now = DateTime.Today;
            int age = now.Year - appProfile.Member.Birthday.Year;
            if (appProfile.Member.Birthday > now.AddYears(-age)) age--;
            if (age < 18) NutritionKey += "Child";
            else if (age < 35) NutritionKey += "Young";
            else if (age < 60) NutritionKey += "Adult";
            else NutritionKey += "Old";
            NutritionKey += appProfile.Member.Goal;

            Nutrients nutrients = appProfile.nutrition[NutritionKey];
            planNutrients = nutrients;
            List<Mobile.Models.Ingridient> resultIngridients = new List<Ingridient>();
            foreach (Ingridient ingridient in ingridients)
            {
                foreach (Mobile.Models.DayActivityMeal dayActivityMeal in appProfile.DayActivityMealList)
                {
                    if (ingridient.Meal_ID == dayActivityMeal.Meal_ID)
                        resultIngridients.Add(ingridient);
                }
            }
            foods = new List<Food>();

            LoadFoodsAndCountCalories(resultIngridients);
            Thread.Sleep(2000);
            
        }
        private async void LoadFoodsAndCountCalories(List<Ingridient> resultIngridients)
        {
            await LoadFoods(resultIngridients);
            Console.WriteLine("Before counting");
            await CountCalories(resultIngridients);
        }
        private async Task LoadFoods(List<Ingridient> resultIngridients)
        {
            Console.WriteLine("Started loading foods");
            foreach (Ingridient ingr in resultIngridients)
            {
                await LoadAsyncFoods(ingr.IngridientData_ID);
            }
        }
        private async Task CountCalories(List<Ingridient> resultIngridients)
        {
            Console.WriteLine("Started counting");
            double calories = 0, procnts = 0, fats = 0, CHOCDFs = 0;
            for (int i = 0; i < resultIngridients.Count; i++)
            {
                calories += foods[i].Nutrients.ENERC_KCAL.GetValueOrDefault(0) * resultIngridients[i].Weight / 100;
                procnts += foods[i].Nutrients.PROCNT.GetValueOrDefault(0) * resultIngridients[i].Weight / 100;
                fats += foods[i].Nutrients.FAT.GetValueOrDefault(0) * resultIngridients[i].Weight / 100;
                CHOCDFs += foods[i].Nutrients.CHOCDF.GetValueOrDefault(0) * resultIngridients[i].Weight / 100;
            }
            this.calories = (float)(calories / planNutrients.ENERC_KCAL.GetValueOrDefault(2500));
            proteins = (float)(procnts / planNutrients.PROCNT.GetValueOrDefault(96));
            this.fats = (float)(fats / planNutrients.FAT.GetValueOrDefault(440));
            carbs = (float) (CHOCDFs / planNutrients.CHOCDF.GetValueOrDefault(117));
            
        }
        public async Task LoadAsync()
        {
            ingridients = new List<Ingridient>();
            Console.WriteLine("ingredients");
            using (var client = new HttpClient())
            {
                string address = "ingridients";
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");

                HttpResponseMessage response = await client.GetAsync(address);
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("----------------------------");

                    res = GetArrayStringResponce(res);

                    List<Ingridient> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        temp = JsonSerializer.Deserialize<List<Ingridient>>(res);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }
                    foreach (Ingridient el in temp)
                    {
                        ingridients.Add(el);
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }

        }
        private string GetArrayStringResponce(string jsonResult)
        {
            JObject o = JObject.Parse(jsonResult);
            var result = o.SelectToken("Ingridient");

            return result.ToString();
        }
        public async Task LoadAsyncFoods(string FoodId)
        {

            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "parser?app_id=56b78e71&app_key=288e1610b3ea253b871e2409c6e712d9" + "&ingr=" + FoodId;
                client.BaseAddress = new Uri(FoodApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("foods");
                HttpResponseMessage response = await client.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("----------------------------");

                    res = GetArrayStringResponceFoods(res);
                    //res = res.Replace("label", "Label");

                    List<Food> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,

                        };

                        Console.WriteLine(res);
                        temp = JsonSerializer.Deserialize<List<Food>>(res, options);


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }

                    foreach (Food el in temp)
                    {
                        foods.Add(el);
                        Console.WriteLine("Value:  " + el.Label);
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }

        }
        private string GetArrayStringResponceFoods(string jsonResult)
        {
            JObject jo = JObject.Parse(jsonResult);
            jo.Property("text").Remove();
            jsonResult = jo.ToString();

            JObject jobj = JObject.Parse(jsonResult);
            jsonResult = jobj.ToString();

            JObject job = JObject.Parse(jsonResult);
            JObject header = (JObject)job.First.First.First;
            JArray arr = new JArray();
            while (header != null)
            {

                //header.Property("measures").Remove();
                arr.Add(header.Property("food").First);

                header = (JObject)header.Next;

            }
            jsonResult = arr.ToString();

            return jsonResult;
        }
    }
}