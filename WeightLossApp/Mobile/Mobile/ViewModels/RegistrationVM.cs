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
using Mobile.Services;
using DevExpress.XamarinForms.Editors;
using System.Threading;

namespace Mobile.ViewModels
{
    public class RegistrationVM : PropertyChangedIpmlementator
    {
        // Data
        private List<User> users;
        private Member member;
        private User user;
        private int premiumStatusID;
        private int inventoryID;
        private List<string> preferences;

        private string passwordAgain;

        private bool ragioButtonKeep;
        private bool ragioButtonLoose;
        private bool ragioButtonGain;

        private IList<object> selectedPrefs;
        // Google data
        private readonly IGoogleManager googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        private bool gooleRegistration;

        // API
        private string ApiUrl { get; set; }

        // Preference items
        public List<string> Items { get; set; }

        // Commands
        public Command Registration { get; private set; }

        // Google commands
        public Command GoogleSignUp { get; private set; }

        // Navigation
        public INavigation Navigation { get; set; }


        /*
         *  Functions
         */

        public RegistrationVM(IList<object> selectedPrefs_)
        {
            // Create Data
            users = new List<User>();
            member = new Member() { Birthday = DateTime.Now };
            user = new User();
            selectedPrefs = selectedPrefs_;
            Items = new List<string>()
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
            preferences = new List<string>();

            // Set Google variables
            googleManager = DependencyService.Get<IGoogleManager>();

            // Set API URL
            ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";

            // Create Commands
            Registration = new Command(RegistrationFuncAsync);

            // Create Google Commands
            GoogleSignUp = new Command(GoogleRegistration);
            gooleRegistration = false;

            // Load Data
            LoadAsync();
        }

        // Add navigation to one of the main pages!!!
        public void RegistrationFuncAsync()
        {
            if (selectedPrefs.Count > 0)
            {
                selectedPrefs.Select(c => c.ToString()).ToList().ForEach(s => preferences.Add(s));
            }

            // Check if there are no users with such email
            if (!isRegistered)
            {
                // Check if password in field 1 and password in field 2 same
                if (PasswordsSame)
                {
                    // If registration by google then password in user can be null
                    if(gooleRegistration && !member.hasNull)
                    {
                        PostData();
                        
                        return;
                    }
                    // 1) Add Member + user + Profile to database
                    // Check data != null
                    if (!member.hasNull && !user.hasNull)
                    {
                        PostData();
                        CreateProfile();
                        App.Current.MainPage = new MainPage();
                        return;
                    }
                    else if (user.Login == null || user.Password == null)
                    {
                        App.Current.MainPage.DisplayAlert("Message", "Please specify your login and password, so that you will be able to enter this app even without google account!", "OK");
                    }
                    else
                        App.Current.MainPage.DisplayAlert("Message", "Not all data is filled", "OK");
                }
                else
                    App.Current.MainPage.DisplayAlert("Message", "Passwords in two fields are not the same", "OK");
            }
            else
                App.Current.MainPage.DisplayAlert("Message", "You are already registered", "OK");
        }

        private void CreateProfile()
        {
            CreateProfileAsync();
        }

        private async Task CreateProfileAsync()
        {
            AppProfile prof = AppProfile.Instance;
            await prof.LoadAsyncPM(user.Login);
            await prof.LoadAsync(prof.Profile.Id);
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
                gooleRegistration = true;
                googleManager.Logout();
                RegistrationFuncAsync();
            }
            else
            {
                googleManager.Logout();
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
        public DateTime Birthday
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

        public int GenderIndex
        {
            get 
            {
                return (Gender == "Male") ? 0
                    : (Gender == "Female") ? 1
                    : (Gender == "Other") ? 2
                    : -1;
            }

            set
            {
                Gender = (value == 0) ? "Male" 
                    : (value == 1) ? "Female" 
                    : (value == 2) ? "Other" 
                    : null;
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
        public bool RadioButtonKeep
        {
            get => ragioButtonKeep;
            set
            {
                ragioButtonKeep = value;
                member.Goal = "Keep";
                OnPropertyChanged();
            }
        }
        public bool RadioButtonLoose
        {
            get => ragioButtonLoose;
            set
            {
                ragioButtonLoose = value;
                member.Goal = "Loose";
                OnPropertyChanged();
            }
        }
        public bool RadioButtonGain
        {
            get => ragioButtonGain;
            set
            {
                ragioButtonGain = value;
                member.Goal = "Gain"; 
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
        
        public async void PostData()
        {
            //await PostInformationNeededForProfile();
            await PostMember();
            await PostUser();
            await PostPremiumStatus();
            await PostInventory();
            await PostProfile();
            // Post AchivementAcquirement!!! (needs to be developed)
        }
        private async Task PostInformationNeededForProfile()
        {
            PostUsersInformation();
            PostInventoryDependentTables();
        }
        private async Task PostUsersInformation()
        {
            await PostMember();
            PostUser();
        }
        private async Task PostInventoryDependentTables()
        {
            await PostPremiumStatus();
            PostInventory();
        }


        // Post Profile
        private async Task PostProfile()
        {
            const int startExp = 0;
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("Profile ~~~~~~~~");
                string prefs = "";
                foreach (string elm in preferences)
                {
                    prefs += elm + ",";
                }
                if (prefs == "") prefs = "none";

                string address = 
                    "postProfile?Exp=" + startExp
                    + "&Inventory_ID=" + inventoryID
                    + "&Member_ID=" + member.ID
                    + "&Preferences=" + prefs;

                HttpResponseMessage response = await client.PostAsync(address, null);
                Console.WriteLine(response);
                Console.WriteLine(address);


                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("----------------------------");
                    OnRegistrationSucceful();
                }
                else
                {
                    Console.WriteLine("Internal server Error, trying Again");
                    PostProfile();
                }
            }
        }
        // Post Inventory
        private async Task PostInventory()
        {
            Console.WriteLine("00000000000000000 Inventory ~~~~~~~~~~");
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
                    "postMember?Birthday=" + date.ToString("s")
                    + "&Gender=" + member.Gender
                    + "&Goal=" + member.Goal
                    + "&Height=" + member.Height
                    + "&RegistrationDate=" + DateTime.Now.ToString("s")
                    + "&Weight=" + member.Weight;

                HttpResponseMessage response = await client.PostAsync(address, null);
                Console.WriteLine(response);
                Console.WriteLine(address);

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

        private void OnRegistrationSucceful()
        {
            CreateProfile();
            App.Current.MainPage = new MainPage();
        }
    }
}
