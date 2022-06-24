using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Model.Models;
using System.Linq;



namespace WeightLossApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public CategoryController(FitnessAssistantContext context)
        {
            _context = context;
        }

        // --- HTTP Request handl methods ---
        #region HTTP

        // Retrieves all data about ingridients and sends it as response
        [HttpGet]
        public JsonResult Get()
        {
            // Sending responce
            return new JsonResult(_context.Category);
        }

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult Post(Category item)
        {
            _context.Category.Add(item);
            _context.SaveChanges();

            return new JsonResult("Succes!!");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult Put(Category item)
        {
            _context.Category.Update(item);
            _context.SaveChanges();

            return new JsonResult("Succes!!");
        }

        // Deletes records using id 
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            Category item = _context.Find<Category>(id);
            _context.Category.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted");
        }
        #endregion
    }
}
