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

namespace Sandbox
{
    public class AddEventVM : PropertyChangedIpmlementator
    {
        // Data
        private ObservableCollection<string> categories;
        private string selectedCategory;
        private string messageText;
        private DateTime date;
        private TimeSpan time;
        private int sheduleID;


        // Commands
        public Command Back { get; private set; }
        public Command Create { get; private set; }

        // Navigation
        public INavigation Navigation { get; set; }

        public AddEventVM(int schedule_Id)
        {
            // Create data
            categories = new ObservableCollection<string>();
            fillCategories();
            
            date = DateTime.Today;
            time = TimeSpan.Zero;
            

            sheduleID = schedule_Id;

            // Set commands
            Back = new Command(goBack);
            Create = new Command(CreateEvent);
        }

        // List of categories
        public ObservableCollection<string> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
            }
        }
        public string MessageText
        {
            get => messageText;
            set
            {
                messageText = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan Time
        {
            get => time;
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }

        private void goBack()
        {
            Navigation.PopAsync();
        }

        
        private void CreateEvent()
        {
            if(DataCorrect)
            {
                // 1) Post new event 
                PostEvent();
            }
            // 2) Navigate to last page
            goBack();
        }
        private async Task PostEvent()
        {

            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "https://stirred-eagle-95.hasura.app/api/rest/";
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");
                
                DateTime neededDate = date + time;
                

                string adr =
                    "postEvent?DateTime=" + neededDate.ToString("G")
                    + "&Description=" + messageText
                    + "&Schedule_ID=" + sheduleID
                    + "&SendNotification=true"
                    + "&Type=" + selectedCategory;

                HttpResponseMessage response = await client.PostAsync(adr ,null);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("----------------------------");

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,

                        };

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
        private void fillCategories()
        {
            categories.Add("Food");
            categories.Add("Training");
            categories.Add("Medicine");
        }
        private bool DataCorrect
        {
            get => messageText != null && selectedCategory != null;
        }
    }
}
