using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace WeightLossApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignThemeDataController : ControllerBase
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public DesignThemeDataController(FitnessAssistantContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public JsonResult GetDesignThemes()
        {
            return new JsonResult(_context.DesignThemeData);
        }
        

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult PostDesignThemeData(DesignThemeData item)
        {
            _context.DesignThemeData.Add(item);
            _context.SaveChanges();

            return new JsonResult("Success!!");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult PutDesignThemeData(DesignThemeData item)
        {
            _context.DesignThemeData.Update(item);
            _context.SaveChanges();

            return new JsonResult("Success!!");
        }
        
        // Deletes records using id 
        [HttpDelete("{id}")]
        public JsonResult DeleteDesignThemeData(int id)
        {
            DesignThemeData item = _context.Find<DesignThemeData>(id);
            _context.DesignThemeData.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted");
        }
    }
}
