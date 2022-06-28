﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;

namespace Mobile.Services
{
    public class MealService
    {
        public async Task PostAsync(Meal meal)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stirred-eagle-95.hasura.app/api/rest/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await client.GetAsync($"meal?name={meal.Name}");
                if (response.StatusCode is HttpStatusCode.Created) 
                { 
                    Console.WriteLine("Meal created." + response.StatusCode);
                }
            }
        }
    }
}
