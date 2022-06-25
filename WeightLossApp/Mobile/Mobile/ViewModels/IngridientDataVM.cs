using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Mobile.Helpers;
using Mobile.Models;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class IngridientDataVM : PropertyChangedIpmlementator
    {
        private ObservableCollection<IngridientData> _ingridients;
        private Command _loadMoreCommand;
        private bool _isLoading;

        public ObservableCollection<IngridientData> Ingridients
        {
            get => _ingridients;
            set
            {
                _ingridients = value;
                OnPropertyChanged();
            }
        }

        public Command LoadCommand
        {
            get => _loadMoreCommand;
            set
            {
                _loadMoreCommand = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public IngridientDataVM()
        {
            Ingridients = new ObservableCollection<IngridientData>();
            Ingridients.Add(new IngridientData
            {
                Name = "test",
                Calories = 0,
                Carbohydrates = 0,
                Fats = 0,
                Proteins = 0,
                ImageName = "test",
                ID = 0,
            });
            LoadCommand = new Command(() => LoadAsync());
        }

        public async Task LoadAsync()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");
                HttpResponseMessage response = await client.GetAsync("ingridientsData");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("----------------------------");
                    res = GetArrayStringFromResponce(res);
                    List<IngridientData> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            
                        };

                        Console.WriteLine(res);
                        temp = JsonSerializer.Deserialize<List<IngridientData>>(res, options);
                        
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }

                    foreach (IngridientData el in temp)
                    {
                        Ingridients.Add(el);
                        Console.WriteLine("Value:  " + el.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }

        private string GetArrayStringFromResponce(string hasuraJsonResult)
        {
            string result = hasuraJsonResult;

            result = result.Remove(0, result.IndexOf("["));
            return result.Remove(result.LastIndexOf("]") + 1);
        }
    }
}
