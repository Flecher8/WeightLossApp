using DevExpress.XamarinForms.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin;
using Xamarin.Forms;
using Mobile.Models;
using Mobile.Services;
using Mobile.Helpers;

namespace Mobile.ViewModels
{
    public class SheduleVM : PropertyChangedIpmlementator
    {
        public EventService data;

        public IReadOnlyList<Event> EventRepository
        { 
            get => data.EventRepository; 
        }

        public SheduleVM()
        {
            data = new EventService();
        }
    }
}
