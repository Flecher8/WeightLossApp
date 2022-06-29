using System;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Xamarin;
using Mobile;
using System.Collections.Generic;

namespace Sandbox
{
    public class ScheduleVM : PropertyChangedIpmlementator
    {
        // Data
        AppProfile profile;
        private ObservableCollection<Event> events;
        private DateTime date;
        private Schedule schedule;
        

        // Commands
        public Command LeftDate { get; private set; }
        public Command RightDate { get; private set; }
        public Command AddEvent { get; private set; }

        // Navigation
        public INavigation Navigation { get; set; }

        public ScheduleVM()
        {
            // Create Data
            events = new ObservableCollection<Event>();
            profile = AppProfile.Instance;
            date = DateTime.Now;

            LoadSchedule();
            fillEvents(date);
            

            // Create Commands
            LeftDate = new Command(SwitchDateLeft);
            RightDate = new Command(SwitchDateRight);
            AddEvent = new Command(AddNewEvent);
        }

        private void AddNewEvent()
        {
            // Add navigation to AddEvent View
            //App.Current.MainPage = new AddEventPage(schedule.ID);
        }

        private void SwitchDateRight()
        {
            date.AddDays(1);
            fillEvents(date);
        }

        private void SwitchDateLeft()
        {
            date.AddDays(-1);
            fillEvents(date);
        }

        public ObservableCollection<Event> Events
        {
            get => events;
        }

        public string Date
        {
            get => date.ToString("d");
        }

        private async Task LoadSchedule()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "https://stirred-eagle-95.hasura.app/api/rest/";
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");


                HttpResponseMessage response = await client.GetAsync("schedule_id?profile_id=" + profile.Profile.Id);
                
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("----------------------------");

                    res = GetArrayStringResponce(res, "Schedule");

                    List<Schedule> tmp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,

                        };
                        tmp = JsonSerializer.Deserialize<List<Schedule>>(res);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }
                    schedule = tmp.ElementAt(0);
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

        // Get events by specific date
        private async Task fillEvents(DateTime date)
        {
            // By profile ID find in table Schedule needed row with Schedule ID
            // Find all events for this Schedule ID and fill ObservableCollection<Event> events
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "https://stirred-eagle-95.hasura.app/api/rest/";
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");
                
                // schedule.ID
                HttpResponseMessage response = await client.GetAsync("event?schedule_ID=" + schedule.ID);
                

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("----------------------------");

                    res = GetArrayStringResponce(res, "Event");

                    List<Event> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        temp = JsonSerializer.Deserialize<List<Event>>(res);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }
                    // Clear events
                    events.Clear();

                    // Add new events
                    foreach (Event ev in temp)
                    {
                        if(ev.DateTime.Date == date.Date)
                        {
                            events.Add(ev);
                        }
                    }
                    foreach(Event ev in events)
                    {
                        Console.WriteLine(ev.Type);
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }
    }
}
