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

namespace Mobile.ViewModels
{
    public class LoginVM : PropertyChangedIpmlementator
    {
        // Data
        private List<User> users;
        private User user;
        private string email;
        private string password;

        // Google variables
        private readonly IGoogleManager googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        public bool IsLogedIn { get; set; }

        // Commands
        public Command LogIn { get; private set; }
        public Command Registration { get; private set; }

        public Command Logout { get; private set; }

        // Google commands
        public Command GoogleLogIn { get; private set; }

        // Navigation
        public INavigation Navigation { get; set; }

        // API
        private string ApiUrl { get; set; }



        /*
         * Functions
         */
        public LoginVM()
        {
            // Create Data
            users = new List<User>();

            // Set Google variables
            googleManager = DependencyService.Get<IGoogleManager>();

            // Set API URL
            ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";

            // Create Commands
            LogIn = new Command(LoginFun);
            Registration = new Command(RegistrationFun);
            Logout = new Command(LogOut);

            // Create Google Commands
            GoogleLogIn = new Command(GoogleLogin);

            // Load Data
            LoadAsync();

            Password = "pass";
            Email = "mail";
        }

        public void LogOut()
        {
            googleManager.Logout();
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        
        // Add navigation to main page!!!
        public async void LoginFun()
        {
            if(isDataCorrect)
            {
                App.LoadProfile(user.Login);

                Xamarin.Essentials.Preferences.Set("UserLogin", user.Login);

                App.Current.MainPage = new MainPage();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Message", "Login faliled", "Ok");
            }
        }
        public void RegistrationFun()
        {
            App.Current.MainPage = new Views.RegistrationPage();
        }
        public void GoogleLogin()
        {
            googleManager.Login(OnLoginComplete);
        }
        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if(googleUser != null)
            {
                GoogleUser = googleUser;

                Console.WriteLine(googleUser.Email);
                IsLogedIn = true;
                if(isRegistered)
                {
                    App.LoadProfile(user.Login);

                    Xamarin.Essentials.Preferences.Set("UserLogin", user.Login);

                    App.Current.MainPage = new MainPage();
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Message", "No such email", "Ok");
                    googleManager.Logout();
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Message", message, "Ok");
            }
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
        private bool isDataCorrect
        {
            get
            {
                foreach (User element in users)
                {
                    if (element.Email == Email && element.Password == Password)
                    {
                        user = element;
                        return true;
                    }
                }
                return false;
            }
        }
        private bool isRegistered
        {
            get
            {
                foreach (User usr in users)
                {
                    if (usr.Email == GoogleUser.Email)
                    {
                        user = usr;
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
