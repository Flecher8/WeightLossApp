using DevExpress.XamarinForms.Scheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Helpers
{
    public interface INotificationCenter
    {
        void UpdateNotifications(IList<TriggeredReminder> reminders, int maxCount);
    }
}
