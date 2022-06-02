using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public bool SendNotification { get; set; }
        public int ScheduleId { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}
