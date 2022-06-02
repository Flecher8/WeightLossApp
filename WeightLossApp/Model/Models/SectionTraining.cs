using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class SectionTraining
    {
        public SectionTraining()
        {
            Training = new HashSet<Training>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Training> Training { get; set; }
    }
}
