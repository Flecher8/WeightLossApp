using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Training
    {
        public Training()
        {
            Exercise = new HashSet<Exercise>();
        }

        public int Id { get; set; }
        public int SectionTrainingId { get; set; }
        public string Complexity { get; set; }

        public virtual SectionTraining SectionTraining { get; set; }
        public virtual ICollection<Exercise> Exercise { get; set; }
    }
}
