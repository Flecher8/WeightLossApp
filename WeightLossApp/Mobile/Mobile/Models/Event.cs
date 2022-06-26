using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Event
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public bool SendNotification { get; set; }
        public int ScheduleId { get; set; }
        public DateTime DataTime { get; set; }
    }
}
