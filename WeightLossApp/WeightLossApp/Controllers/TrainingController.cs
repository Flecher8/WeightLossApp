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
    public class TrainingController : ControllerBase
    {
        // DataBase context
        private readonly FitnessAssistantContext _context;

        public TrainingController(FitnessAssistantContext context)
        {
            _context = context;
        }
        // --- HTTP Request handl methods ---
        #region HTTP

        // Retrieves all data about training and sends it as response
        [HttpGet]
        public JsonResult Get()
        {
            // Load training data
            LoadTrainingData();

            // Sending responce
            return new JsonResult(_context.Training);
        }

        // Retrieves all data about TrainingExercise and sends it as response
        [HttpGet("TrainingExercise")]
        public JsonResult GetTrainingExercise()
        {
            // Sending responce
            return new JsonResult(_context.TrainingExercise);
        }

        // Used to add new records to DB, input - json 
        [HttpPost]
        public JsonResult Post(Training item)
        {
            _context.Training.Add(item);
            _context.SaveChanges();

            return new JsonResult("Added successfully");
        }

        // Used to add new records to DB, input - json 
        [HttpPost("TrainingExercise")]
        public JsonResult PostTrainingExercise(TrainingExercise item)
        {
            _context.TrainingExercise.Add(item);
            _context.SaveChanges();

            return new JsonResult("Added successfully");
        }

        // Used to update existing DB records, input - json 
        [HttpPut]
        public JsonResult Put(Training item)
        {
            _context.Training.Update(item);
            _context.SaveChanges();

            return new JsonResult("Updated successfully");
        }

        // Deletes records using id 
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            CascadeDelete(id);

            Training item = _context.Find<Training>(id);
            _context.Training.Remove(item);
            _context.SaveChanges();

            return new JsonResult("Deleted successfully");
        }
        #endregion
        private void LoadTrainingData()
        {
            LoadTrainingExerciseData();
            LoadSectionTrainingData();
        }
        private void LoadTrainingExerciseData()
        {
            foreach (Training training in _context.Training)
            {
                List<TrainingExercise> res = _context.TrainingExercise.Where(trE => trE.TrainingId == training.Id).ToList();
                foreach(var x in res)
                {
                    training.TrainingExercise.Add(x);
                }
            }
        }
        private void LoadSectionTrainingData()
        {
            foreach(Training training in _context.Training)
            {
                if(training.SectionTrainingId != null)
                {
                    List<SectionTraining> sectionTrainings = _context.SectionTraining.Where(x => x.Id == training.SectionTrainingId).ToList();
                    training.SectionTraining = sectionTrainings.ElementAt(0);
                }
            }
        }
        private void CascadeDelete(int id)
        {
            CascadeDeleteTrainingExercise(id);
        }
        private void CascadeDeleteTrainingExercise(int id)
        {
            // If in TrainingExercise table column Training_ID equals id, delete this row 
            foreach (TrainingExercise trainingExercise in _context.TrainingExercise)
            {
                if (trainingExercise.TrainingId == id)
                {
                    _context.TrainingExercise.Remove(trainingExercise);
                }
            }

            // Save data to database
            _context.SaveChanges();
        }
    }
}
