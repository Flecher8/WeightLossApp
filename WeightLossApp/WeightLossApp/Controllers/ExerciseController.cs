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
    public class ExerciseController : ControllerBase
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public ExerciseController(FitnessAssistantContext context)
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
            return new JsonResult(_context.Exercise);
        }

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult Post(Exercise item)
        {
            _context.Exercise.Add(item);
            _context.SaveChanges();

            return new JsonResult("Added successfully");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult Put(Exercise item)
        {
            _context.Exercise.Update(item);
            _context.SaveChanges();

            return new JsonResult("Updated successfully");
        }


        // Deletes records using id 
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            Exercise item = _context.Find<Exercise>(id);
            _context.Exercise.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted successfully");
        }
        #endregion
    }
}
