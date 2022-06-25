using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Model.Models;
using System.Linq;

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
            // Loading data that should appear in responce
            var c = _context.IngridientCategory.ToList();
            var b = _context.Category.ToList();

            // Sending responce
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

        // Used to add categories to ingridient
        [HttpPost("/PostIngridientCategory/{newList}")]
        public JsonResult PostIngridientCategory(IEnumerable<IngridientCategory> newList)
        {
            if (newList == null) return null;

            // Collecting existing objectes, which are related with updated Ingridient
            IEnumerable<IngridientCategory> existing = _context.IngridientCategory.Where(i => i.IngridientId == newList.First().IngridientId);

            // Iterating through them to find what to delete
            foreach (IngridientCategory item in existing)
            {
                // Deleting only if element not in new list
                if (!newList.Contains(item))
                {
                    _context.IngridientCategory.Remove(item);
                }
            }

            // Iterating through newList to find what to add
            foreach (IngridientCategory item in newList)
            {
                // Adding only if element is new
                if (!newList.Contains(item))
                {
                    _context.IngridientCategory.Add(item);
                }
            }

            _context.SaveChanges();

            return new JsonResult("Succes!!");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult Put(IngridientData item)
        {

            IEnumerable<IngridientCategory> existing = _context.IngridientCategory.Where(i => i.IngridientId == item.Id);

            // Iterating through existing categories to find what to delete
            foreach (IngridientCategory element in existing)
            {
                // Deleting only if element not in new list
                if (!item.IngridientCategory.Contains(element))
                {
                    _context.IngridientCategory.Remove(element);
                }
            }

            // Iterating through newList to find what to add
            foreach (IngridientCategory element in item.IngridientCategory)
            {
                // Adding only if element is new
                if (!existing.Contains(element))
                {
                    _context.IngridientCategory.Add(element);
                }
            }

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
