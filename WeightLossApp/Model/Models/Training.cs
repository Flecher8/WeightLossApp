using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Training
    {
        public Training()
        {
            TrainingExercise = new HashSet<TrainingExercise>();
        }

        public int Id { get; set; }
        public int SectionTrainingId { get; set; }
        public string Complexity { get; set; }

        public virtual SectionTraining SectionTraining { get; set; }
        public virtual ICollection<TrainingExercise> TrainingExercise { get; set; }
    }
}
