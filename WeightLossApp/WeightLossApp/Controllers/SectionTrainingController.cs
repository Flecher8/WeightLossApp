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

    }
}
