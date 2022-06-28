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
using Xamarin;
using Mobile;

namespace Sandbox
{
    public class RegistrationVM : PropertyChangedIpmlementator
    {
        // Data
        private List<User> users;
        private Member member;
        private User user;
        private int premiumStatusID;
        private int inventoryID;

        private string passwordAgain;
        private string preferences;

        private bool ragioButtonKeep;
        private bool ragioButtonLoose;
        private bool ragioButtonGain;

        // Google data
        private readonly IGoogleManager googleManager;
        GoogleUser GoogleUser = new GoogleUser();

        // API
        private string ApiUrl { get; set; }

        // Commands
        public Command Registration { get; private set; }

        // Google commands
        public Command googleSignIn { get; private set; }

        // Navigation
        public INavigation Navigation { get; set; }


        /*
         *  Functions
         */

        public RegistrationVM()
        {

            // Create Data
            users = new List<User>();
            member = new Member();
            user = new User();

            // Set Google variables
            //googleManager = DependencyService.Get<IGoogleManager>();

            // Set API URL
            ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";

            // Create Commands
            Registration = new Command(RegistrationFunc);

            // Create Google Commands
            googleSignIn = new Command(GoogleRegistration);

            // Load Data
            LoadAsync();
        }

        // Add navigation to one of the main pages!!!
        public void RegistrationFunc()
        {
            // Check if there are no users with such email
            if (!isRegistered)
            {
                // Check if password in field 1 and password in field 2 same
                if (PasswordsSame)
                {
                    // 1) Add Member + user + Profile to database !!!
                    // Check data != null
                    if (!member.hasNull && !user.hasNull)
                    {
                        PostData();
                        CreateProfile();
                    }

                    // 2) Add navigation to the main page!!!
                    //Navigation.PushAsync();
                    return;
                }
                App.Current.MainPage.DisplayAlert("Message", "Passwords in two fields are not the same", "Ok");

            }
            App.Current.MainPage.DisplayAlert("Message", "You are already registered", "Ok");
        }
        // Need to be done
        private void CreateProfile()
        {
            throw new NotImplementedException();
        }

        // Google Registration
        public void GoogleRegistration()
        {
            googleManager.Login(OnLoginComplete);
        }
        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                GoogleUser = googleUser;
                user.Email = GoogleUser.Email;
                RegistrationFunc();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Message", message, "Ok");
            }
        }

        // Input fields
        // User info
        public double Weight
        {
            get => member.Weight;
            set
            {
                member.Weight = value;
                OnPropertyChanged();
            }
        }
        public double Height
        {
            get => member.Height;
            set
            {
                member.Height = value;
                OnPropertyChanged();
            }
        }
        public DateTime? Birthday
        {
            get => member.Birthday;
            set
            {
                member.Birthday = value;
                OnPropertyChanged();
            }
        }
        public string Gender
        {
            get => member.Gender;
            set
            {
                member.Gender = value;
                OnPropertyChanged();
            }
        }

        // User preferences
        public string Preferences
        {
            get => preferences;
            set
            {
                preferences = value;
                OnPropertyChanged();
            }
        }

        // Registration data
        public string Login
        {
            get => user.Login;
            set
            {
                user.Login = value;
                OnPropertyChanged();
            }
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
                user.Password = value;
                OnPropertyChanged();
            }
        }
        // Repeat password input field
        public string PasswordAgain
        {
            get => passwordAgain;
            set
            {
                passwordAgain = value;
                OnPropertyChanged();
            }
        }

        // RagioButtons
        public bool RagioButtonKeep
        {
            get => ragioButtonKeep;
            set
            {
                ragioButtonKeep = value;
                OnPropertyChanged();
            }
        }
        public bool RagioButtonLoose
        {
            get => ragioButtonLoose;
            set
            {
                ragioButtonLoose = value;
                OnPropertyChanged();
            }
        }
        public bool RagioButtonGain
        {
            get => ragioButtonGain;
            set
            {
                ragioButtonGain = value;
                OnPropertyChanged();
            }
        }

        
        public async Task LoadAsync()
        {
            Console.WriteLine("~~~~~~~~~~");
            Console.WriteLine("users");
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

                    res = GetArrayStringResponce(res, "User");

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
        private string GetArrayStringResponce(string jsonResult, string token)
        {
            JObject o = JObject.Parse(jsonResult);
            var result = o.SelectToken(token);

            return result.ToString();
        }


        private bool isRegistered
        {
            get
            {
                foreach (User usr in users)
                {
                    if (usr.Email == user.Email)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        private bool PasswordsSame
        {
            get => (user.Password == passwordAgain);
        }
        // Пробная функция!!!
        public async void PostData()
        {
            //member.Weight = 15;
            //member.Height = 15;
            //member.Goal = "Less";
            //member.Birthday = DateTime.Today.AddYears(-20);
            //member.RegistrationDate = DateTime.Now;
            //member.Gender = "Male";
            //await PostMember();
            //user.Email = "test@gmail.com";
            //user.Login = "test";
            //user.Password = "test";
            //PostUser();
            await PostPremiumStatus();
            PostInventory();

        }
        // Post Inventory
        private async Task PostInventory()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");

                string address = "postInventory?PremiumStatus_ID=" + premiumStatusID;

                HttpResponseMessage response = await client.PostAsync(address, null);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("----------------------------");

                    res = GetArrayStringResponce(res, "insert_Inventory");
                    res = GetArrayStringResponce(res, "returning");

                    // temp value to get ID from new PremiumStatus
                    // Member is just a good container for ID
                    // (Can be changed to Models.Inventory)
                    List<Member> temp = new List<Member>();
                    temp = JsonSerializer.Deserialize<List<Member>>(res);

                    inventoryID = temp[0].ID;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }
        // Post PremiumStatus
        private async Task PostPremiumStatus()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");

                string address = "postPremiumStatusNulls";

                HttpResponseMessage response = await client.PostAsync(address, null);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("----------------------------");

                    res = GetArrayStringResponce(res, "insert_PremiumStatus");
                    res = GetArrayStringResponce(res, "returning");

                    // temp value to get ID from new PremiumStatus
                    List<Member> temp = new List<Member>();
                    temp = JsonSerializer.Deserialize<List<Member>>(res);

                    premiumStatusID = temp[0].ID;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }
        // Post User
        private async Task PostUser()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");

                string address =
                    "postUser?Email=" + user.Email
                    + "&Login=" + user.Login
                    + "&Member_ID=" + member.ID
                    + "&Password=" + user.Password;

                HttpResponseMessage response = await client.PostAsync(address, null);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("----------------------------");
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }
        // Post Member
        private async Task PostMember()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");

                DateTime date = (DateTime)member.Birthday;

                string address =
                    "postMember?Birthday=" + date.ToString("G")
                    + "&Gender=" + member.Gender
                    + "&Goal=" + member.Goal
                    + "&Height=" + member.Height
                    + "&RegistrationDate=" + DateTime.Now.ToString("G")
                    + "&Weight=" + member.Weight;

                HttpResponseMessage response = await client.PostAsync(address, null);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("----------------------------");

                    List<Member> temp = new List<Member>();

                    res = GetArrayStringResponce(res, "insert_Member");

                    res = GetArrayStringResponce(res, "returning");


                    temp = JsonSerializer.Deserialize<List<Member>>(res);
                    member.ID = temp[0].ID;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }
    }
}
