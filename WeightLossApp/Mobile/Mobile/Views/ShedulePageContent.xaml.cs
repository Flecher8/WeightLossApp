using DevExpress.XamarinForms.Scheduler;
using Mobile.Services;
using Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShedulePageContent : ContentView
    {
        readonly RemindersNotificationCenter remindersNotificationCenter = new RemindersNotificationCenter();

        public ScheduleVM ViewModel { get; set; } 

        public ShedulePageContent()
        {
            DevExpress.XamarinForms.Scheduler.Initializer.Init();
            ViewModel = new ScheduleVM();
            InitializeComponent();

            BindingContext = ViewModel;
        }

        private void OnTap(object sender, SchedulerGestureEventArgs e)
        {
            if (e.AppointmentInfo == null)
            {
                ShowNewAppointmentEditPage(e.IntervalInfo);
                return;
            }
            AppointmentItem appointment = e.AppointmentInfo.Appointment;
            ShowAppointmentEditPage(appointment);
        }

        private void ShowAppointmentEditPage(AppointmentItem appointment)
        {
            var vm = new CustomAppointmentVM(appointment, this.storage);

            AppointmentEditPage appEditPage = new AppointmentEditPage(vm);
            Navigation.PushAsync(appEditPage);
        }

        private void ShowNewAppointmentEditPage(IntervalInfo info)
        {
            AppointmentEditPage appEditPage = new AppointmentEditPage(
                new CustomAppointmentVM(info.Start, info.End, info.AllDay, this.storage));
            Navigation.PushAsync(appEditPage);
        }

        void OnRemindersChanged(object sender, EventArgs e)
        {
            remindersNotificationCenter.UpdateNotifications(storage);
        }
    }

}