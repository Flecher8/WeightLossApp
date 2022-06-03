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
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public IngridientDataController(FitnessAssistantContext context)
        {
            _context = context;
        }

        // --- HTTP Request handl methods ---
        #region HTTP

        // Retrieves all data about ingridients and sends it as response
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_context.IngridientData);
        }

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult Post(IngridientData item)
        {
            _context.IngridientData.Add(item);
            _context.SaveChanges();

            return new JsonResult("Succes!!");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult Put(IngridientData item)
        {
            _context.IngridientData.Update(item);
            _context.SaveChanges();

            return new JsonResult("Succes!!");
        }

        // Deletes records using id 
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            IngridientData item = _context.Find<IngridientData>(id);
            _context.IngridientData.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted");
        }
        #endregion
    }
}
