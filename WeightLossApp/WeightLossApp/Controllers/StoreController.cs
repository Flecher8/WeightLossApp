using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace WeightLossApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public StoreController(FitnessAssistantContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetPremiums()
        {
            return new JsonResult(_context.PremiumSubscription);
        }

        [HttpGet]
        public JsonResult GetDesignThemes()
        {
            return new JsonResult(_context.DesignThemeData);
        }

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult PostPremium(PremiumSubscription item)
        {
            _context.PremiumSubscription.Add(item);
            _context.SaveChanges();

            return new JsonResult("Success!!");
        }

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult PostDesignThemeData(DesignThemeData item)
        {
            _context.DesignThemeData.Add(item);
            _context.SaveChanges();

            return new JsonResult("Success!!");
        }
    }
}
