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

namespace Sandbox
{
    public class LoginVM : PropertyChangedIpmlementator
    {
        private List<User> users;
        private User user;
        public string ApiUrl { private get; set; }
        public string result;
        public LoginVM()
        {
            users = new List<User>();
            user = new User();
            ApiUrl = "https://stirred-eagle-95.hasura.app/api/rest/";
            //LoadAsync();
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
                user.Email = value;
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
                    Console.WriteLine(res);
                    Console.WriteLine("----------------------------");

                    res = GetArrayStringResponce(res);
                    Console.WriteLine(res);
                    //res = res.Replace("label", "Label");

                    List<User> temp = null;

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        Console.WriteLine(res);
                        temp = JsonSerializer.Deserialize<List<User>>(res, options);


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
            JObject jo = JObject.Parse(jsonResult);
            jo.Property("text").Remove();
            jsonResult = jo.ToString();

            JObject jobj = JObject.Parse(jsonResult);
            jobj.Property("parsed").Remove();
            jsonResult = jobj.ToString();

            JObject job = JObject.Parse(jsonResult);
            JObject header = (JObject)job.First.First.First;
            JArray arr = new JArray();
            while (header != null)
            {

                //header.Property("measures").Remove();
                arr.Add(header.Property("food").First);

                header = (JObject)header.Next;

            }
            jsonResult = arr.ToString();

            //JObject jobje = JObject.Parse(jsonResult);
            //jobje.Property("_links").Remove();
            //jsonResult = jobje.ToString();

            //jsonResult = jsonResult.Remove(0, jsonResult.IndexOf("["));
            //return jsonResult.Remove(jsonResult.LastIndexOf("]") + 1);

            return jsonResult;
        }
    }
}
