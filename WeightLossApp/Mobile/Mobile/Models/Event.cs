using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Event
    {
        public int Id { get; set; }
        public bool AllDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public int LabelID { get; set; }
        public int StatusID { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }

        public Event()
        {

        }
    }
}
