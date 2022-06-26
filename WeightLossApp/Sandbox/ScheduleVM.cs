using System;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Mobile.Helpers;
using Mobile.Models;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Xamarin;

namespace Sandbox
{
    public class ScheduleVM : PropertyChangedIpmlementator
    {
        // Data
        // Profile!!!
        private ObservableCollection<Event> events;
        private DateTime date;

        // Commands
        public Command LeftDate { get; private set; }
        public Command RightDate { get; private set; }
        public Command AddEvent { get; private set; }

        // Navigation
        public INavigation Navigation { get; set; }

        public ScheduleVM()
        {
            // Create Data
            // Fill profile!!!
            date = DateTime.Now;
            fillEvents(date);

            // Create Commands
            LeftDate = new Command(SwitchDateLeft);
            RightDate = new Command(SwitchDateRight);
            AddEvent = new Command(AddNewEvent);
        }
        // Доделать !!!
        private void AddNewEvent()
        {
            // Add navigation to AddEvent View!!!
            //Navigation.PushAsync();
        }

        // Доделать !!!
        private void SwitchDateRight()
        {
            throw new NotImplementedException();
        }

        // Доделать !!!
        private void SwitchDateLeft()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Event> Events
        {
            get => events;
        }

        // Доделать !!!
        // Функция заполнения events по определённой дате
        private void fillEvents(DateTime date)
        {
            // By profile ID find in table Schedule needed row with Schedule ID
            // Find all events for this Schedule ID and fill ObservableCollection<Event> events
        }
    }
}
