using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeightLossApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionTrainingController : ControllerBase
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public SectionTrainingController(FitnessAssistantContext context)
        {
            _context = context;
        }
        // --- HTTP Request handl methods ---
        #region HTTP

        // Retrieves all data about exercises and sends it as response
        [HttpGet]
        public JsonResult Get()
        {
            // Sending responce
            return new JsonResult(_context.SectionTraining);
        }

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult Post(SectionTraining item)
        {
            _context.SectionTraining.Add(item);
            _context.SaveChanges();

            return new JsonResult("Added successfully");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult Put(SectionTraining item)
        {
            _context.SectionTraining.Update(item);
            _context.SaveChanges();

            return new JsonResult("Updated successfully");
        }

        // Deletes records using id 
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            SectionTraining item = _context.Find<SectionTraining>(id);
            _context.SectionTraining.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted successfully");
        }
        #endregion
    }
}
