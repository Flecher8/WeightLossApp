using System;
using System.Collections.Generic;
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
    public class AddEventVM : PropertyChangedIpmlementator
    {
        // Data
        private ObservableCollection<string> categories;
        private string selectedCategory;
        private string messageText;
        private DateTime date;
        private DateTime time;


        // Commands
        public Command Back { get; private set; }
        public Command Create { get; private set; }

        // Navigation
        public INavigation Navigation { get; set; }

        public AddEventVM()
        {
            // Create data
            categories = new ObservableCollection<string>();
            fillCategories();
            date = DateTime.Today;
            time = DateTime.Now;

            // Set commands
            Back = new Command(goBack);
            Create = new Command(CreateEvent);
        }

        // List of categories
        public ObservableCollection<string> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
            }
        }
        public string MessageText
        {
            get => messageText;
            set
            {
                messageText = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
        public DateTime Time
        {
            get => time;
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }
        private void goBack()
        {
            Navigation.PopAsync();
        }
        // Need new functions!!!
        private void CreateEvent()
        {
            // 1) Post new event !!!
            // 2) Navigate to last page ( use goBack function)
        }
        private void fillCategories()
        {
            categories.Add("Food");
            categories.Add("Training");
            categories.Add("Medicine");
        }
    }
}
