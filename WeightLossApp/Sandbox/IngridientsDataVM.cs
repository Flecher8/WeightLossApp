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
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Sandbox
{
    public class IngridientsDataVM : PropertyChangedIpmlementator
    {
        private ObservableCollection<Food> foods;
        private Command loadMoreCommand;
        private string searchlineValue;
        private string category;
        public string ApiUrl { get; set; }
        public string result;
        //string sht = "parser?app_id=56b78e71&app_key=288e1610b3ea253b871e2409c6e712d9";
        public string SearchlineValue {
            get => searchlineValue;
            set
            {
                searchlineValue = value;
                OnPropertyChanged();
            }
        }
        public string Category {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Food> Foods {
            get => foods;
            set
            {
                foods = value;
                OnPropertyChanged();
            }
        }
        public Command LoadCommand
        {
            get => loadMoreCommand;
            set
            {
                loadMoreCommand = value;
                OnPropertyChanged();
            }
        }
        public IngridientsDataVM()
        {
            Foods = new ObservableCollection<Food>();
            ApiUrl = "https://api.edamam.com/api/food-database/v2/";
            SearchlineValue = "bread";
            Category = "vegan";
            LoadAsync();
        }
        public async Task LoadAsync()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "parser?app_id=56b78e71&app_key=288e1610b3ea253b871e2409c6e712d9" + "&ingr=" + SearchlineValue + "&health=" + Category;
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");
                HttpResponseMessage response = await client.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("----------------------------");
               
                    res = GetArrayStringResponce(res);
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
                        Foods.Add(el);
                        Console.WriteLine("Value:  " + el.Label);
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

                //header.Property("measures").Remove();
                arr.Add(header.Property("food").First);

                header = (JObject)header.Next;

            }
            jsonResult = arr.ToString();

            //JObject jobje = JObject.Parse(jsonResult);
            //jobje.Property("_links").Remove();
            //jsonResult = jobje.ToString();

            //jsonResult = jsonResult.Remove(0, jsonResult.IndexOf("["));
            //return jsonResult.Remove(jsonResult.LastIndexOf("]") + 1);

            return jsonResult;
        }
    }
}
