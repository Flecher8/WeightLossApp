using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XamarinForms.Scheduler;
using Mobile.Services;

namespace Mobile.ViewModels
{
    internal class CustomAppointmentVM : AppointmentEditViewModel
    {
        public bool IsUpdate = false;
        private AppointmentItem appointment = null;

        public CustomAppointmentVM(AppointmentItem appointment, SchedulerDataStorage storage)
            : base(appointment, storage)
        {
            this.appointment = appointment;
        }

        public CustomAppointmentVM(
            DateTime startDate,
            DateTime endDate,
            bool allDay,
            SchedulerDataStorage storage)
            : base(startDate, endDate, allDay, storage)
        {

        }

        public override Task<bool> SaveChanges()
        {
            var _event = new Models.Event()
            {
                AllDay = AllDay,
                EndTime = End,
                LabelID = Labels.IndexOf(Label),
                StartTime = Start,
                RecurrenceInfo = "",
                ReminderInfo = "",
                StatusID = Statuses.IndexOf(Status),
                Subject = Subject,
                Id = (appointment.SourceObject as Models.Event).Id,
            };

            if (IsUpdate)
            {
                EventService.UpdateEvent(_event);
            }
            else
            {
                EventService.AddEvent(_event);
            }


            return base.SaveChanges();
        }


    }
}
