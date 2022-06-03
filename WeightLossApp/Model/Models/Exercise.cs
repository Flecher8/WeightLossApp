using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            TrainingExercise = new HashSet<TrainingExercise>();
        }

        public int Id { get; set; }
        public string Section { get; set; }
        public string Name { get; set; }
        public DateTime Length { get; set; }
        public string Instructions { get; set; }
        public byte[] ImageName { get; set; }
        public int BurntCalories { get; set; }
        public int NumberOfReps { get; set; }
        public int TrainingId { get; set; }

        public virtual Training Training { get; set; }
        public virtual ICollection<TrainingExercise> TrainingExercise { get; set; }
    }
}
