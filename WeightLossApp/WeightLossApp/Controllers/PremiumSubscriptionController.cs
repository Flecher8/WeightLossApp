using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace WeightLossApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiumSubscriptionController : ControllerBase
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public PremiumSubscriptionController(FitnessAssistantContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetPremiums()
        {
            return new JsonResult(_context.PremiumSubscription);
        }
        
        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult PostPremium(PremiumSubscription item)
        {
            _context.PremiumSubscription.Add(item);
            _context.SaveChanges();

            return new JsonResult("Success!!");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult PutPremium(PremiumSubscription item)
        {
            _context.PremiumSubscription.Update(item);
            _context.SaveChanges();

            return new JsonResult("Success!!");
        }

        // Deletes records using id 
        [HttpDelete("{id}")]
        public JsonResult DeletePremium(int id)
        {
            PremiumSubscription item = _context.Find<PremiumSubscription>(id);
            _context.PremiumSubscription.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted");
        }
    }
}
