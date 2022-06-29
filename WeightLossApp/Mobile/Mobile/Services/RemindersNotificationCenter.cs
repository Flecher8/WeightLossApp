using DevExpress.XamarinForms.Scheduler;
using Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mobile.Services
{
    internal class RemindersNotificationCenter
    {
        const int MaxNotificationsCount = 32;

        readonly INotificationCenter notificationCenter;

        public RemindersNotificationCenter()
        {
            this.notificationCenter = DependencyService.Get<INotificationCenter>();
        }

        public void UpdateNotifications(SchedulerDataStorage storage)
        {
            IList<TriggeredReminder> futureReminders =
                                            storage.GetNextReminders(MaxNotificationsCount);
            notificationCenter.UpdateNotifications(futureReminders, MaxNotificationsCount);
        }
    }
}
