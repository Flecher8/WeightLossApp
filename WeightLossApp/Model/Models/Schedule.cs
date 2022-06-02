using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Event = new HashSet<Event>();
        }

        public int Id { get; set; }
        public int ProfileId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<Event> Event { get; set; }
    }
}
