﻿using System;
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

            fillEvents(date);
            LoadSchedule();

            // Create Commands
            LeftDate = new Command(SwitchDateLeft);
            RightDate = new Command(SwitchDateRight);
            AddEvent = new Command(AddNewEvent);
        }

        private void AddNewEvent()
        {
            // Add navigation to AddEvent View
            //App.Current.MainPage = new AddEventPage();
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

        public DateTime Date
        {
            get => date;
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

                    res = GetArrayStringResponce(res);

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

        private string GetArrayStringResponce(string jsonResult)
        {
            JObject o = JObject.Parse(jsonResult);
            var result = o.SelectToken("Schedule");
            
            return result.ToString();
        }
        // Доделать !!!
        // Функция заполнения events по определённой дате
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

                HttpResponseMessage response = await client.GetAsync("schedule_id?profile_id=" + profile.Profile.Id);

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
    }
}
