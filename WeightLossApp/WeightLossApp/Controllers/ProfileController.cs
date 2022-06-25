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
    public class ProfileController : Controller
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public ProfileController(FitnessAssistantContext context)
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
            return new JsonResult(_context.Profile);
        }

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult Post(Profile item)
        {
            _context.Profile.Add(item);
            _context.SaveChanges();

            return new JsonResult("Added successfully");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult Put(Profile item)
        {
            _context.Profile.Update(item);
            _context.SaveChanges();

            return new JsonResult("Updated successfully");
        }

        // Deletes records using id 
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            Profile item = _context.Find<Profile>(id);
            _context.Profile.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted successfully");
        }
        #endregion
    }
}
