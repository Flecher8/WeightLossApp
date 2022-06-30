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
namespace Mobile.ViewModels
{
    public class MainPageVM : PropertyChangedIpmlementator
    {
        private ObservableCollection<DonutChartPiece> mainChartValues;
        private List<Food> foods;
        private float nutritionProgress;
        private float sportProgress;
        private Nutrients planNutrients;
        private List<Ingridient> ingridients;
        private string title;
        private string ApiUrl { get; set; }
        private string FoodApiUrl { get; set; }

        // Property for PieChart, should contain 2 values 
        // First Progress in %
        // Second 100% - Progress
        public ObservableCollection<DonutChartPiece> MainChartValues
        {
            get => mainChartValues;
            set
            {
                mainChartValues = value;
                OnPropertyChanged();
            }
        }

        // Progress by nutrition
        public float NutritionProgress
        {
            get => nutritionProgress;
            set
            {
                nutritionProgress = value;
                OnPropertyChanged();
            }
        }

        public float SportProgress
        {
            get => sportProgress;
            set
            {
                sportProgress = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        // Color palette for chart
        public Color[] Palette { get; set; }

        public  MainPageVM()
        {
            // This initialization should be replaced by calculated values
            MainChartValues = new ObservableCollection<DonutChartPiece>();
            ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";
            FoodApiUrl = "https://api.edamam.com/api/food-database/v2/";
            NutritionProgress = 0.45f;
            SportProgress = 0.60f;

            // Initialization of color Palette for chart
            Palette = new Color[2];
            Palette[0] = (Color)Application.Current.Resources["PrimaryColor"];
            Palette[1] = (Color)Application.Current.Resources["SecondaryColor"];

            initialize(AppProfile.Instance);

            Title = (AppProfile.Instance.Login != null) ? "Hi, " + AppProfile.Instance.Login + "!" : "Welcome back!";
            
        }
        public async void initialize(AppProfile appProfile)
        {
            var t = DateTime.Now;
            await App.LoadProfile(Xamarin.Essentials.Preferences.Get("UserLogin", "empty"));
            await LoadAsync();

            string NutritionKey = "";
            NutritionKey += (appProfile.Member.Gender == "Male") ? "Male" : "Female";
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
                foreach (DayActivityMeal dayActivityMeal in appProfile.DayActivityMealList)
                {
                    if (ingridient.Meal_ID == dayActivityMeal.Meal_ID)
                        resultIngridients.Add(ingridient);
                }
            }
            foods = new List<Food>();

            await LoadFoodsAndCountCalories(resultIngridients);
            //Thread.Sleep(2000);
            //MainChartValues.Clear();
            MainChartValues.Add(new DonutChartPiece("Progress", (int)((NutritionProgress + SportProgress) * 50)));
            MainChartValues.Add(new DonutChartPiece("Empty", 100 - (int)((NutritionProgress + SportProgress) * 50)));

            Console.WriteLine("Time: " + (DateTime.Now - t));
        }
        private async Task LoadFoodsAndCountCalories(List<Ingridient> resultIngridients)
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
        private async Task CountCalories (List<Ingridient> resultIngridients)
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
            double nutritionProgressNumber = (calories / planNutrients.ENERC_KCAL.GetValueOrDefault(2500) + procnts / planNutrients.PROCNT.GetValueOrDefault(96) +
                fats / planNutrients.FAT.GetValueOrDefault(440) + CHOCDFs / planNutrients.CHOCDF.GetValueOrDefault(117)) / 4;
            NutritionProgress = (float)nutritionProgressNumber;
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

                arr.Add(header.Property("food").First);

                header = (JObject)header.Next;

            }
            jsonResult = arr.ToString();

            return jsonResult;
        }
    }
}

