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
    }
}
