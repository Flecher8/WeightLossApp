using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mobile.Models;
using Newtonsoft.Json.Linq;
using Mobile.Helpers;

namespace Mobile.Services
{
    public class EventService : PropertyChangedIpmlementator
    {
        public ObservableCollection<Event> EventRepository { get; set; }
        public static string ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";

        public EventService()
        {
            EventRepository = new ObservableCollection<Event>();
        }

        public async Task LoadEvents()
        {
            EventRepository.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("event?id=" + AppProfile.Instance.Profile.Id);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    res = GetArrayStringResponce(res);
                    List<Event> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        temp = JsonSerializer.Deserialize<List<Event>>(res, options);
                        
                        foreach(Event e in temp)
                        {
                            e.RecurrenceInfo = e.RecurrenceInfo == "empty" ? null : e.RecurrenceInfo;
                            e.ReminderInfo = ComposeRems(e.ReminderInfo);

                            EventRepository.Add(e);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message );
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }

        public static async Task AddEvent(Event newEvent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string adress = "event?rec=" + newEvent.RecurrenceInfo + "&rem=" + DecomposeRems(newEvent.ReminderInfo) +
                    "&allday=" + newEvent.AllDay.ToString().ToLower() + "&end=" + newEvent.EndTime.ToString("s") +
                    "&start=" + newEvent.StartTime.ToString("s") + "&id=" + AppProfile.Instance.Profile.Id +
                    "&status=" + newEvent.StatusID + "&subj=" + newEvent.Subject + "&label=" + newEvent.LabelID;

                HttpResponseMessage response = await client.PostAsync(adress, null);
                Console.WriteLine(adress);
                Console.WriteLine(response.ToString());            
            }
        }

        public static async Task UpdateEvent(Event newEvent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string adress = "event?allday=" + newEvent.AllDay.ToString().ToLower() + "&end=" + newEvent.EndTime.ToString("s") +
                    "&start=" + newEvent.StartTime.ToString("s") + "&pid=" + AppProfile.Instance.Profile.Id +
                    "&status=" + newEvent.StatusID + "&subj=" + newEvent.Subject + "&label=" + newEvent.LabelID + "&id=" + newEvent.Id +
                    "rec=" + newEvent.RecurrenceInfo + " & rem = " + newEvent.ReminderInfo;

                HttpResponseMessage response = await client.PutAsync(adress, null);
                Console.WriteLine(adress);
                Console.WriteLine(response.ToString());
            }
        }

        private string GetArrayStringResponce(string jsonResult)
        {
            return JObject.Parse(jsonResult).SelectToken("Event").ToString();
        }

        // From xml
        private static string DecomposeRems(string composedRems)
        {
            StringBuilder sb = new StringBuilder();
            string[] arr = composedRems.Split('\n');

            for (int i = 1; i < arr.Length - 1; ++i)
            {
                int prevIndex = arr[i].IndexOf('"');
                sb.Append(arr[i].Substring(prevIndex, arr[i].IndexOf('"', prevIndex + 1) - prevIndex) + ",");
                prevIndex = arr[i].LastIndexOf("=") + 1;
                sb.Append(arr[i].Substring(prevIndex, arr[i].LastIndexOf('"') - prevIndex) + ",");
            }

            return sb.ToString();
        }
        // To xml
        private static string ComposeRems(string decomposedRems)
        {
            if (decomposedRems == null || decomposedRems == "empty" || decomposedRems == "") 
                return null;
            else
            {
                StringBuilder sb = new StringBuilder();
                string[] arr = decomposedRems.Split(',');

                sb.Append("<Reminders>\n");

                for (int i = 0; i < arr.Length - 1; i += 2)
                {
                    sb.Append("<Reminder Id=");
                    sb.Append(arr[i]);
                    sb.Append("\" TimeBeforeStart=");
                    sb.Append(arr[i + 1]);
                    sb.Append("\" />\n");
                }

                sb.Append("</Reminders>");

                return sb.ToString();
            }
        }
    }
}
