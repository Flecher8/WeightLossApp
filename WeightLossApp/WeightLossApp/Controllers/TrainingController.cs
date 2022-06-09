﻿using Microsoft.AspNetCore.Http;
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
    public class TrainingController : ControllerBase
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public TrainingController(FitnessAssistantContext context)
        {
            _context = context;
        }

    }
}
