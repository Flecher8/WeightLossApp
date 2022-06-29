using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XamarinForms.Scheduler;
using Mobile.Models;
using Mobile.Services;

namespace Mobile.ViewModels
{
    internal class CustomAppointmentVM : AppointmentEditViewModel
    {
        private SchedulerDataStorage _storage = null;
        private AppointmentItem _appointment = null;

        public CustomAppointmentVM(AppointmentItem appointment, SchedulerDataStorage storage)
            : base(appointment, storage)
        {
            _storage = storage;
            _appointment = appointment;
        }

        public CustomAppointmentVM(
            DateTime startDate,
            DateTime endDate,
            bool allDay,
            SchedulerDataStorage storage)
            : base(startDate, endDate, allDay, storage)
        {
            _storage = storage;
        }

        public override Task<bool> SaveChanges()
        {
            //var _event = new Models.Event()
            //{
            //    AllDay = AllDay,
            //    EndTime = End,
            //    LabelID = Labels.IndexOf(Label),
            //    StartTime = Start,
            //    RecurrenceInfo = "",
            //    ReminderInfo = "",
            //    StatusID = Statuses.IndexOf(Status),
            //    Subject = Subject,
            //};

            //if (IsUpdate)
            //{
            //    _event.Id = (appointment.SourceObject as Models.Event).Id;
            //    EventService.UpdateEvent(_event);
            //}
            //else
            //{
            //    EventService.AddEvent(_event);
            //}


            Task<bool> res = base.SaveChanges();
            IList<Event> events = (_storage.DataSource.AppointmentsSource as IList<Event>);

            if (_appointment == null)
            {
                Event _event = events[events.Count - 1];
                _event.ReminderInfo = _event.ReminderInfo != null ? _event.ReminderInfo : "empty";
                _event.RecurrenceInfo = _event.RecurrenceInfo != null ? _event.RecurrenceInfo : "empty"; 

                EventService.AddEvent(_event);
            }
            else
            {
                Event _event = _appointment.SourceObject as Event;
                _event.ReminderInfo = _event.ReminderInfo == null ? _event.ReminderInfo : "empty";
                _event.RecurrenceInfo = _event.RecurrenceInfo == null ? _event.RecurrenceInfo : "empty";

                EventService.UpdateEvent(_event);
            }

            return res;


        }


    }
}
