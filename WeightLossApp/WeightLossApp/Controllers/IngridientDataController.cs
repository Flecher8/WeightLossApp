using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeightLossApp.Models;
using System.Collections.Generic;
using Model.Models;

namespace WeightLossApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngridientDataController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        private readonly FitnessAssistantContext _context;


        public IngridientDataController(FitnessAssistantContext context)
        {
            //_configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_context.IngridientData);
        }

        [HttpPost]
        public JsonResult Post(IngridientData item)
        {
            _context.IngridientData.Add(item);
            _context.SaveChanges();

            return new JsonResult("Succes!!");
        }

        [HttpPut]
        public JsonResult Put(IngridientData item)
        {
            _context.IngridientData.Update(item);
            _context.SaveChanges();

            return new JsonResult("Succes!!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            IngridientData item = _context.Find<IngridientData>(id);
            _context.IngridientData.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted");
        }
    }
}
