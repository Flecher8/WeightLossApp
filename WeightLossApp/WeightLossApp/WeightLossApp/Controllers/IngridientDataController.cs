using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeightLossApp.Models;
using System.Collections.Generic;

namespace WeightLossApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngridientDataController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public IngridientDataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            List<IngridientData> res = new List<IngridientData>
            {
                new IngridientData
                {
                    ID = 100,
                    Name = "Potato",
                    Calories = 100f,
                    Proteins = 10f,
                    Carbohydrates = 10f,
                    Fats = 0
                },
                new IngridientData
                {
                    ID = 101,
                    Name = "Tomato",
                    Calories = 80f,
                    Proteins = 12f,
                    Carbohydrates = 15f,
                    Fats = 2f
                },
                new IngridientData
                {
                    ID = 102,
                    Name = "Chicken",
                    Calories = 300f,
                    Proteins = 50f,
                    Carbohydrates = 10f,
                    Fats = 15f
                },
            };

            return new JsonResult(res);
        }
    }
}
