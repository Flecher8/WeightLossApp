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
                            EventRepository.Add(e);
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

        public static async Task AddEvent(Event newEvent, bool isJunkParamsAreNull = true)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string adress = "event";
                if (!isJunkParamsAreNull)
                {
                    adress += "&rec=" + newEvent.RecurrenceInfo + "&rem=" + newEvent.ReminderInfo + "?allday=" + newEvent.AllDay.ToString().ToLower() + "&end=" + newEvent.EndTime.ToString("s") +
                        "&start=" + newEvent.StartTime.ToString("s") + "&id=" + AppProfile.Instance.Profile.Id +
                        "&status=" + newEvent.StatusID + "&subj=" + newEvent.Subject + "&label=" + newEvent.LabelID;
                }
                else
                {

                    adress += "Null?allday=" + newEvent.AllDay.ToString().ToLower() + "&end=" + newEvent.EndTime.ToString("s") +
                        "&start=" + newEvent.StartTime.ToString("s") + "&id=" + AppProfile.Instance.Profile.Id +
                        "&status=" + newEvent.StatusID + "&subj=" + newEvent.Subject + "&label=" + newEvent.LabelID;
                }


                HttpResponseMessage response = await client.PostAsync(adress, null);

                Console.WriteLine(response.ToString());            
            }
        }

        public static async Task UpdateEvent(Event newEvent, bool isJunkParamsAreNull = true)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string adress = "event?allday=" + newEvent.AllDay.ToString().ToLower() + "&end=" + newEvent.EndTime.ToString("s") +
                    "&start=" + newEvent.StartTime.ToString("s") + "&pid=" + AppProfile.Instance.Profile.Id +
                    "&status=" + newEvent.StatusID + "&subj=" + newEvent.Subject + "&label=" + newEvent.LabelID + "&id=" + newEvent.Id;

                HttpResponseMessage response = await client.PutAsync(adress, null);
                Console.WriteLine(adress);
                Console.WriteLine(response.ToString());
                //event?allday=false&end=2022-06-28T10:30:00&start=2022-06-28T10:00:00&pid=7&status=0&subj=Appointment&label=1&id=13
            }
        }

        private string GetArrayStringResponce(string jsonResult)
        {
            return JObject.Parse(jsonResult).SelectToken("Event").ToString();
        }
    }
}
