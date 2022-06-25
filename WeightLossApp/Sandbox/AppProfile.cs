using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mobile.Helpers;
using Mobile.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
namespace Sandbox
{
    public class AppProfile
    {
        public AppProfile()
        {
            nutrition = new Dictionary<string, Nutrients>
            {
                {"ManChildKeep", new Nutrients{ ENERC_KCAL = 2500, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"ManChildLoose", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"ManChildGain", new Nutrients{ ENERC_KCAL = 3200, PROCNT = 96, CHOCDF = 117, FAT = 440 }},
                {"ManYoungKeep", new Nutrients{ ENERC_KCAL = 2750, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"ManYoungLoose", new Nutrients{ ENERC_KCAL = 2400, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"ManYoungGain", new Nutrients{ ENERC_KCAL = 3000, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"ManAdultKeep", new Nutrients{ ENERC_KCAL = 2600, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"ManAdultLoose", new Nutrients{ ENERC_KCAL = 2200, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"ManAdultGain", new Nutrients{ ENERC_KCAL = 3000, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"ManOldKeep", new Nutrients{ ENERC_KCAL = 2250, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"ManOldLoose", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"ManOldGain", new Nutrients{ ENERC_KCAL = 2600, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"WomanChildKeep", new Nutrients{ ENERC_KCAL = 2500, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"WomanChildLoose", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"WomanChildGain", new Nutrients{ ENERC_KCAL = 3200, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"WomanYoungKeep", new Nutrients{ ENERC_KCAL = 2200, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"WomanYoungLoose", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"WomanYoungGain", new Nutrients{ ENERC_KCAL = 2400, PROCNT = 96, CHOCDF = 117, FAT = 440 } },
                {"WomanAdultKeep", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"WomanAdultLoose", new Nutrients{ ENERC_KCAL = 1800, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"WomanAdultGain", new Nutrients{ ENERC_KCAL = 2200, PROCNT = 93, CHOCDF = 114, FAT = 426 } },
                {"WomanOldKeep", new Nutrients{ ENERC_KCAL = 1900, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"WomanOldLoose", new Nutrients{ ENERC_KCAL = 1600, PROCNT = 88, CHOCDF = 108, FAT = 406 } },
                {"WomanOldGain", new Nutrients{ ENERC_KCAL = 2000, PROCNT = 88, CHOCDF = 108, FAT = 406 } }
            };
            DayActivityMealList = new List<DayActivityMeal>();
            LoadAsync();
        }
        public Dictionary<string, Nutrients> nutrition;
        public Member Member { get; set; }
        public long Experience { get; set; }
        public List<DayActivityMeal> DayActivityMealList { get; set; }
        public List<string> Preferences { get; set; }
        public long GetLevel() { return (Experience/1000)+1; }

        public async Task LoadAsync()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "https://stirred-eagle-95.hasura.app/api/rest/";
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");
                HttpResponseMessage response = await client.GetAsync("DayActivityMeal");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("----------------------------");

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
        private string GetArrayStringFromResponce(string hasuraJsonResult)
        {
            string result = hasuraJsonResult;

            result = result.Remove(0, result.IndexOf("["));
            return result.Remove(result.LastIndexOf("]") + 1);
        }
        public async Task LoadAsyncPM()
        {
            Console.WriteLine("~~~~~~~~~~");
            using (var client = new HttpClient())
            {
                string address = "https://stirred-eagle-95.hasura.app/api/rest/";
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("~~~~~~~~");
                HttpResponseMessage response = await client.GetAsync("ProfileData?login=Val");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("----------------------------");

                    string memberStr;
                    string profileStr;
                    JObject member = JObject.Parse(res);
                    JObject memberS = (JObject)member.First.First.First.First.First;
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
                        var prof = JsonSerializer.Deserialize<Profile>(profileStr, options);
                        Experience = prof.Exp;
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
        private string GetArrayStringResponce(string hasuraJsonResult)
        {
            string result = hasuraJsonResult;

            result = result.Remove(0, result.IndexOf("["));
            return result.Remove(result.LastIndexOf("]") + 1);
        }
    }
}


