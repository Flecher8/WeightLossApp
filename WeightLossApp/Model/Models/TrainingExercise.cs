using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class TrainingExercise
    {
        public int TrainingId { get; set; }
        public int ExerciseId { get; set; }

        public virtual Training Training { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}
