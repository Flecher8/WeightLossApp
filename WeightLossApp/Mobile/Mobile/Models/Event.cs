using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool SendNotification { get; set; }
        public int Schedule_ID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
