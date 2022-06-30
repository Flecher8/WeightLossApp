using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mobile.Helpers;
using Mobile.Models;
using Mobile.Views;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;


namespace Mobile.ViewModels
{
    public class IngridientsDataVM : PropertyChangedIpmlementator
    {
        private ObservableCollection<IngridientData> _ingridients;
        private Command _loadMoreCommand;
        private bool _isLoading;

        public ObservableCollection<IngridientData> Ingridients;
        private ObservableCollection<Food> foods;
        private Command loadMoreCommand;
        private Command addCommand;
        private string searchlineValue;
        private ObservableCollection<object> categoryChosen;
        private IngridientsPage currentPage;

        public string ApiUrl { get; set; }
        public string SearchlineValue
        {
            get => searchlineValue;
            set
            {
                searchlineValue = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> CategoryChosen
        {
            get => categoryChosen;
            set
            {
                categoryChosen = value;
                OnPropertyChanged();
            }
        }
        public List<string> Suggestions { get; set; }
        public ObservableCollection<Food> Foods
        {
            get => foods;
            set
            {
                foods = value;
                OnPropertyChanged();
            }
        } 

        // List of chosen foods on current page 
        public ObservableCollection<Food> ChosenFood { get; set; }
        public List<string> Categories { get; set; }
        public Command LoadCommand
        {
            get => loadMoreCommand;
            set
            {
                loadMoreCommand = value;
                OnPropertyChanged();
            }
        }
        public Command AddCommand
        {
            get => addCommand;
            set
            {
                addCommand = value;
                OnPropertyChanged();
            }
        }
        public IngridientsDataVM(IngridientsPage page, ObservableCollection<Food> chosen)
        {
            Foods = new ObservableCollection<Food>();
            ApiUrl = "https://api.edamam.com/api/food-database/v2/";
            Categories = new List<string>()
            {
                "alcohol-free",
                "celery-free",
                "crustacean-free",
                "dairy-free",
                "egg-free",
                "fish-free",
                "fodmap-free",
                "gluten-free",
                "immuno-supportive",
                "keto-friendly",
                "kidney-friendly",
                "kosher",
                "low-fat-abs",
                "low-potassium",
                "low-sugar",
                "luoine-free",
                "mustard-free",
                "no-oil-added",
                "paleo",
                "peanut-free",
                "pescatarian",
                "pork-free",
                "read-meat-free",
                "sesame-free",
                "soy-free",
                "sugar-conscious",
                "tree-nut-free",
                "vegan",
                "vegetarian",
                "wheat-free"
            };
            CategoryChosen = new ObservableCollection<object>();
            Suggestions = new List<string>() { "Bread", "Meat", "Butter", "Potato", "Tomato", "Cheese", "Pork", "Chicken" };
            LoadCommand = new Command(() => LoadAsync());
            AddCommand = new Command((label) => AddIngridient(label));
            ChosenFood = chosen;
            currentPage = page;

            LoadAsync();
        }

        private async Task LoadAsync()
        {
            Foods.Clear();
            Console.WriteLine("~~~");

            using (var client = new HttpClient())
            {
                string ingridient = (searchlineValue == null) ? "bread" : searchlineValue;
                StringBuilder address = new StringBuilder("parser?app_id=56b78e71&app_key=288e1610b3ea253b871e2409c6e712d9&ingr=" + ingridient);
                // https://api.edamam.com/api/food-database/v2/parser?app_id=56b78e71&app_key=288e1610b3ea253b871e2409c6e712d9&ingr=bread
                foreach (string category in categoryChosen)
                {
                    address.Append("&health=" + category);
                }
                


                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(address.ToString());
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    res = GetArrayStringResponce(res);
                    List<Food> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        temp = JsonSerializer.Deserialize<List<Food>>(res, options);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }

                    foreach (Food el in temp)
                    {
                        Foods.Add(el);
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
            JObject jo = JObject.Parse(jsonResult);
            jo.Property("text").Remove();
            jsonResult = jo.ToString();

            JObject jobj = JObject.Parse(jsonResult);
            jobj.Property("parsed").Remove();
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

        private async void AddIngridient(object label)
        {
            Food selected = Foods.Where(i => i.Label == label.ToString()).FirstOrDefault(); 
            if (!ChosenFood.Contains(selected))
            {
                ChosenFood.Add(selected);
                await currentPage.DisplayAlert("Success!", "Ingridient added. Total count: " + ChosenFood.Count(), "OK");
            }
        }
    }
}
