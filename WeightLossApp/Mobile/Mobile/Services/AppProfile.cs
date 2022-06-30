﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mobile.Helpers;
using Mobile.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;


namespace Mobile.Services
{
    public sealed class AppProfile
    {
        // Singleton implementation
        private static AppProfile instance = null;
        private static readonly object padlock = new object();

        private AppProfile()
        {
            nutrition = new Dictionary<string, Nutrients>
            {
                {"MaleChildKeep", new Nutrients{ ENERC_KCAL = 2500, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"MaleChildLoose", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"MaleChildGain", new Nutrients{ ENERC_KCAL = 3200, PROCNT = 96, CHOCDF = 117, FAT = 440 }},
                {"MaleYoungKeep", new Nutrients{ ENERC_KCAL = 2750, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"MaleYoungLoose", new Nutrients{ ENERC_KCAL = 2400, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"MaleYoungGain", new Nutrients{ ENERC_KCAL = 3000, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"MaleAdultKeep", new Nutrients{ ENERC_KCAL = 2600, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"MaleAdultLoose", new Nutrients{ ENERC_KCAL = 2200, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"MaleAdultGain", new Nutrients{ ENERC_KCAL = 3000, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"MaleOldKeep", new Nutrients{ ENERC_KCAL = 2250, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"MaleOldLoose", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"MaleOldGain", new Nutrients{ ENERC_KCAL = 2600, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"FemaleChildKeep", new Nutrients{ ENERC_KCAL = 2500, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"FemaleChildLoose", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"FemaleChildGain", new Nutrients{ ENERC_KCAL = 3200, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"FemaleYoungKeep", new Nutrients{ ENERC_KCAL = 2200, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"FemaleYoungLoose", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"FemaleYoungGain", new Nutrients{ ENERC_KCAL = 2400, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"FemaleAdultKeep", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"FemaleAdultLoose", new Nutrients{ ENERC_KCAL = 1800, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"FemaleAdultGain", new Nutrients{ ENERC_KCAL = 2200, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"FemaleOldKeep", new Nutrients{ ENERC_KCAL = 1900, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"FemaleOldLoose", new Nutrients{ ENERC_KCAL = 1600, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"FemaleOldGain", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 88, CHOCDF = 108, FAT = 406 } }
            };
            DayActivityMealList = new List<DayActivityMeal>();
        }

        // Singleton Implementation
        public static AppProfile Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AppProfile();
                    }
                    return instance;
                }
            }
        }

        public Dictionary<string, Nutrients> nutrition;
        public Member Member { get; set; }
        public Profile Profile { get; set; }

        public string Login { get; set; }

        public int Id { get; set; }

        public long Experience 
        { 
            get => Profile.Exp;
            set {
                if (Profile != null) 
                    Profile.Exp = value;
            }
        }

        public List<string> Preferences { get; set; }

        public List<DayActivityMeal> DayActivityMealList { get; set; }

        public long GetLevel() { return (Experience/1000)+1; }

        public async Task LoadAsync(int profileId)
        {
            Id = profileId;
            using (var client = new HttpClient())
            {
                string address = "https://stirred-eagle-95.hasura.app/api/rest/";
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("DayActivityMeal?id=" + profileId);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    res = GetArrayStringFromResponce(res);
                   

                    List<DayActivityMeal> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,

                        };

                        Console.WriteLine(res);
                        temp = JsonSerializer.Deserialize<List<DayActivityMeal>>(res, options);


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ~~~~~ " + ex.Message);
                    }

                    foreach (DayActivityMeal el in temp)
                    {
                        DayActivityMealList.Add(el);
                        Console.WriteLine("OK");
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }

        }

        private async Task PostDayActivityMealAsync(Meal meal)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");
                DateTime now = DateTime.Now;
                string address = "dam?date=" + now.ToString("s") + "&mealid=" + meal.Id + "&profileid" + Profile.Id;

                HttpResponseMessage response = await client.PostAsync(address, null);

                if (response.IsSuccessStatusCode)
                {
                    DayActivityMealList.Add(new DayActivityMeal{Datetime = now, Meal_ID = meal.Id, Profile_ID = Profile.Id});
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }

        public async Task DeleteAsync(DayActivityMeal dam)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("dam?mealid=" + dam.Meal_ID + "&profileid=" + Profile.Id );
                if (response.StatusCode is HttpStatusCode.NoContent)
                {
                    Console.WriteLine("deleted" + response.StatusCode);
                }
            }
        }



        public async Task LoadAsyncPM(string login)
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "https://stirred-eagle-95.hasura.app/api/rest/";
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("appprofile");
                HttpResponseMessage response = await client.GetAsync("ProfileData?login=" + login);
                //Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("----------------------------");
                    Console.WriteLine(res);

                    string memberStr;
                    string profileStr;
                    JObject member = JObject.Parse(res);
                    JObject memberS = (JObject)member.First.First.First.First.First;
                    Console.WriteLine(memberS.ToString());
                    memberStr = memberS.ToString();

                    JObject profile = (JObject)memberS.Last.First.First;
                    profileStr = profile.ToString();
                    Console.WriteLine(profileStr);

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,

                        };


                        Member = JsonSerializer.Deserialize<Member>(memberStr, options);
                        Profile = JsonSerializer.Deserialize<Profile>(profileStr, options);
                        Id = Profile.Id;
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

        public void LogOut()
        {
            Member = null;
            Profile = null;
            Id = -1;
            Experience = -1;
            Preferences = null;
            DayActivityMealList = null;

            Xamarin.Essentials.Preferences.Remove("UserLogin");
        }

        private string GetArrayStringFromResponce(string hasuraJsonResult)
        {
            string result = hasuraJsonResult;

            result = result.Remove(0, result.IndexOf("["));
            return result.Remove(result.LastIndexOf("]") + 1);
        }

        private string GetArrayStringResponce(string jsonResult, string token)
        {
            JObject o = JObject.Parse(jsonResult);
            var result = o.SelectToken(token);

            return result.ToString();
        }
    }
}
