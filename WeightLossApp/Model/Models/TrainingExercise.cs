using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class TrainingExercise
    {
        public int TrainingId { get; set; }
        public int ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }
        public virtual Training Training { get; set; }
    }
}
