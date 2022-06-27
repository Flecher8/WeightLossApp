using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealAddPage : ContentPage
    {
        public ObservableCollection<DataObject> Foods { get; set; }

        public MealAddPage()
        {
            Foods = new ObservableCollection<DataObject>()
            {
                new DataObject("Tomato"),
                new DataObject("Potato"),
            };
            InitializeComponent();
            BindingContext = this;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IngridientsPage());
        }
    }

    public class DataObject {
        public string Name { get; set; }
        public double Weight { get; set; }

        public DataObject(string name)
        {
            Name = name;
            Weight = 100;
        }
    }
}