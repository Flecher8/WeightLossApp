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

namespace Mobile.ViewModels
{
    public class LoginVM : PropertyChangedIpmlementator
    {
        // Data
        private List<User> users;
        private User user;

        // Commands
        public Command login { get; private set; }
        public Command registration { get; private set; }

        // Navigation
        public INavigation Navigation { get; set; }

        // API
        private string ApiUrl { get; set; }

        public LoginVM()
        {
            // Create Data
            users = new List<User>();
            user = new User();

            // Set API URL
            ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";

            // Create Commands
            login = new Command(Login);
            registration = new Command(Registration);

            // Load Data
            LoadAsync();
        }
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                user.Email = value;
                OnPropertyChanged();
            }
        }

        // Add navigation to main page!!!
        public void Login()
        {
            //Navigation.PushAsync();
        }
        // Add navigation to one of the registration pages!!!
        public void Registration()
        {
            //Navigation.PushAsync();
        }
        public async Task LoadAsync()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "users";
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

                    List<User> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        temp = JsonSerializer.Deserialize<List<User>>(res);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }
                    foreach (User el in temp)
                    {
                        users.Add(el);
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
            var result = o.SelectToken("User");

            return result.ToString();
        }
    }
}
